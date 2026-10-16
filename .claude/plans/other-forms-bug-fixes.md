# Fix plan — all forms except `FrmSoThuTien`

> Companion to [frmsothutien-bug-fixes.md](.claude/plans/frmsothutien-bug-fixes.md). That plan owns
> `FrmSoThuTien*.cs`; nothing here touches those files. The only shared file is
> `Service/Data/SoThuTienService.cs` (Task 10, item 1.10) — land one plan before the other, or
> coordinate that file.
>
> **For agentic workers:** executed with superpowers:subagent-driven-development, one dispatch per
> `### Task N`. Tasks are dependency-ordered — do not reorder them. Item numbers (0.1, 3.2, …) are
> stable references used by the Context and Files sections.

**Goal:** Remove every crash, silent data loss and wrong report number found in the 30 non-`FrmSoThuTien`
forms, without changing what the app is for.

**Architecture:** Fix in place, outermost layer first. Shared defects move into the layer that owns
them (`CRUDForm`, a new `TruyThuService`); per-form defects stay in the form. No new UI, no schema
changes, no EF model regeneration.

**Tech stack:** WinForms on .NET Framework 4.6.2, DevExpress 17.1, Entity Framework 6 over MySQL,
pre-C#6 language level.

**Spec:** none — this plan is the audit. The authority for behaviour is the running app against a
restored `DB/qlmamnon.sql`, plus the paper forms in `Documents/` for report layout.

## Global Constraints

- Language level is **pre-C#6 apart from string interpolation**. No `?.`, `??`, `nameof`,
  expression-bodied members. Write `x == null ? 0 : x.Value`, explicit `if (x != null)`,
  `ListUtil.IsEmpty(list)`.
- **Never** write `Nullable<T>.Value` on a grid-bound or DB-nullable value without a null check.
- `.Designer.cs` stays machine-generated. New wiring, layout fixes, null guards and column
  re-binding go in the constructor or `*_Load`. **Never** hand-edit `.resx`.
- Guard grid events with `e.RowHandle < 0` (covers InvalidRowHandle, NewItemRow, group and filter
  rows) and null-check `GetRow(...)` before use.
- Prefer field-name constants (`ViewBangThuTienFieldName.*`, `this.TablePrimaryKey`) over string
  literals and column indexes.
- Report failures with a `MessageBox` **and** `LoggerFacade` logging. There is no
  `Application.ThreadException` handler, so an unhandled exception is a crash dialog.
- Non-UI-thread updates go through `this.Invoke(new Action(() => { ... }))` or `async`/`await` with
  `Progress<T>`.
- `using` / `.Dispose()` for `Graphics`, `Pen`, `Brush`, modal dialogs and reports.
- UI text, table and column names stay Vietnamese. Keep the existing vocabulary.
- Build (the only build that works — `dotnet` cannot build this solution):
  ```bash
  "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" QLMamNon.sln /t:Build /p:Configuration=Debug
  ```
  Warning MSB4078 from `Setup\Setup.vdproj` is expected and is **not** a failure. Any other new
  warning or error is.
- **There is no test project and none is to be created.** Verification is a clean build plus the
  task's manual checklist, run against a *restored copy* of `DB/qlmamnon.sql` — never live data.
  Point `QLMamNon/app.config`'s `qlmamnonEntities` at the restore.
- If the app was not actually run, say so plainly in the report and the commit message. Do not
  describe an unverified change as fixed.

## Review Focus

Input classes the audit shows this code meets in production and currently mishandles. Each has a
task that owns it; listed here so the final review can check them as a set.

1. **A DB-nullable column rendered or edited in a grid** (`PhanLoaiThuId`, `SoLuong`, `DonGia`,
   `SoLuongBanGiao`, `LopId`) — today an unguarded unbox crashes the form. Tasks 3, 6, 11.
2. **A grouped or filtered grid** — row-handle walks that assume data rows crash or skip rows.
   Tasks 1, 2.
3. **A month boundary** — January (`Month - 1` → 0), a single-month range, a range spanning two
   months, end-of-month vs 1st-of-month `ngayTinh`. Tasks 10, 11.
4. **An empty or sparse database** — `First()`, `FirstOrDefault().Value` and `[0]` on empty results.
   Tasks 10, 11.
5. **A form reopened in the same app session** — cached lookup data, `IsEditing` left set, a form
   evicted from `FormFactory` by another form's key. Tasks 5, 12.

---

## Context

Audit of every form compiled into `QLMamNon.csproj` other than `FrmSoThuTien`, plus the service and
facade code they call. 30 forms read line by line.

### Three verified structural facts that drive several tasks

**1. All four `view*` entity sets are read-only.** `Dao/QLMNModel.edmx` maps `viewtaisan` (:809),
`viewhoctap` (:780), `viewbangiaotaisan` (:733) and `viewbangthutien` (:752) with a
`<DefiningQuery>`, and `rg -c ModificationFunctionMapping QLMNModel.edmx` → **0**. EF6 throws
`UpdateException` when `SaveChanges()` sees any of them as Added/Modified/Deleted. Rows returned by
the `ExecuteFunction` stubs **are** tracked (`MergeOption.AppendOnly`). So `FrmTaiSan`,
`FrmThongTinHocTap` and `FrmPhanBoTaiSan` are each wired to Add/Edit/Delete/Save against a
non-writable entity set — Tasks 3 and 9.

**2. `[NotMapped]` display properties are written as if they were columns, and a mapped column is
written as if it were a display property.** `lop.KhoiId` and `khoi.TruongId` are `[NotMapped]`
([lop.Partial.cs](QLMamNon/Dao/lop.Partial.cs), [khoi.Partial.cs](QLMamNon/Dao/khoi.Partial.cs)) —
editing them never marks the entity `Modified`. Conversely `hocsinh.NgayVaoLop` **is** mapped
([hocsinh.cs:21](QLMamNon/Dao/hocsinh.cs:21), edmx `:1671`), and `ThongTinHocSinhUtil` assigns it on
every loaded row. Tasks 2 and 8.

**3. `FormKey` is duplicated across 11 forms.** Four forms claim `FormDanhMucTruongHoc`, five claim
`FormBangKeThuHocPhi`. For `CRUDForm` subclasses that means `Form_FormClosed` evicts *another*
form's cached instance from `FormFactory` — Task 12.

### Decisions taken

- **D1 — view-backed CRUD forms:** make the grids **read-only** now (drop the Thêm/Sửa/Xóa/Lưu
  wiring, keep the grids as lists). These forms cannot save at all today, so nothing working is
  lost, and routing writes to the real tables (`taisan`, `hoctap`/`hocsinh_lop`, `taisan_lop`) is
  three separate features to schedule later. Tasks 3 and 9.
- **D2 — dead code: out of scope.** `FrmQuanHuyen.cs` and `FrmPhuongXa.cs` stay as empty shells
  reachable from the menu; `FrmThongTinCanDo.cs` and `FrmGiaoVien.cs` stay in the tree excluded from
  the csproj. **No task touches them. Do not delete them and do not remove the menu handlers.**

---

## Task 1: `CRUDForm` base class

Shared by every grid form, so it lands first. Tasks 8 and 9 depend on its save/error behaviour.

**Files:**
- Modify: `QLMamNon/Forms/CRUDForm.cs`

**Interfaces produced:** `onSavingError` now reports to the user and the log; `onSaving` sets the
Saved caption only on success; `Entities` is disposed in `Form_FormClosed`. Later tasks that
override `onSaving` must call `base.onSaving()` last and must not re-add a blanket
`Detached → Added` loop.

**Resolved contract (as implemented — Tasks 8 and 9 depend on it):** `onAdding` records the row
returned by `bindingSource.AddNew()` in an `addedRows` set, and `onSaving` promotes
`Detached → Added` for **only** those rows. The set is cleared after a successful save, on cancel,
and whenever `DataTable` is reassigned. Rows loaded from a view entity set are therefore never
promoted, which is what keeps the read-only-view forms in Task 9 from throwing `UpdateException`.
Rows typed directly into a grid's New Item Row are not tracked — no compiled `CRUDForm` grid enables
that row (see 0.1).

**0.1 `onAdding` assigns an `int` to the grid's `DataSource`.**
[CRUDForm.cs:219](QLMamNon/Forms/CRUDForm.cs:219):
```csharp
this.GridViewMain.GridControl.DataSource = GridControl.NewItemRowHandle;
```
`GridControl.NewItemRowHandle` is an `int` constant and `DataSource` is `object`, so this compiles
and replaces the bound list with a boxed integer — the grid loses its rows and `ShowEditForm()` has
no row to edit.

**Delete the assignment entirely.** Do not "fix" it to
`FocusedRowHandle = GridControl.NewItemRowHandle` — that was this plan's first instruction and it
was wrong: `OptionsView.NewItemRowPosition` is set in only two designers repo-wide
(`TinNhan/FrmTinNhanPhuHuynh.Designer.cs:394`, not a `CRUDForm`, and
`HocSinh/FrmThongTinCanDo.Designer.cs:625`, not compiled into the csproj), so on every compiled
`CRUDForm` grid that handle is not a focusable row — the assignment would be a no-op at best and
could move focus off the row `AddNew()` just created. `bindingSource.AddNew()` on the preceding line
already appends the row and moves `BindingSource.Position` to it, which the grid syncs focus to, and
`ShowEditForm()` then opens on it — DevExpress's normal add-through-EditForm path. Do not use
`GetRowHandle(bindingSource.Count - 1)` either: it is wrong under an active filter or sort.

Also guard the cast two lines up (`:217`), which is used unchecked:
```csharp
BindingSource bindingSource = this.GridViewMain.GridControl.DataSource as BindingSource;
if (bindingSource == null) { return; }
```
This bug dates to the initial commit, so **Thêm** has never worked on these forms. The human
partner's manual pass confirms the fix — a subagent cannot run the app.

**0.2 `onDeleting` loses rows and throws on unsaved ones.**
[CRUDForm.cs:262-271](QLMamNon/Forms/CRUDForm.cs:262):
- `GridViewMain.GetRow(...) as T` can be null (group or new-item row) → `Entities.Entry<T>(null)`
  throws `ArgumentNullException`.
- `DeleteSelectedRows()` removes **every selected** row but only the focused one is marked
  `Deleted` → a multi-select delete drops the rest from the grid while leaving them in the DB.
- Setting `State = Deleted` on a `Detached` entity (a row added via Thêm but not yet saved) throws
  `InvalidOperationException`.

Rewrite: capture `GridViewMain.GetSelectedRows()`, resolve each handle to `T` with a null check, and
per row — if the state is `Detached` or `Added`, set it to `EntityState.Detached`; otherwise set
`EntityState.Deleted`. Then remove those rows from the binding source. Keep the existing
confirmation `MessageBox` and the `DeletedCaption` status.

**0.3 `onSaving` hides failures and force-adds detached rows.**
[CRUDForm.cs:231-249](QLMamNon/Forms/CRUDForm.cs:231): the `catch` routes everything to
`onSavingError`, which handles only `DBConcurrencyException` — every other exception is swallowed
and the status bar still shows `StatusCaptions.SavedCaption`.
- `onSavingError` must `MessageBox.Show` the failure (Vietnamese, in the style of the other
  dialogs) and log it via `LoggerFacade`, keeping the existing `DBConcurrencyException` →
  `reloadDataTable()` path.
- Move `FormMainFacade.SetStatusCaption(..., SavedCaption)` inside the success path so a failed save
  never reports "Đã lưu".
- Reconsider the blanket `Detached → Added` loop (`:235-238`): it is what makes a read-only view
  entity throw (Task 9), and it re-adds rows detached for any other reason. Narrow it to rows the
  grid actually added, or drop it if the binding source already attaches new rows — say in the
  report which you chose and why.

**0.4 `onCanceling` unguarded cast.** [CRUDForm.cs:276](QLMamNon/Forms/CRUDForm.cs:276) —
`as BindingSource` then used at `:298` and `:302` without a null check. Add the same guard as 0.1.

**0.5 `Entities` is never disposed.** Created per form in the constructor
([CRUDForm.cs:58](QLMamNon/Forms/CRUDForm.cs:58)) and never released; `Form_FormClosed`
([:126](QLMamNon/Forms/CRUDForm.cs:126)) only unregisters the form. Each open/close leaks a
`DbContext` and its MySQL connection. Dispose it there and null the field so a late event cannot
use a disposed context.

**0.6 `GridViewMain_CustomDrawCell` dereferences `e.Column`.**
[CRUDForm.cs:150](QLMamNon/Forms/CRUDForm.cs:150) checks `e == null` then reads `e.Column.Caption`.
Add `if (e.Column == null) { return; }`.

- [ ] Build clean.
- [ ] Manual (`Danh mục → Khoản thu`): **Thêm** → the edit form opens on a new row and the existing
      rows are still visible after cancelling. Enter a row, **Lưu**, reopen the form → persisted.
      Add a row then delete it before saving → no crash. Select two rows, **Xóa**, **Lưu**, reopen →
      both gone. Stop the MySQL service and click **Lưu** → an error dialog appears, the log has the
      exception, and the status bar does **not** say "Đã lưu".
- [ ] Commit.

---

## Task 2: `NgayVaoLop` mass-write, and `FrmThongTinHocSinh`

Lands early because Tasks 4, 5 and 8 consume `ThongTinHocSinhUtil`.

**Files:**
- Modify: `QLMamNon/Dao/hocsinh.Partial.cs`
- Modify: `QLMamNon/Utils/ThongTinHocSinhUtil.cs`
- Modify: `QLMamNon/Forms/HocSinh/FrmThongTinHocSinh.cs`
- Modify: `QLMamNon/Forms/HocSinh/FrmXepLop.cs`, `QLMamNon/Forms/HocSinh/FrmXepLop..Service.cs`
  (consumers only)
- Modify: `QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs` (consumer only — line 72)

**3.2 Opening `Thông tin học sinh` dirties every student row.**
[ThongTinHocSinhUtil.cs:31](QLMamNon/Utils/ThongTinHocSinhUtil.cs:31) assigns
`row.NgayVaoLop = hocSinhLop.DateJoin` — a **mapped** column — on rows tracked by the form's
context. Every displayed student becomes `Modified`, so the next **Lưu** mass-updates
`hocsinh.NgayVaoLop` from `hocsinh_lop.DateJoin` for all of them, whether or not the user edited
anything.

Fix: add a `[NotMapped] public Nullable<System.DateTime> NgayVaoLopDisplay { get; set; }` to
`hocsinh.Partial.cs` (beside the existing `LopDangHoc`, `PhuongXa`, `QuanHuyen` display
properties) and have the util write **that** instead. Then update every consumer:
- `FrmThongTinHocSinh` — the grid column bound to `NgayVaoLop` must now bind to `NgayVaoLopDisplay`.
  `.Designer.cs` is machine-generated, so set the column's `FieldName` in the constructor or
  `FrmThongTinHocSinh_Load`, not in the Designer.
- `FrmXepLop.cs:84` writes `newRow.NgayVaoLop = DateTime.Now` for a student being moved into a lớp,
  and `:158` reads `hocSinhRow.NgayVaoLop` when calling `insertHocSinhToLop`. That value is a real
  write to `hocsinh_lop.DateJoin`, not a display value — keep it on `NgayVaoLop`, and make sure the
  util's new display write does not clobber it. If the two uses collide, use `NgayVaoLopDisplay` for
  the grid and keep `NgayVaoLop` for the insert, and say so in the report.
- `FrmTaoPhieuThu.cs:72` calls the util only to populate `LopDangHoc` for a filter — no change
  needed beyond confirming it still compiles and the `LopDangHoc IS NOT NULL` filter at `:74` still
  works.

**1.11 Printing học sinh while grouped crashes.**
[FrmThongTinHocSinh.cs:237-243](QLMamNon/Forms/HocSinh/FrmThongTinHocSinh.cs:237) loops
`i < GridViewMain.DataRowCount` but indexes with `GetVisibleRowHandle(i)`, which counts group rows
too → `GetRow` returns a group row → `dataRow as hocsinh` is null → `NullReferenceException` at
`:243`. Iterate data rows, or keep the visible-row walk but `continue` when the cast is null and use
a separate counter for `STT`. Grouped **and** filtered must both produce the right rows in the right
order.

**6.8 Grid setup is fragile.**
- [gcMain_Load:66-91](QLMamNon/Forms/HocSinh/FrmThongTinHocSinh.cs:66) hides columns by hardcoded
  index (`HidePhuPhiColumns = new int[] { 3 }`, `:23`) and dereferences
  `gvMain.Columns["HoTenCha"].OwnerBand`, `gvMain.Columns["DienThoai"].OwnerBand`,
  `gvMain.Columns["HoTenMe"].OwnerBand`, `gvMain.Columns["DienThoaiMe"].OwnerBand` (`:84-90`) with
  no null checks. Use the Designer's generated column fields instead of the index, and guard every
  lookup and every `.OwnerBand` / `.ParentBand` hop.
- `initCmbNamSinh:56` uses `i < DateTime.Now.Year`, so a child born in the current year cannot be
  filtered. Use `<=`.
- `cmbQuanHuyen_EditValueChanged:171` tests `EditValue != null` twice (identical condition, `&&`-ed)
  and does not clear `cmbPhuongXa`, leaving a phường from the previous quận selected. Fix both.
- `btnReset_Click:187` clears the combos but does not re-run the search, so the grid keeps the old
  result set. Re-run `loadThongTinHocSinh` with all-null filters.

- [ ] Build clean.
- [ ] Manual: note one student's `hocsinh.NgayVaoLop` in SQL. Open `Thông tin học sinh`, confirm the
      "Ngày vào lớp" column still shows the right value, click **Lưu** without editing, re-query →
      **unchanged**. Edit a student's name, **Lưu** → only that row changed. Group the grid by Lớp
      and print → every student appears once, `STT` runs 1..n. Filter, then print → only filtered
      rows. `Xếp lớp`: move a student into a lớp, **Lưu**, check `hocsinh_lop.DateJoin`. Search by
      a birth year equal to the current year → returns rows. Pick a quận, a phường, then change the
      quận → phường resets. **Làm lại** → grid shows all students again.
- [ ] Commit.

---

## Task 3: `FrmPhanBoTaiSan` — cannot open

**Files:**
- Modify: `QLMamNon/Forms/TaiSan/FrmPhanBoTaiSan.cs`

**1.1 The form throws before it is visible.**
[FrmPhanBoTaiSan.cs:39](QLMamNon/Forms/TaiSan/FrmPhanBoTaiSan.cs:39) assigns
`new BindingList<viewtaisan>(...)` to `viewTaiSanRowBindingSource.DataSource`; `:43` then casts that
same property to `List<viewtaisan>` → `InvalidCastException`, thrown from
`loadTaiSanToGridTaiSan()` which the constructor calls at `:34`. Keep the list in a local and
iterate that, or iterate the `BindingList`.

`:57` has the same defect against a DataSet-era type:
`DataView dataView = (DataView)this.viewBanGiaoTaiSanRowBindingSource.List;` — `.List` is a
`BindingList<viewbangiaotaisan>`, so this throws as soon as `DataTable` is non-null. Iterate
`this.DataTable`.

**2.3 Save cannot work (decision D1).** `viewbangiaotaisan` is a read-only view entity set, so
`btnSave` ([:33](QLMamNon/Forms/TaiSan/FrmPhanBoTaiSan.cs:33)) → `base.onSaving()` →
`UpdateException`. Pass `null` for the save button in `InitForm` and hide `btnSave` from the
constructor (not the Designer). Add a code comment stating that writes belong on the `taisan_lop`
table and that this form is read-only until that service exists. Delete the `onSaving` override at
`:248-251`, which only calls `base`.

With Save gone, `btnMoveRight` / `btnMoveLeft` / `gvLop_RowUpdated` can no longer persist anything.
Hide those two buttons as well rather than leaving controls that silently do nothing, and say in the
report what you hid.

**6.10 Remaining defects in the same file.**
- Duplicate `newRow.LopId` assignment at
  [:139 and :146](QLMamNon/Forms/TaiSan/FrmPhanBoTaiSan.cs:139) — drop one.
- Unguarded `SoLuongBanGiao.Value` at `:170`, `:218` and `:229`
  (`Nullable<double>`, [viewbangiaotaisan.cs:30](QLMamNon/Dao/viewbangiaotaisan.cs:30)), and
  unguarded `_taiSanIdToSoLuongDictionary[...]` lookups at `:218` and `:229`
  (`KeyNotFoundException`). Guard all of them.
- Two contexts in one form: the field `entities` (`:28`) feeds the tài sản grid (`:39`) while the
  base `Entities` feeds the lớp grid (`:85`), so the two grids are tracked by different contexts.
  Use one — the base `Entities` — and delete the field.
- `cmbLopHoc_EditValueChanged:189` prompts "Bạn có muốn lưu thay đổi không?" during form load, when
  `lopRowBindingSource` is first bound in `FrmPhanBoTaiSan_Load:96`. Suppress the prompt until the
  form has finished loading (a `bool isLoaded` set at the end of `_Load`). With Save removed the
  prompt should go entirely — confirm which you did.

- [ ] Build clean.
- [ ] Manual: `Tài sản → Phân bổ tài sản` **opens**. Pick a lớp → both grids populate, no
      "lưu thay đổi" prompt appears on load. Set one `taisan_lop.SoLuongBanGiao` to NULL in SQL and
      reopen → no crash. No Save button is offered.
- [ ] Commit.

---

## Task 4: `FrmXepLop`

**Files:**
- Modify: `QLMamNon/Forms/HocSinh/FrmXepLop.cs`
- Modify: `QLMamNon/Forms/HocSinh/FrmXepLop..Service.cs`

Depends on Task 2 (`NgayVaoLop` / `NgayVaoLopDisplay` split — read that task's resolution from the
dispatch before touching `:84` or `:158`).

**1.2 "Move left" crashes.**
[FrmXepLop.cs:110](QLMamNon/Forms/HocSinh/FrmXepLop.cs:110) casts the left grid's `DataSource` to
`List<hocsinh>`, but it is a `BindingList<hocsinh>`
([FrmXepLop..Service.cs:43](QLMamNon/Forms/HocSinh/FrmXepLop..Service.cs:43) and
[FrmXepLop.cs:39](QLMamNon/Forms/HocSinh/FrmXepLop.cs:39)) → null → `NullReferenceException` on
`.FindAll`. Resolve as `BindingList<hocsinh>` and loop, or use
`hocSinhRowBindingSourceDi.Find("HocSinhId", id)` as this file already does elsewhere. Keep the
`hocSinhRows[0]` guard against an empty match.

**3.7 Save deletes fee records with no confirmation.**
[FrmXepLop.cs:162-178](QLMamNon/Forms/HocSinh/FrmXepLop.cs:162) computes "students the DB has in the
target lớp but the right-hand grid does not" and then calls `deleteHocSinhLopByHocSinhIds` **and**
`SoThuTienService.DeleteBangThuTienByHocSinhIdsAndDate` for them. Any state where the grid is not
fully loaded — an exception inside `loadHocSinhToGridDen`, a lớp switched without a reload — turns
**Lưu** into a mass delete of that class's bảng thu tiền.
- Add a `MessageBox` confirmation naming the count and the students about to be removed, and abort
  on anything but Yes.
- Refuse to proceed when the right-hand grid is empty while the DB returns students for that lớp —
  that combination means the grid never loaded, not that the user emptied the class. Show a message
  saying so.
- Log via `LoggerFacade` what was deleted (ids and date), so a mistaken delete can be traced.
- The override deliberately never calls `base.onSaving()` and never `SaveChanges()` — it works
  entirely through stored procedures. Add a comment saying that, so nobody "fixes" it by adding a
  base call.

**Also in these files, fix while you are here:**
- `loadHocSinhToGridDi` ([FrmXepLop..Service.cs:28-38](QLMamNon/Forms/HocSinh/FrmXepLop..Service.cs:28))
  dereferences `namHoc.Value` when a lớp is selected but the năm học combo has been cleared →
  `InvalidOperationException`. Guard it and return early with the existing message-box style.
- `gvDen_RowUpdated` ([FrmXepLop.cs:183-189](QLMamNon/Forms/HocSinh/FrmXepLop.cs:183)) calls
  `hocSinhRowBindingSourceDi.RemoveCurrent()`, which removes whatever is *currently* selected in the
  left grid — not necessarily the student just moved. Remove the moved student by id instead.

**3.13 (transferred from Task 2 — the edit path for an existing member's ngày vào lớp).** Task 2
split the display value into `[NotMapped] hocsinh.NgayVaoLopDisplay` and left the mapped
`hocsinh.NgayVaoLop` as the real-write field, then made `onSaving` resolve the date display-first
with a null-skip guard. Two consequences land here:

- **An intentional edit is currently ignored.** `UCEditFormXepLop.Designer.cs:95` binds the date
  editor to the mapped `NgayVaoLop`, but `onSaving` now prefers `NgayVaoLopDisplay`, so a user who
  edits an existing member's date sees it discarded. And re-pointing the editor at the display
  property would be worse: it is `[NotMapped]`, so EF would never mark the row `Modified` and
  `onSaving`'s `State == Modified` test would never fire.
- **The fix is snapshot-based change detection**, the same pattern Task 8 uses for item 3.3.
  After `loadHocSinhToGridDen`, snapshot each row's resolved join date into a
  `Dictionary<int, DateTime?>` keyed by `HocSinhId`. At save time, treat a row whose editor value
  differs from its snapshot as needing `insertHocSinhToLop`, in addition to the rows EF reports as
  `Added`/`Detached`/`Modified`. Refresh the snapshot after a successful save. Keep the
  display-first resolution and the null-skip guard Task 2 added — they are correct; this only adds
  the missing "the user changed it" trigger.

Do not revert Task 2's narrowing of which rows reach `insertHocSinhToLop`: running the procedure for
every loaded row was itself a bug (see the ruling recorded in Task 2's interfaces).

**Do not change** `new DateTime(namHoc.Value + 1, 1, 1)` at `FrmXepLop..Service.cs:38`. It looks
wrong but may be the intended school-year convention; it is listed as out of scope.

- [ ] Build clean.
- [ ] Manual: move a student right, then left → no crash, the student is back in the left grid.
      Move a student right, click in the left grid on a *different* student, then complete the edit
      form → the moved student is the one removed from the left grid. Clear the năm học combo with a
      lớp selected → a message, no crash. **Lưu** with students missing from the right grid → the
      confirmation lists them; answering No changes nothing in `hocsinh_lop` or `bangthutien`;
      answering Yes deletes exactly those and logs them. Open the form, pick a lớp, and click **Lưu**
      immediately → nothing is deleted.
- [ ] Commit.

---

## Task 5: Receipt-creation forms, and extracting the truy-thu recalculation

**Files:**
- Create: `QLMamNon/Service/Data/TruyThuService.cs`
- Modify: `QLMamNon/QLMamNon.csproj` (add the `<Compile Include>` for the new file)
- Modify: `QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs`, `QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.Service.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmTaoPhieuChi.cs`

**Interfaces produced (Task 6 consumes these):**
```csharp
// QLMamNon/Service/Data/TruyThuService.cs
public class TruyThuService : BaseDataService
{
    // Recomputes SoTienTruyThu on the following month's bảng thu tiền for one student
    // after that student's receipts for `ngay`'s month have changed.
    public void UpdateSoTienTruyThuForBangThuTienNextMonths(qlmamnonEntities entities, DateTime ngay, int hocSinhId);
}
```

**Extraction (enables 3.10 in Task 6).** `updateSoTienTruyThuForBangThuTienNextMonths` and its
helper `evaluatePrevMonthViewBangThuTienTable` are private members of the `FrmTaoPhieuThu` partial
class ([FrmTaoPhieuThu.Service.cs:15 and :53](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.Service.cs:15)),
so `FrmPhieuThu` cannot reach them. Move both into the new `TruyThuService` **unchanged in
behaviour**, taking the `qlmamnonEntities` as a parameter instead of calling
`StaticDataFacade.GetQLMNEntities()` internally (`:17` and `:55` each create their own context
today — one context for the whole operation is both correct and fewer leaks). Have
`FrmTaoPhieuThu` call the service. Delete `FrmTaoPhieuThu.Service.cs` only if nothing else remains
in it; otherwise leave the file with whatever is left and remove the moved methods.

Note `SoThuTienService` already exposes a public `EvaluatePrevMonthViewBangThuTienTable`
(called from `FrmBaoCaoTinhHinhThuChi.cs:144` and `FrmDanhSachChuaNopHocPhi.cs:65`). If it is
behaviourally identical to the private copy, call it instead of moving a second copy, and say so in
the report. **Do not edit `SoThuTienService`** — it is shared with the other plan.

**1.8 Printing a non-student phiếu thu crashes.**
[FrmTaoPhieuThu.cs:242-245](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs:242) unboxes
`(int)cmbHocSinh.EditValue` three times. `btnIn_Click` is reached from `btnLuuTao_Click` (`:122`)
for **every** phân loại thu, including ones not listed in
`Settings.Default.PhanLoaiThuByHocSinh`, where no student is selected → `EditValue` is null →
`NullReferenceException`. Resolve once into an `int?`, skip the `GetLopsByHocSinhIds` lookup and the
`Lop` parameter when it is null. Also guard `authenticatedEntity` at `:248-249` — a null there would
crash the print of every receipt.

**3.8 Lookup caches are never refreshed, for the whole app session.**
`InitFormData` ([FrmTaoPhieuThu.cs:68](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs:68)) loads the
student list **and** `hocSinhIdToBangKeThuTienItems` behind an `isStaticDataLoaded` flag.
`FormFactory` caches the instance and this form is not a `CRUDForm`, so it has no
`FormClosed → CloseForm` wiring and the flag stays true until the app exits: newly enrolled students
never appear, and the "Còn lại" figure is computed from a `DaThu` captured at first open — so it is
wrong immediately after the first receipt is saved.

Reload the student list and `hocSinhIdToBangKeThuTienItems` on **every** `InitFormData()` call.
Keep `isStaticDataLoaded` only for genuinely static data (`StaticDataKeys.PhanLoaiThu`). Preserve
the existing `hocSinhRowBindingSource.Filter = "LopDangHoc IS NOT NULL"` (`:74`) across reloads.

Same pattern in [FrmTaoPhieuChi.cs:59-64](QLMamNon/Forms/ThuChi/FrmTaoPhieuChi.cs:59): a phân loại
chi added in `Danh mục → Phân loại chi` does not appear until restart. Reload
`phanLoaiChiRowBindingSource` on every `InitFormData()`.

**3.9 Duplicate student id in the receipt cache throws.**
[FrmTaoPhieuThu.cs:84](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs:84)
`hocSinhIdToBangKeThuTienItems.Add(item.HocSinhId, item)` → `ArgumentException` if
`GetBangKeTongHopThuTienForTaoPhieuThu` ever returns two rows for one student.
`FrmBangKeThuTrongNgay.cs:52` guards the same call with `ContainsKey`; do the same here.

**1.6 Editing a phiếu chi with NULL số lượng/đơn giá crashes.**
[FrmTaoPhieuChi.cs:113-114](QLMamNon/Forms/ThuChi/FrmTaoPhieuChi.cs:113)
`(decimal)this.PhieuChiRow.SoLuong` on a `Nullable<double>`
([phieuchi.cs:25-26](QLMamNon/Dao/phieuchi.cs:25)) → `InvalidOperationException` ("Nullable object
must have a value"). Use `this.PhieuChiRow.SoLuong == null ? 0m : (decimal)this.PhieuChiRow.SoLuong.Value`
for both. Also guard `(int)this.cmbPhanLoaiChi.EditValue` at `:169` and `:186`.

**3.6 "Lưu & Tạo" from an edit overwrites the edited phiếu chi.**
[FrmTaoPhieuChi.cs:86-96](QLMamNon/Forms/ThuChi/FrmTaoPhieuChi.cs:86) never resets `IsEditing`, so
after editing a phiếu chi and clicking **Lưu & Tạo** the form clears its fields but stays in edit
mode — the *next* save calls `updatePhieuChi()` and overwrites the original row instead of
inserting. `FrmTaoPhieuThu` gets this right
([FrmTaoPhieuThu.cs:125](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs:125)). Set
`this.IsEditing = false;` after a successful save in `btnLuuTao_Click`.

Same handler pair, same fix: `btnLuu_Click` ([:98-108](QLMamNon/Forms/ThuChi/FrmTaoPhieuChi.cs:98))
validates, then calls `btnLuuTao_Click`, which validates again, sets
`AddedAndAddingPhieuChiCaption` and then has `AddedPhieuChiCaption` set over it — and it closes the
form even when the inner save was rejected. Restructure to `FrmTaoPhieuThu`'s shape: a
`private bool onSavePhieuChi()` that validates, saves, sets `IsSaved`, and returns false on
rejection; `btnLuuTao_Click` and `btnLuu_Click` both branch on it.

While here: `cmbPhanLoaiChi_EditValueChanged:222` casts `GetColumnValue("DienGiai")` to `String`
unguarded and overwrites whatever the user typed in `txtNoiDung` — including when re-selecting the
same phân loại chi while editing an existing phiếu chi. Guard the cast, and only overwrite
`txtNoiDung` when it is empty or still equals the previous phân loại's `DienGiai`.

**Do not change** `Settings.Default.LastMaPhieuThu` incrementing at
[FrmTaoPhieuThu.cs:192](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs:192) — it desynchronises when the
user types a custom mã phiếu, but that is listed as out of scope.

- [ ] Build clean.
- [ ] Manual: create a phiếu thu for a phân loại thu **not** in `PhanLoaiThuByHocSinh` (no student
      selected) and let it print → no crash. Create one for a student, and without closing the form
      start a second for the same student → "Còn lại" reflects the first receipt. Add a học sinh in
      `Thông tin học sinh`, then open `Tạo phiếu thu` → the new student is in the list. Add a phân
      loại chi, then open `Tạo phiếu chi` → it is in the combo. Set a `phieuchi.SoLuong` to NULL and
      edit that row → no crash. Edit a phiếu chi, **Lưu & Tạo**, enter a different one, **Lưu** →
      two distinct rows, the original unchanged. Create a phiếu thu for a student and verify next
      month's `bangthutien.SoTienTruyThu` changed exactly as it did before the extraction.
- [ ] Commit.

---

## Task 6: Receipt-list forms and `PhieuThuService`

**Files:**
- Modify: `QLMamNon/Forms/ThuChi/FrmPhieuThu.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmPhieuChi.cs`
- Modify: `QLMamNon/Service/Data/PhieuThuService.cs`

Depends on Task 5's `TruyThuService.UpdateSoTienTruyThuForBangThuTienNextMonths` (exact signature
comes in the dispatch).

**1.4 Deleting a phiếu thu crashes when `PhanLoaiThuId` is NULL.**
[FrmPhieuThu.cs:179-185](QLMamNon/Forms/ThuChi/FrmPhieuThu.cs:179) reads seven cell values;
`int phanLoaiThuId = (int)this.GridViewMain.GetFocusedRowCellValue("PhanLoaiThuId");` unboxes a
`Nullable<int>` column ([phieuthu.cs:23](QLMamNon/Dao/phieuthu.cs:23)) → `NullReferenceException`.
Only `phieuThuId` is used today. Keep `phieuThuId` (null-checked) **and** `ngay` and `hocSinhId`
because 3.10 needs them; delete the other four locals. Use `this.TablePrimaryKey` instead of the
`"PhieuThuId"` literal.

**3.10 Deleting a phiếu thu leaves truy thu stale.** The create path recalculates
([FrmTaoPhieuThu.cs:150](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs:150)); `onDeleting`
([FrmPhieuThu.cs:167-194](QLMamNon/Forms/ThuChi/FrmPhieuThu.cs:167)) does not reverse it, so next
month's `SoTienTruyThu` keeps crediting a receipt that no longer exists. After the delete succeeds,
call `TruyThuService.UpdateSoTienTruyThuForBangThuTienNextMonths(this.Entities, ngay, hocSinhId)`
when `hocSinhId` is non-null. Guard the whole delete with try/catch → `MessageBox` + `LoggerFacade`
(it calls `SaveChanges` at `:189` outside any handler today).

**1.5 Same nullable-unbox crash in `FrmPhieuChi`.**
[FrmPhieuChi.cs:172-179](QLMamNon/Forms/ThuChi/FrmPhieuChi.cs:172) casts `SoLuong` and `DonGia` to
`double`; both are `Nullable<double>` ([phieuchi.cs:25-26](QLMamNon/Dao/phieuchi.cs:25)) → NRE.
Only `phieuChiId` is used — delete the other six locals, null-check `phieuChiId`, use
`this.TablePrimaryKey`, and wrap the `SaveChanges` at `:183` the same way.

`loadPhieuChi` ([FrmPhieuChi.cs:40](QLMamNon/Forms/ThuChi/FrmPhieuChi.cs:40)) passes a brand-new
`StaticDataFacade.GetQLMNEntities()` on every reload and never disposes it. The comment above it
explains why a fresh context is needed — keep the fresh context, but dispose the previous one (or
scope it in a field that `FormClosed` disposes).

**3.11 `PhieuThuService.UpdatePhieuThu` dereferences a possibly-missing row.**
[PhieuThuService.cs:56-57](QLMamNon/Service/Data/PhieuThuService.cs:56): `FirstOrDefault(...)` then
immediate property assignment → `NullReferenceException` if the row was deleted elsewhere. Return
early (or throw a message the caller shows) instead.

**3.12 `LoadPhieuThuByDateRangeWithGroupPhanLoaiThu` crashes on NULL phân loại.**
[PhieuThuService.cs:74](QLMamNon/Service/Data/PhieuThuService.cs:74) `phieuThuRow.PhanLoaiThuId.Value`
→ `InvalidOperationException`. Guard it the way the sibling `LoadPhieuThu` does at `:26-29`. Used by
`FrmBaoCaoTinhHinhThuChi:105`.

`LoadPhieuThu` (`:16`) and `LoadPhieuThuByDateRangeWithGroupPhanLoaiThu` (`:69`) each create an
undisposed context. Give them `using` scopes — the rows they return are detached-by-copy value
objects used only for display, so confirm nothing downstream relies on them staying tracked and say
so in the report.

**Do not** try to make `UpdatePhieuThu` write through the caller's context — `FrmPhieuThu` reloads
the grid precisely because this service uses its own, and that reload is correct.

- [ ] Build clean.
- [ ] Manual: set one `phieuthu.PhanLoaiThuId` and one `phieuchi.SoLuong` to NULL in SQL, then
      delete each row from its list form → no crash, the row disappears, the grid reloads. Delete a
      phiếu thu for a student with a following-month bảng thu tiền → `SoTienTruyThu` drops by that
      receipt's amount. Stop MySQL and delete → error dialog, logged, no crash. Run
      `Báo cáo tình hình thu chi` over a range containing the NULL-phân-loại receipt → no crash.
- [ ] Commit.

---

## Task 7: SMS, login, backup

Three independent forms, batched because each change is small and self-contained.

**Files:**
- Modify: `QLMamNon/Forms/TinNhan/FrmTinNhanPhuHuynh.cs`
- Modify: `QLMamNon/Forms/Authenticate/FrmLogin.cs`
- Modify: `QLMamNon/Forms/SystemSetting/FrmBackupDb.cs`

**1.9 Sending SMS crashes when a parent name is NULL.**
[FrmTinNhanPhuHuynh.cs:135-138](QLMamNon/Forms/TinNhan/FrmTinNhanPhuHuynh.cs:135) chains
`.Replace(PhraseFatherName, fatherName)` where the name came from a nullable column →
`ArgumentNullException` ("Value cannot be null. Parameter name: newValue"). Coalesce all three
(`hocSinhName`, `fatherName`, `motherName`) to `string.Empty` before the `Replace` chain.

**1.12 The student grid binds an unmaterialised query.**
[FrmTinNhanPhuHuynh.cs:103 and :107](QLMamNon/Forms/TinNhan/FrmTinNhanPhuHuynh.cs:103) assign the
`ObjectResult` of `getHocSinhByLopAndNgay(...)` straight to `hocsinhBindingSource.DataSource`. A
second enumeration throws "The result of a query cannot be enumerated more than once". Add
`.ToList()` to both — every other call site in the repo does.

**6.6 UI calls from a worker thread, and button mix-ups.**
- [backgroundWorker1_DoWork](QLMamNon/Forms/TinNhan/FrmTinNhanPhuHuynh.cs:220) shows two
  `MessageBox`es (`:236`, `:240`) from the non-UI thread. Pass the outcome out through `e.Result`
  and show the dialogs in `backgroundWorker1_RunWorkerCompleted` (`:246`), which already runs on the
  UI thread.
- `btnSend_Click` disables `btnGui` (`:173-174`) but re-enables `btnSend` in `finally` (`:187-188`)
  — two different buttons, so whichever one the user actually clicked may stay disabled or was never
  disabled. Pick the button that is on the form and use it in all four places
  (`btnSend_Click`, the `finally`, and `RunWorkerCompleted`). Check the Designer for which of
  `btnSend` / `btnGui` exists and is wired to the click handler; if both exist, say so in the report.
- `gridControl1_EmbeddedNavigator_ButtonClick:54` calls `entities.SaveChanges()` with no error
  handling → crash dialog on any DB failure. Wrap it: `MessageBox` + `LoggerFacade`.
- `txtNoiDung_EditValueChanging:194` counts `txtNoiDung.Text`, which is still the **old** value, so
  the remaining-characters label lags one keystroke. Use `e.NewValue`. Nothing enforces the
  160-character limit the label advertises — block or truncate at 160 in the same handler.
- `entities` is created in `_Load` (`:31`) and never disposed. Dispose it in a `FormClosed` handler
  wired from the constructor.

**1.13 `FrmLogin` NREs when there is no saved current form.**
[FrmLogin.cs:48-49](QLMamNon/Forms/Authenticate/FrmLogin.cs:48): `(String)` of a missing key yields
null, and `(FormShowType)` unboxing a null throws. Return early from `showCurrentForm()` when either
value is missing. Add an empty-username/password check in `btnLogin_Click` before hashing, with the
existing error-dialog style. Dispose `entities` (`:19`) on close.

**Do not change** the MD5 password hashing at `:28` — weak, but changing it invalidates every stored
password and needs a migration. Out of scope.

**6.5 A failed backup reports success.**
[FrmBackupDb.cs:30-55](QLMamNon/Forms/SystemSetting/FrmBackupDb.cs:30) chains `ContinueWith` without
checking `x.IsFaulted`, so when `MySqlFacade.BackupDb` throws: the exception is never observed, the
UI still steps to 50%, the upload runs against a file that does not exist, and
`onUploadMySqlFileCompleted` sets "Hoàn thành". For a backup feature that is the worst possible
failure mode.
- Check `x.IsFaulted` / `x.Exception` at the start of each continuation; on failure show the error,
  log it via `LoggerFacade`, leave the progress bar where it stopped, set `lblState` to a failure
  message, re-enable the close button, and do not run the later stages.
- Guard `File.Delete` at `:70` with `File.Exists` and a try/catch — a missing file currently throws
  on the UI thread.
- `:28` builds the path as `Path.GetTempPath() + "\\qlmn_backup_...sql"`, doubling the separator.
  Use `Path.Combine`.

- [ ] Build clean.
- [ ] Manual: blank a student's `HoTenCha` in SQL and send an SMS to that class → no crash. Switch
      lớp twice in `Tin nhắn phụ huynh` → the grid repopulates both times. Send a batch → the
      result dialog appears once, after the worker finishes, and the send button ends up enabled.
      Type past 160 characters → blocked, and the counter is correct as you type. Log in with empty
      fields → a message, no crash. Rename the MySQL service so backup fails, then run
      `Sao lưu dữ liệu` → an error is shown and logged, and it does **not** say "Hoàn thành".
- [ ] Commit.

---

## Task 8: Danh mục writes

**Files:**
- Modify: `QLMamNon/Forms/DanhMuc/FrmTruongHoc.cs`
- Modify: `QLMamNon/Forms/DanhMuc/FrmLopHoc.cs`
- Modify: `QLMamNon/Forms/DanhMuc/FrmKhoiHoc.cs`
- Modify: `QLMamNon/Forms/SystemSetting/FrmUser.cs`

Depends on Task 1 (`base.onSaving()` error reporting and the narrowed `Detached → Added` loop).

**3.1 `FrmTruongHoc` never saves anything.**
[FrmTruongHoc.cs:19](QLMamNon/Forms/DanhMuc/FrmTruongHoc.cs:19) binds `Entities.truongs.ToList()`,
so `InitForm`'s `dataTable as BindingList<T>`
([CRUDForm.cs:71](QLMamNon/Forms/CRUDForm.cs:71)) yields **null** and `onSaving`'s
`if (this.DataTable != null)` guard skips `SaveChanges()` entirely — adds, edits **and** deletes are
discarded with no error while the status bar says "Đã lưu". Replace with the pattern every sibling
form uses:
```csharp
Entities.truongs.Load();
this.truongRowBindingSource.DataSource = Entities.truongs.Local.ToBindingList();
```

**3.3 Changing only the Khối of a Lớp (or the Trường of a Khối) is silently dropped.**
`FrmLopHoc.onSaving` collects `modifiedRow` from `entry.State == EntityState.Modified`
([FrmLopHoc.cs:81](QLMamNon/Forms/DanhMuc/FrmLopHoc.cs:81)), but `lop.KhoiId` is `[NotMapped]` — EF
never sees that edit, so the `lop_khoi` rewrite at `:92-110` never runs for it. Identical in
[FrmKhoiHoc.cs:77](QLMamNon/Forms/DanhMuc/FrmKhoiHoc.cs:77) for `khoi.TruongId`.

Track the link change explicitly: the loaders already fetch the current value
([FrmLopHoc.cs:51](QLMamNon/Forms/DanhMuc/FrmLopHoc.cs:51),
[FrmKhoiHoc.cs:51](QLMamNon/Forms/DanhMuc/FrmKhoiHoc.cs:51)), so snapshot it per row into a
`Dictionary<int, int?>` keyed by `LopId`/`KhoiId` after load, and at save time treat a row whose
current value differs from its snapshot as needing the link rewrite, in addition to the rows EF
reports as `Modified`. Refresh the snapshot after a successful save.

Note `FrmKhoiHoc.cs:77` also uses `if` where the two branches above it use `else if` — harmless
today because the states are mutually exclusive, but make it `else if` for consistency.

**3.4 New Lớp/Khối get link rows with id 0.**
`FrmLopHoc.onSaving` builds `lop_khoi { LopId = row.LopId }` at
[:112-123](QLMamNon/Forms/DanhMuc/FrmLopHoc.cs:112) **before** `base.onSaving()` at `:125` runs
`SaveChanges()` — `row.LopId` is still 0 for an inserted row (DB identity), so the link row is
written against id 0. Same at
[FrmKhoiHoc.cs:108-119](QLMamNon/Forms/DanhMuc/FrmKhoiHoc.cs:108). Move the added-row link insert
**after** `base.onSaving()` and save again, so the identity exists when the FK is copied.

The immediate `Entities.deleteLopKhoiByLopId(...)` / `deleteKhoiTruongByKhoiId(...)` stored-procedure
calls ([FrmLopHoc.cs:89 and :98](QLMamNon/Forms/DanhMuc/FrmLopHoc.cs:89),
[FrmKhoiHoc.cs:85 and :94](QLMamNon/Forms/DanhMuc/FrmKhoiHoc.cs:85)) also run before the save, so a
failed `SaveChanges()` leaves links already deleted. Wrap the whole override in a transaction
(`Entities.Database.BeginTransaction()`), or if that fights the stored procedures, report the failure
loudly and reload the grid so the user sees the real state. Say which you did.

Keep the `StaticDataFacade.Reload(StaticDataKeys.LopHoc)` / `(KhoiHoc)` calls at the end
([FrmLopHoc.cs:128](QLMamNon/Forms/DanhMuc/FrmLopHoc.cs:128),
[FrmKhoiHoc.cs:124](QLMamNon/Forms/DanhMuc/FrmKhoiHoc.cs:124)) — and make sure they still run after
the reordering, since every other form's lớp/khối combo depends on them.

**3.5 A new user's privileges are written against `UserId = 0`.**
[FrmUser.cs:37-42](QLMamNon/Forms/SystemSetting/FrmUser.cs:37) calls
`authenService.UpdateUserPrivileges(Entities, userRow.UserId, userRow.UserPrivileges)` **before**
`base.onSaving()`. Same identity-ordering bug as 3.4: call `base.onSaving()` first, then write the
privileges, then save again. Keep the existing try/catch → `onSavingError` path, and move the
`SavedCaption` so a failure does not report success (Task 1 does the same in the base).

- [ ] Build clean.
- [ ] Manual: add, edit and delete a Trường học, reopening the form after each **Lưu** → all three
      persist. Change **only** a Lớp's Khối, **Lưu**, reopen → persisted, and `lop_khoi` has exactly
      one row for that lớp. Same for a Khối's Trường. Add a new Lớp with a Khối → `lop_khoi` carries
      the real new `LopId`, not 0. Add a new Khối with a Trường → same for `khoi_truong`. Open any
      form with a lớp combo afterwards → the new lớp is listed. Create a user with two privileges →
      `user_privilege` rows carry the new `UserId`. Stop MySQL mid-flow and **Lưu** → error dialog,
      and `lop_khoi` is not left emptied.
- [ ] Commit.

---

## Task 9: Read-only view forms (decision D1)

**Files:**
- Modify: `QLMamNon/Forms/TaiSan/FrmTaiSan.cs`
- Modify: `QLMamNon/Forms/HocSinh/FrmThongTinHocTap.cs`

Depends on Task 1.

**2.1 `FrmTaiSan`.** [FrmTaiSan.cs:23](QLMamNon/Forms/TaiSan/FrmTaiSan.cs:23) passes
`btnThem`, `btnChinhSua`, `btnXoa`, `btnLuu`, `btnHuyBo` to `InitForm` over a `viewtaisan` grid, and
`viewtaisan` is a read-only view entity set — **Lưu** after any edit throws `UpdateException`. Pass
`null` for all five and hide the buttons from the constructor (not the Designer), leaving the grid as
a read-only list. Add a code comment stating that writes belong on the `taisan` table and that this
is read-only until that service exists. Make the grid itself non-editable
(`gvMain.OptionsBehavior.Editable = false`) so the user cannot type changes that go nowhere.

**2.2 `FrmThongTinHocTap`.** [FrmThongTinHocTap.cs:30](QLMamNon/Forms/HocSinh/FrmThongTinHocTap.cs:30)
wires `btnLuu` and `btnHuyBo` over `viewhoctap`. Worse, `loadThongTinHocTap` mutates the tracked
rows at `:91` and `:96` (`row.NgayTinh`, `row.SoNgayNghiThang`), so every row is `Modified` before
the user touches anything — **Lưu throws even with no edits**.
- Same read-only treatment as 2.1.
- Stop dirtying the rows: either compute the two defaults into `[NotMapped]` display properties on
  `viewhoctap` (a new `viewhoctap.Partial.cs`, matching the existing `viewbangthutien.Partial.cs`
  pattern) and bind the columns to those in code-behind, or detach the rows after load. Say which
  you chose.
- Fix `:82` — `IntUtil.StringToInt((string)cmbThang.EditValue).Value` casts an editable combo's
  value to `string` and then dereferences an `int?`. Resolve as `int?`, validate `1 <= thang <= 12`,
  and return early with a message box rather than throwing.

**2.4 (transferred from Task 2 — the `NgayVaoLop` column has no maintainer).** `viewhoctap.NgayVaoLop`
is `hs.NgayVaoLop` (`DB/qlmamnon.sql:1575`), and Task 2 removed the accidental write that used to
keep `hocsinh.NgayVaoLop` populated — nothing in the application writes that column now, so this
form's "Ngày vào lớp" column reads blank for every student placed through `Xếp lớp`. The view also
exposes `DateJoin` (from `hocsinh_lop`), which is the authoritative join date. Re-point the column
to `DateJoin` in code-behind (not the Designer), or give the view's partial class a display property
resolving `DateJoin` first and `NgayVaoLop` second, consistent with how Task 2 resolved the same
question for `hocsinh`. Say which you chose and why.

This form is currently unreachable: its menu handler at `FrmMain.cs:91` is commented out. **Leave
`FrmMain.cs` alone** — keep the form compiling and correct, do not re-enable the menu item.

- [ ] Build clean.
- [ ] Manual: `Tài sản → Quản lý tài sản` opens, shows data, offers no Thêm/Sửa/Xóa/Lưu, and cells
      cannot be edited. `Thông tin học tập` (reach it by temporarily uncommenting its menu handler
      locally — **do not commit that**): pick lớp/năm/tháng → data loads, the ngày tính and số ngày
      nghỉ columns still show their computed defaults, and `Entities.ChangeTracker.Entries()` reports
      no `Modified` rows. Enter a non-numeric tháng → message box, no crash.
- [ ] Commit.

---

## Task 10: Cash-book and tình-hình-thu-chi reports

**Files:**
- Modify: `QLMamNon/Service/Data/SoThuTienService.cs` **(shared with the other plan — change only
  `GetSoTienTonDauKy`)**
- Modify: `QLMamNon/Forms/ThuChi/FrmSoQuyTienMat.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmSoQuyTienMatTheoNgay.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmBaoCaoTinhHinhThuChi.cs`

**1.10 `GetSoTienTonDauKy` crashes when a SUM is NULL or empty.**
[SoThuTienService.cs:599-606](QLMamNon/Service/Data/SoThuTienService.cs:599):
```csharp
decimal tongThu = entities.getSumSoTienThuByDateRange(Settings.Default.AppLannchDate, lastDayOfPrevMonth, null).First().Value;
```
`First()` on an empty result throws `InvalidOperationException`, and `.Value` on a NULL sum throws
too. Use `FirstOrDefault()` into a `decimal?` local and treat null as 0, for both `tongThu` and
`tongChi`. A fresh or sparsely populated DB currently breaks all three callers
(`FrmSoQuyTienMat:118`, `FrmSoQuyTienMatTheoNgay:121`, `FrmBaoCaoTinhHinhThuChi:120`).
**Change nothing else in this file.**

**4.2 The opening balance is computed from the wrong end of the range.**
`GetSoTienTonDauKy` resolves "end of the month before the date you pass it"
([SoThuTienService.cs:601](QLMamNon/Service/Data/SoThuTienService.cs:601)). All three callers pass
**`toDate`** ([FrmSoQuyTienMat.cs:90](QLMamNon/Forms/ThuChi/FrmSoQuyTienMat.cs:90),
[FrmSoQuyTienMatTheoNgay.cs:92](QLMamNon/Forms/ThuChi/FrmSoQuyTienMatTheoNgay.cs:92),
[FrmBaoCaoTinhHinhThuChi.cs:99](QLMamNon/Forms/ThuChi/FrmBaoCaoTinhHinhThuChi.cs:99)), so for any
range not inside a single month the "số tồn đầu kỳ" already contains the period's own transactions.
Pass `fromDate` in all three.

**4.2 (second half) `Sổ quỹ tiền mặt` counts bank transfers as cash.**
`SoTienChi = phieuChiRow.SoTien` ([FrmSoQuyTienMat.cs:156](QLMamNon/Forms/ThuChi/FrmSoQuyTienMat.cs:156))
and `soQuyTienMatItemThu.SoTienThu += phieuThuRow.SoTien` (`:197`) ignore `PaymentType`, so transfers
land in a **cash** book. `FrmSoQuyTienMatTheoNgay` splits TM/CK correctly (`:159-160`, `:203-209`).
Check the paper form in `Documents/` for what the printed `Sổ quỹ tiền mặt` expects: if it has no
chuyển khoản column, exclude `PaymentType == TRANSFER` rows; if it does, split them as the TheoNgay
form does. State which the document showed and what you chose.

**1.3 `FrmSoQuyTienMat` crashes on a receipt with no phân loại thu.**
[FrmSoQuyTienMat.cs:169](QLMamNon/Forms/ThuChi/FrmSoQuyTienMat.cs:169) deliberately handles
`PhanLoaiThuId == null` ("Thu tiền học phí"), and then `:179` and `:195` read
`phieuThuRow.PhanLoaiThuId.Value` → `InvalidOperationException`. Build the group key from
`phieuThuRow.PhanLoaiThuId == null ? 0 : phieuThuRow.PhanLoaiThuId.Value` in **both** places (they
must agree, or the second loop throws `KeyNotFoundException`).

**4.3 `Sổ quỹ tiền mặt theo ngày` silently drops tuition receipts.**
[FrmSoQuyTienMatTheoNgay.cs:169](QLMamNon/Forms/ThuChi/FrmSoQuyTienMatTheoNgay.cs:169) filters
`pt.PhanLoaiThuId.HasValue && pt.PhanLoaiThuId.Value > 0`, excluding exactly the receipts that
`:174` then has a now-unreachable "Thu tiền học phí" branch for. Drop that part of the filter (keep
the date range and the selected-ids filter) and key the group on 0 for NULL, as in 1.3 — `:184` and
`:200` both dereference `.Value` and must change together.

**4.5 Duplicate-key crash in both sổ quỹ forms.** Both keep a `SortedList` keyed by `DateTime` and
de-duplicate with millisecond offsets drawn from two different counters:
`AddMilliseconds(soQuyTienMatMap.Count)` for chi
([FrmSoQuyTienMat.cs:154](QLMamNon/Forms/ThuChi/FrmSoQuyTienMat.cs:154),
[FrmSoQuyTienMatTheoNgay.cs:157](QLMamNon/Forms/ThuChi/FrmSoQuyTienMatTheoNgay.cs:157)) and
`AddMilliseconds(addedCount++)` starting from 0 for thu (`:203` / `:216`). A thu group and a chi row
on the same date can produce the same key → `ArgumentException` ("Item has already been added").
Use one monotonically increasing counter shared by both insert loops in each form. Do not change the
report's row ordering: items must still sort by their real date, with same-date rows keeping the
current thu-before-chi order.

**4.4 `Báo cáo tình hình thu chi` on sparse data.**
[FrmBaoCaoTinhHinhThuChi.cs:96-102](QLMamNon/Forms/ThuChi/FrmBaoCaoTinhHinhThuChi.cs:96) assigns
`FirstOrDefault()` (a `decimal?`) into report parameters and then unboxes those parameters with
`(long)` for the arithmetic at `:100` and `:102` — a null gives a `NullReferenceException`, and the
unbox depends on the parameter's declared type. Compute `long` locals first (null → 0), assign the
locals to the parameters, and do the arithmetic on the locals.

- [ ] Build clean.
- [ ] Manual, reconciling every figure against direct SQL (`SELECT SUM(SoTien) …`): a two-month
      `Sổ quỹ tiền mặt` whose opening balance you know independently → "số tồn đầu kỳ" matches the
      balance at `fromDate`, not at `toDate`. A range containing a receipt with NULL
      `PhanLoaiThuId` → no crash, and the amount appears under "Thu tiền học phí" in **both** sổ quỹ
      reports. A date carrying both a thu group and a phiếu chi → no duplicate-key crash, rows in
      date order. A transfer receipt and a transfer expense → treated per the `Documents/` paper
      form. An empty date range in all three reports → zeros, no crash. An empty database → no
      crash.
- [ ] Commit.

---

## Task 11: The remaining reports

**Files:**
- Modify: `QLMamNon/Forms/ThuChi/FrmBaoCaoSoLuongHocSinhTheoLop.cs`
- Modify: `QLMamNon/Forms/TaiSan/FrmSoTheoDoiTaiSan.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmBangKeCacLoaiChi.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmDanhSachChuaNopHocPhi.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmBaoCaoThuHocPhiTheoThang.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmBangKeThuTheoLop.cs`
- Modify: `QLMamNon/Forms/ThuChi/FrmBangKeThuTrongNgay.cs`

**4.1 `Báo cáo số lượng học sinh theo lớp` drops the last month.**
[FrmBaoCaoSoLuongHocSinhTheoLop.cs:68](QLMamNon/Forms/ThuChi/FrmBaoCaoSoLuongHocSinhTheoLop.cs:68):
`for (DateTime date = fromDate; date < toDate; date = date.AddMonths(1))` with both bounds set to
end-of-month (`:64-65`). A single-month selection gives `fromDate == toDate`, so **the loop never
runs and the report is empty**; every other range is one month short. Use `date <= toDate`.

**4.7 `Sổ theo dõi tài sản` nullable dereferences.**
[FrmSoTheoDoiTaiSan.cs:61](QLMamNon/Forms/TaiSan/FrmSoTheoDoiTaiSan.cs:61) `row.LopId.Value` and
`:72`, `:91`, `:92` `row.SoLuongBanGiao.Value` (`Nullable<double>`,
[viewbangiaotaisan.cs:30](QLMamNon/Dao/viewbangiaotaisan.cs:30)) → `NullReferenceException` on an
unassigned handover row. Coalesce all four. The grouping at `:63` assumes the stored procedure
returns rows ordered by `TaiSanId`; add an explicit `OrderBy(row => row.TaiSanId)` after `.ToList()`
so the "first row of an asset" fields cannot land on the wrong row. Also guard
`(int)cmbYear.EditValue` at `:54`.

**1.7 `FrmBangKeCacLoaiChi` crashes every January.**
[FrmBangKeCacLoaiChi.cs:174](QLMamNon/Forms/ThuChi/FrmBangKeCacLoaiChi.cs:174) sets
`cmbThang.Text = (DateTime.Now.Month - 1).ToString()` → `"0"` in January, and `:46` does
`new DateTime((int)cmbNam.EditValue, int.Parse(cmbThang.Text), 1)` →
`ArgumentOutOfRangeException`. Default to the previous month properly — derive both year and month
from `DateTime.Now.AddMonths(-1)` — and validate the parsed month is 1-12 before constructing the
date, reusing the existing "Xin vui lòng chọn Năm/Tháng" message box on failure. Use
`int.TryParse`, not `int.Parse`, since `cmbThang` is editable.
Also guard `dateRanges[0]`..`dateRanges[3]` at `:160-163`: a month whose last day is a Sunday never
adds its final range, because the `DayOfWeek.Sunday` `continue` at `:91` runs before the
`i == totalDays - 1` check at `:102` — so fix that ordering **and** bound-check the four indexes the
way `:164-165` already bound-checks weeks 5 and 6. Guard the `item.PhanLoaiChi` dictionary key at
`:70-72` against null (`StaticDataUtil.GetMaPhanLoaiChiNameByPhieuChiId` can return null →
`ArgumentNullException`).

**4.6 `Ngày tính` is the 1st of the month, not the end.**
[FrmDanhSachChuaNopHocPhi.cs:48](QLMamNon/Forms/ThuChi/FrmDanhSachChuaNopHocPhi.cs:48) and
[FrmBaoCaoThuHocPhiTheoThang.cs:56](QLMamNon/Forms/ThuChi/FrmBaoCaoThuHocPhiTheoThang.cs:56) build
`new DateTime(year, month, 1)` and pass it to `getViewBangThuTienByNgayTinhAndLop`, while every
other caller passes an end-of-month or end-of-day value (`FrmBangKeThuHocPhi.cs:49`,
`FrmTaoPhieuThu.cs:78`, `FrmXepLop.cs:177`).
**Check first:** read the `getViewBangThuTienByNgayTinhAndLop` definition in `DB/qlmamnon.sql` and
determine which convention its `ngayTinh` parameter expects. If it is end-of-month, these two
reports are reading the **previous** month's bảng thu tiền — switch both to
`DateTimeUtil.DateEndOfMonth`. If the procedure genuinely wants the 1st, change nothing and record
what the SQL said. Do not guess.

**6.1 Full-table reload inside a per-row loop.**
[FrmDanhSachChuaNopHocPhi.cs:74-75](QLMamNon/Forms/ThuChi/FrmDanhSachChuaNopHocPhi.cs:74) and
[FrmBaoCaoThuHocPhiTheoThang.cs:82-83](QLMamNon/Forms/ThuChi/FrmBaoCaoThuHocPhiTheoThang.cs:82) call
`entities.hocsinhs.ToList()` **twice per row**. Hoist to a single list before the loop.

Both files also declare a **local** `entities` (`:49` / `:57`) that shadows the field initialised in
the constructor, so the field's `DbContext` is created and never used at all. Use one context per
report run, dispose it, and delete the dead field or the dead local.

**6.2 N+1 lớp lookups.** `StaticDataUtil.GetLopsByHocSinhIds` takes a list but is called with a
single-element list inside a loop at
[FrmBangKeThuTheoLop.cs:78](QLMamNon/Forms/ThuChi/FrmBangKeThuTheoLop.cs:78) and
[FrmBaoCaoSoLuongHocSinhTheoLop.cs:74](QLMamNon/Forms/ThuChi/FrmBaoCaoSoLuongHocSinhTheoLop.cs:74).
Batch the ids per date and call it once per date. In `FrmBangKeThuTheoLop` the date varies per
receipt (`item.Ngay`), so group the receipts by date first. Keep the results identical — the lookup
is date-sensitive, so a single call for the whole range would be wrong.

**6.3 `FrmBangKeThuTrongNgay` loads every receipt and every student.**
[FrmBangKeThuTrongNgay.cs:60 and :73](QLMamNon/Forms/ThuChi/FrmBangKeThuTrongNgay.cs:60) materialise
the whole `phieuthus` and `hocsinhs` tables, then filter in memory with
`item.Ngay.ToShortDateString() == fromDate.ToShortDateString()` (`:67`, `:79`) — a
culture-dependent string comparison under the app's forced `vi-VN` culture. Filter in the query on
`Ngay >= startOfDay && Ngay <= endOfDay` (use `DateTimeUtil.StartOfDate` / `EndOfDate`, as the
sibling forms do) and drop the string comparison. Load only the students the report needs.

- [ ] Build clean.
- [ ] Manual, reconciling against direct SQL: single-month `Báo cáo số lượng học sinh theo lớp` →
      non-empty, counts match; a three-month range → three months of rows. A handover row with NULL
      `SoLuongBanGiao` / NULL `LopId` → `Sổ theo dõi tài sản` renders, no crash. Set the machine
      clock to a January date and open `Bảng kê các loại chi` → defaults to the previous December,
      renders. A month ending on a Sunday → the last week's date range appears. A phiếu chi with no
      resolvable phân loại → no crash. `Danh sách chưa nộp học phí` for a month whose fees changed
      mid-month → figures match the month's end state (or the 1st, per what the SQL said), and the
      report is noticeably faster than before. `Bảng kê thu trong ngày` for a day with receipts →
      same rows as before the change, visibly faster.
- [ ] Commit.

---

## Task 12: `FormKey` collisions

A batch of one-line changes plus one guard. Runs late so it rebases cleanly over the earlier tasks.

**Files:**
- Modify: the ten form files in the table below
- Modify: `QLMamNon/Forms/Helper/FormFactory.cs`

**5.1** Every constant below already exists in
[AppForms.cs](QLMamNon/Constant/AppForms.cs) — this is a pure reassignment:

| File | Declares | Change to |
|---|---|---|
| `Forms/DanhMuc/FrmKhoiHoc.cs:22` | `FormDanhMucTruongHoc` | `FormDanhMucKhoiHoc` |
| `Forms/DanhMuc/FrmLopHoc.cs:22` | `FormDanhMucTruongHoc` | `FormDanhMucLopHoc` |
| `Forms/DanhMuc/FrmTinhThanhPho.cs:16` | `FormDanhMucTruongHoc` | `FormDanhMucTinhThanhPho` |
| `Forms/SystemSetting/FrmPreference.cs:21` | `FormBangKeThuHocPhi` | `FormPreference` |
| `Forms/ThuChi/FrmBangKeThuTheoLop.cs:28` | `FormBangKeThuHocPhi` | `FormBangKeThuTheoLop` |
| `Forms/ThuChi/FrmBangKeThuTrongNgay.cs:28` | `FormBangKeThuHocPhi` | `FormBangKeThuTrongNgay` |
| `Forms/ThuChi/FrmBaoCaoSoLuongHocSinhTheoLop.cs:26` | `FormBangKeThuHocPhi` | `FormBaoCaoSoLuongHocSinhTheoLop` |
| `Forms/ThuChi/FrmBaoCaoSiSoHocSinhTheoLop.cs:27` | `FormBangKeThuHocPhi` | `FormBaoCaoSiSoHocSinhTheoLop` |
| `Forms/TaiSan/FrmSoTheoDoiTaiSan.cs:34` | `FormBaoCaoChiTietHoatDongTaiChinh` | `FormSoTheoDoiTaiSan` |
| `Forms/ThuChi/FrmBaoCaoThuHocPhiTheoThang.cs:28` | `FormDanhSachChuaNopHocPhi` | `FormBaoCaoThuHocPhiTheoThang` |

Line numbers may have shifted from the earlier tasks — find the `this.FormKey = AppForms.…` /
`FormKey = AppForms.…` assignment in each file rather than trusting the number.

Why it matters: status-bar and caption updates land on the wrong form
([FormMainFacade.cs:120](QLMamNon/Facade/FormMainFacade.cs:120)), and for the three `CRUDForm`
subclasses `Form_FormClosed` → `FormMainFacade.CloseForm(FormKey)`
([CRUDForm.cs:126](QLMamNon/Forms/CRUDForm.cs:126)) evicts the **Trường học** entry from
`FormFactory` — so closing `Lớp học` while `Trường học` is open orphans the live instance and the
next menu click builds a duplicate window.

**Same task, related guard:** `FormFactory.GetFormCaption`
([FormFactory.cs:84-94](QLMamNon/Forms/Helper/FormFactory.cs:84)) calls `GetForm`, which
**constructs** the form when it is absent — and these constructors run `Entities.<table>.Load()`.
Reading a caption therefore instantiates a form and hits the DB. Return
`CommonConstant.EMPTY` when the key is not already present in the `forms` dictionary.

Also give `FrmBackupDb` and `FrmSelectHocSinhsToGenerate` the `FormKey` they lack (`FormBackupDb`;
`FrmSelectHocSinhsToGenerate` has no `AppForms` constant — leave it alone if adding one means
touching `AppForms` and `FormFactory` registration, and say so), or confirm in the report that
neither ever reaches `SetFormCaption`.

- [ ] Build clean.
- [ ] Manual: open `Trường học` and `Lớp học` together, close `Lớp học`, then click `Trường học` in
      the menu → the existing window is reactivated, not duplicated. Repeat for `Khối học` and
      `Tỉnh/Thành phố`. Open each of the renamed report forms and confirm the caption and status
      text are its own. Open `Tùy chọn` → its caption is not "Bảng kê thu học phí".
- [ ] Commit.

---

## Task 13: Date-range validation

A batch of the same change across ten forms.

**Files:** modify each of
`Forms/ThuChi/FrmSoQuyTienMat.cs`, `FrmSoQuyTienMatTheoNgay.cs`, `FrmBangKeThuHocPhi.cs`,
`FrmBangKeThuTheoLop.cs`, `FrmBangKeThuTrongNgay.cs`, `FrmBaoCaoTinhHinhThuChi.cs`,
`FrmBaoCaoHoatDongTaiChinh.cs`, `FrmBaoCaoChiTietHoatDongTaiChinh.cs`,
`FrmBaoCaoSoLuongHocSinhTheoLop.cs`, `FrmBaoCaoSiSoHocSinhTheoLop.cs`.

**6.9 Dead null guards mean no date validation at all.** `DateEdit.DateTime` is a non-nullable
`DateTime`, so `if (this.dateTuNgay.DateTime == null || this.dateDenNgay.DateTime == null)` is
always false. Every form above opens `btnXemBaoCao_Click` with that no-op guard (at
`FrmSoQuyTienMat:45`, `FrmSoQuyTienMatTheoNgay:47`, `FrmBangKeThuHocPhi:40`,
`FrmBangKeThuTheoLop:41`, `FrmBangKeThuTrongNgay:41`, `FrmBaoCaoTinhHinhThuChi:48`,
`FrmBaoCaoHoatDongTaiChinh:41`, `FrmBaoCaoChiTietHoatDongTaiChinh:43`,
`FrmBaoCaoSoLuongHocSinhTheoLop:39`, `FrmBaoCaoSiSoHocSinhTheoLop:40`) and therefore validates
nothing.

Replace each with a real check, keeping the existing message box text and style
("Xin vui lòng chọn ngày" / "Chọn ngày"):
- reject `DateTime.MinValue` (an unset `DateEdit`), and
- reject `tuNgay > denNgay` with its own message, for every form that has **both** editors.

`FrmBaoCaoSoLuongHocSinhTheoLop:45` already checks the ordering and
`FrmBangKeThuTrongNgay`/`FrmBaoCaoSiSoHocSinhTheoLop` have a single date editor — keep what is
already correct and do not duplicate it. Line numbers will have shifted from earlier tasks; locate
the guard in each `btnXemBaoCao_Click`.

- [ ] Build clean.
- [ ] Manual: in every form above, enter a reversed range → rejected with a message, no report.
      Clear a date editor → rejected. A valid range still renders exactly what it did before.
- [ ] Commit.

---

## Task 14: `DbContext` and report disposal

Runs last — it touches almost every file the earlier tasks changed.

**Files:**
- Modify: `QLMamNon/Facade/FormMainFacade.cs`
- Modify: every plain `XtraForm` holding a `qlmamnonEntities` field, listed below

**6.4 Contexts and reports are never disposed.** `StaticDataFacade.GetQLMNEntities()`
([StaticDataFacade.cs:30-34](QLMamNon/Facade/StaticDataFacade.cs:30)) returns a **new**
`qlmamnonEntities` on every call. `CRUDForm` subclasses are handled by Task 1 (0.5); the plain
`XtraForm`s each hold one for the form's lifetime and none dispose it:
`FrmSoQuyTienMat:33`, `FrmSoQuyTienMatTheoNgay:32`, `FrmBangKeCacLoaiChi:28`,
`FrmBangKeThuHocPhi:28`, `FrmBangKeThuTheoLop:29`, `FrmBangKeThuTrongNgay:29`,
`FrmBaoCaoTinhHinhThuChi:36`, `FrmBaoCaoChiTietHoatDongTaiChinh:31`,
`FrmBaoCaoThuHocPhiTheoThang:29`, `FrmBaoCaoSoLuongHocSinhTheoLop:27`,
`FrmBaoCaoSiSoHocSinhTheoLop:28`, `FrmDanhSachChuaNopHocPhi:28`, `FrmSoTheoDoiTaiSan:35`,
`FrmTinNhanPhuHuynh` (Task 7 covers it), `FrmLogin` (Task 7 covers it),
`FrmTaoPhieuThu:70`, `FrmTaoPhieuChi:42`.

For each: wire a `FormClosed` handler from the constructor that disposes the context and nulls the
field. Where a form is cached by `FormFactory` and reopened (`FrmTaoPhieuThu`, `FrmTaoPhieuChi` —
shown with `ShowDialog`, so `FormClosed` fires but the instance survives), **do not** dispose a
context the next open will need: give those forms a fresh context in `InitFormData()` instead,
disposing the previous one. Task 5 reloads their data on every `InitFormData()` anyway, so this
fits.

`FormMainFacade.ShowReport` ([:109-113](QLMamNon/Facade/FormMainFacade.cs:109)) never disposes the
`ReportPrintTool` or the `XtraReport`. `ShowPreview()` is modal, so dispose both after it returns —
check that against DevExpress 17.1's behaviour before committing, and if disposing the report breaks
the preview, dispose only the print tool and say so.

- [ ] Build clean.
- [ ] Manual: `SHOW STATUS LIKE 'Threads_connected'` in MySQL, then open and close every form in the
      app twice, run three reports, and check the count again → it returns to roughly its starting
      value and does not grow monotonically. Open `Tạo phiếu thu`, save a receipt, close it, reopen
      it → it still loads students and still saves.
- [ ] Commit.

---

## Known non-bugs / deliberately out of scope

- `PhieuThuService.InsertPhieuThu:41` storing the amount in `SoTien` regardless of payment type and
  distinguishing with `PaymentType` is the intended schema — `loadPhieuThu` reading
  `PhieuThuRow.SoTien` for a transfer receipt is correct, not a bug.
- `khoi.TruongId` / `lop.KhoiId` being `[NotMapped]` is by design (they are link-table projections).
  Task 8 fixes the *consumers*, not the mapping.
- MD5 without a salt for login passwords
  ([FrmLogin.cs:28](QLMamNon/Forms/Authenticate/FrmLogin.cs:28)) is weak, but changing it
  invalidates every stored password and needs a migration. Raise separately.
- `Settings.Default.LastMaPhieuThu` incrementing on insert
  ([FrmTaoPhieuThu.cs:192](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs:192)) desynchronises if the user
  types a custom mã phiếu. Pre-existing design; flag to the user rather than changing numbering.
- `FrmXepLop..Service.cs:38`'s `new DateTime(namHoc.Value + 1, 1, 1)` (next January as the lookup
  date) looks wrong but may be the intended school-year convention. Ask before changing.
- **Dead code (D2): out of scope.** `FrmQuanHuyen.cs`, `FrmPhuongXa.cs`, `FrmThongTinCanDo.cs`,
  `FrmGiaoVien.cs` and the `FrmMain` menu handlers that open the first two are left exactly as they
  are.
- `FrmMain.cs` is not modified by any task.

## Files to modify

```
QLMamNon/Forms/CRUDForm.cs                                  T1
QLMamNon/Dao/hocsinh.Partial.cs                             T2
QLMamNon/Utils/ThongTinHocSinhUtil.cs                       T2
QLMamNon/Forms/HocSinh/FrmThongTinHocSinh.cs                T2
QLMamNon/Forms/TaiSan/FrmPhanBoTaiSan.cs                    T3
QLMamNon/Forms/HocSinh/FrmXepLop.cs                         T2 (consumer), T4
QLMamNon/Forms/HocSinh/FrmXepLop..Service.cs                T2 (consumer), T4
QLMamNon/Service/Data/TruyThuService.cs                     T5 (new)
QLMamNon/QLMamNon.csproj                                    T5 (Compile Include)
QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.cs                     T2 (consumer), T5, T14
QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.Service.cs             T5
QLMamNon/Forms/ThuChi/FrmTaoPhieuChi.cs                     T5, T14
QLMamNon/Forms/ThuChi/FrmPhieuThu.cs                        T6
QLMamNon/Forms/ThuChi/FrmPhieuChi.cs                        T6
QLMamNon/Service/Data/PhieuThuService.cs                    T6
QLMamNon/Forms/TinNhan/FrmTinNhanPhuHuynh.cs                T7
QLMamNon/Forms/Authenticate/FrmLogin.cs                     T7
QLMamNon/Forms/SystemSetting/FrmBackupDb.cs                 T7
QLMamNon/Forms/DanhMuc/FrmTruongHoc.cs                      T8
QLMamNon/Forms/DanhMuc/FrmLopHoc.cs                         T8
QLMamNon/Forms/DanhMuc/FrmKhoiHoc.cs                        T8
QLMamNon/Forms/SystemSetting/FrmUser.cs                     T8
QLMamNon/Forms/TaiSan/FrmTaiSan.cs                          T9
QLMamNon/Forms/HocSinh/FrmThongTinHocTap.cs                 T9
QLMamNon/Dao/viewhoctap.Partial.cs                          T9 (new, if chosen)
QLMamNon/Service/Data/SoThuTienService.cs                   T10 — GetSoTienTonDauKy ONLY [shared]
QLMamNon/Forms/ThuChi/FrmSoQuyTienMat.cs                    T10, T13, T14
QLMamNon/Forms/ThuChi/FrmSoQuyTienMatTheoNgay.cs            T10, T13, T14
QLMamNon/Forms/ThuChi/FrmBaoCaoTinhHinhThuChi.cs            T10, T13, T14
QLMamNon/Forms/ThuChi/FrmBaoCaoSoLuongHocSinhTheoLop.cs     T11, T12, T13, T14
QLMamNon/Forms/TaiSan/FrmSoTheoDoiTaiSan.cs                 T11, T12, T14
QLMamNon/Forms/ThuChi/FrmBangKeCacLoaiChi.cs                T11, T14
QLMamNon/Forms/ThuChi/FrmDanhSachChuaNopHocPhi.cs           T11, T14
QLMamNon/Forms/ThuChi/FrmBaoCaoThuHocPhiTheoThang.cs        T11, T12, T14
QLMamNon/Forms/ThuChi/FrmBangKeThuTheoLop.cs                T11, T12, T13, T14
QLMamNon/Forms/ThuChi/FrmBangKeThuTrongNgay.cs              T11, T12, T13, T14
QLMamNon/Forms/DanhMuc/FrmKhoiHoc.cs                        T12 (FormKey)
QLMamNon/Forms/DanhMuc/FrmLopHoc.cs                         T12 (FormKey)
QLMamNon/Forms/DanhMuc/FrmTinhThanhPho.cs                   T12 (FormKey)
QLMamNon/Forms/SystemSetting/FrmPreference.cs               T12 (FormKey)
QLMamNon/Forms/ThuChi/FrmBaoCaoSiSoHocSinhTheoLop.cs        T12, T13, T14
QLMamNon/Forms/Helper/FormFactory.cs                        T12
QLMamNon/Forms/ThuChi/FrmBangKeThuHocPhi.cs                 T13, T14
QLMamNon/Forms/ThuChi/FrmBaoCaoHoatDongTaiChinh.cs          T13
QLMamNon/Forms/ThuChi/FrmBaoCaoChiTietHoatDongTaiChinh.cs   T13, T14
QLMamNon/Facade/FormMainFacade.cs                           T14
```

## Verification

Every task is gated on the same four steps, plus its own manual checklist:

1. **Build clean** with the MSBuild command in Global Constraints. Only MSB4078 from
   `Setup\Setup.vdproj` is acceptable; any other new warning or error fails the task.
2. **Run against a restored copy of `DB/qlmamnon.sql`** — never live data.
3. **Walk that task's manual checklist.** For money and report changes, reconcile against direct SQL.
4. **Open and close every form the task touched, twice**, and confirm
   `SHOW STATUS LIKE 'Threads_connected'` does not grow.
5. **Say in the report and the commit message which checks actually ran.** If the app was not run,
   say so plainly — do not describe an unverified change as fixed.
