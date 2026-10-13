# Fix plan — `QLMamNon/Forms/ThuChi/FrmSoThuTien.cs`

## Context

`FrmSoThuTien` is the money-entry grid for the whole school (sổ thu tiền). It carries 18 bugs, one of which means **grid edits to the main table columns never persist and crash the app**, and several of which cause **silent money-data loss**. Full audit below; user approved all four fix buckets (crash/correctness, Save/Cancel integrity, performance, hygiene), immediate-but-safe row delete, and MessageBox + log error reporting.

### The headline defect (verified, reshapes everything)

`viewbangthutien` is mapped as a **read-only** EF entity set:

- `Dao/QLMNModel.edmx:752` — `<EntitySet Name="viewbangthutien" … store:Type="Views">` with a `<DefiningQuery>`
- `grep -c ModificationFunctionMapping QLMNModel.edmx` → **0** (no `<UpdateFunction>` anywhere)

EF6 throws `UpdateException` ("Unable to update the EntitySet … because it has a DefiningQuery and no `<UpdateFunction>` element exists") when `SaveChanges()` sees such an entity as Modified. `getViewBangThuTienByNgayTinhAndLop` is an `ExecuteFunction` with default `MergeOption.AppendOnly`, so the rows **are** tracked. Therefore:

- Editing any mapped column in the grid (`SXThangTruoc`, `AnSangThangTruoc`, `AnToiThangTruoc`, `SoTienAnSangThangNay`, `SoTienAnToiThangNay`, `SoTienNangKhieu`, `SoTienTruyThu`, `SoTienDieuHoa`, `SoTienDoDung`, `GhiChu`, `STT`) then clicking **Lưu** throws at [FrmSoThuTien.cs:391](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:391) — **outside any try/catch**, and the repo has no `Application.ThreadException` handler → crash dialog.
- `loadViewBangThuTiens` already dirties mapped columns on every row at load ([BangThuTienUtil.cs:64-66](QLMamNon/Utils/BangThuTienUtil.cs:64), `:84`), so Save only appears to work when recomputed values happen to equal stored ones.
- The STT move buttons ([FrmSoThuTien.MoveRow.cs](QLMamNon/Forms/ThuChi/FrmSoThuTien.MoveRow.cs)) write `STT` on these same view instances, so reordering + Lưu is on the same broken path.

The writable table entity exists and is the repo's own established path: `entities.bangthutiens` ([SoThuTienService.cs:255](QLMamNon/Service/Data/SoThuTienService.cs:255), `:277`). **Save must go through `bangthutien`, not `viewbangthutien`.**

### Environment facts (CLAUDE.md is wrong)

- `dotnet build` / `dotnet run --project src/QLMamNon.csproj` / `dotnet test` all fail — legacy non-SDK csproj, `packages.config`, DevExpress 17.1, `ToolsVersion 12.0`, net462. There is **no `src/` directory and no test project** in `QLMamNon.sln`. Build with MSBuild; verification is manual.
- Language: no `<LangVersion>`. String interpolation is used in the repo; `?.`, `??`, `nameof`, expression-bodied members are **not**. Stay pre-C#6 idiom: `x == null ? 0 : x.Value`, `ListUtil.IsEmpty(list)`, explicit `if (x != null)`.

---

## Phase 1 — Crash & correctness

**1.1 `DataTable` is null until the first search.** [Designer.cs:272](QLMamNon/Forms/ThuChi/FrmSoThuTien.Designer.cs:272) sets only `Sort`; `viewBangThuTienRowBindingSource.DataSource` is never set, so `InitForm(..., ...DataSource)` ([FrmSoThuTien.cs:89](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:89)) passes null and `CRUDForm.InitForm`'s `dataTable as BindingList<T>` ([CRUDForm.cs:71](QLMamNon/Forms/CRUDForm.cs:71)) yields null. Clicking **Lưu** before searching NREs at [FrmSoThuTien.cs:320](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:320) (outside base's try).
→ In the constructor (not the Designer — CLAUDE.md forbids hand edits there) create a real `BindingList<viewbangthutien>`, assign it to the binding source, pass it to `InitForm`. Also guard `onSaving` with `if (ListUtil.IsEmpty(this.DataTable)) { return; }` (`BindingList<T>` is an `ICollection`, so `ListUtil.IsEmpty` takes it).

**1.2 Unguarded `Nullable<int>.Value` in `onSaving`.** [FrmSoThuTien.cs:322-323](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:322) → `InvalidOperationException` when the cell is cleared or the column is NULL. Use `x == null ? 0 : x.Value`. Line 324 (`AnToiThangTruoc`) is a non-nullable `int`, leave it.

**1.3 Same in `BangThuTienUtil.RecalculateBangThuTienKhoanThuList`** (reached from [FrmSoThuTien.cs:226](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:226)):
- [BangThuTienUtil.cs:214](QLMamNon/Utils/BangThuTienUtil.cs:214) — `GetKhoiIdByLopId(...).Value`; that helper returns null for a lop with no `lop_khoi` row ([StaticDataUtil.cs:124-140](QLMamNon/Utils/StaticDataUtil.cs:124)). Keep it as `int?`, early-return when null (no khối ⇒ no `khoanthuhangnam` to apply). `CalculateSoTienPhi` already accepts `int?`.
- [BangThuTienUtil.cs:228](QLMamNon/Utils/BangThuTienUtil.cs:228) — `SXThangTruoc.Value` → null-coalesce to 0.
- Shared file, but `RecalculateBangThuTienKhoanThuList`'s **only** caller is FrmSoThuTien:226 → safe to change in place.

**1.4 `viewBangThuTienRows[0]` with no empty check** ([FrmSoThuTien.cs:362](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:362)) → `IndexOutOfRangeException` for an orphan khoản-thu row. Replace the per-iteration `DataTable.Where(...)` + `[0]` with a `Dictionary<int, viewbangthutien>` keyed on `BangThuTienId` built once (also kills an O(N²) scan); `continue` on a miss.

**1.5 Newly inserted khoản-thu rows are persisted as 0 — silent money loss.** [FrmSoThuTien.cs:337-356](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:337) creates the missing `bangthutien_khoanthu` rows with hardcoded `SoTien = 0`, but the SoTien `switch` loop at `:359-389` iterates only the DB-fetched list, so those new rows never receive the user's amounts.
→ Seed one `List<bangthutien_khoanthu> rowsToUpdate` from `getBangThuTienKhoanThuByBangThuTienIds`, append each newly created row to it, then run the single `switch` loop over `rowsToUpdate`. Drop the hardcoded `SoTien = 0`.

**1.6 `CreateSubMenuRows` NREs on non-data rows.** [FrmSoThuTien.cs:430](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:430) unboxes `(int)view.GetRowCellValue(rowHandle, "BangThuTienId")` **before** the `IsDataRow` check on the next line → right-clicking a group/new-item/footer row throws.
→ Check `view.IsDataRow(rowHandle)` first and return a disabled item with `Tag = null`. Replace the `"BangThuTienId"` literal with `this.TablePrimaryKey` (already set at `:83`).

**1.7 `gvMain_CellValueChanged` NREs on the new-item row.** The guard only rejects `InvalidRowHandle`, so `gv.GetRow(...) as viewbangthutien` can be null at [:210](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:210)/`:219`.
→ At the top: bail if `gv == null || e.RowHandle < 0` (covers `InvalidRowHandle`, `NewItemRowHandle`, group and auto-filter rows), then resolve `row` once, bail if null, and use it in both blocks.

**1.8 `GetNgayTinh` type assumptions** ([FrmSoThuTien.cs:255-267](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:255)). `cmbNam` is a `LookUpEdit` over `int FromYear`, so that cast is practically safe; the real exposures are `IntUtil.StringToInt((string)cmbThang.EditValue).Value` on the **editable** `cmbThang` (`InvalidCastException` or `InvalidOperationException`) and an unguarded `DateTime.DaysInMonth` / `new DateTime(...)` when the month parses outside 1-12 (`ArgumentOutOfRangeException`).
→ Resolve both as `int?`, validate `1 <= thang <= 12`, return the existing `DateTime.MinValue` sentinel on failure (`isValidNgayTinh` at [GenerateSoThuTien.cs:29](QLMamNon/Forms/ThuChi/FrmSoThuTien.GenerateSoThuTien.cs:29) already shows a message box for it — no new UX). Also self-guard `(int)this.cmbLop.EditValue` at `:252`.

**1.9 Stale state after a failed/empty load** ([FrmSoThuTien.cs:514-563](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:514)). On the early `isNeedToGenerateSoThuTiens` return (`:516-520`) and when `bangThuTienIds` is empty (`:530`), the fields `DataTable`, `bTTKTDataTable`, `phieuThuDataTable`, `prevMonthRowDictionary` keep the **previous class/month's** values. Print then emits the old class while the combos show the new one, and cell recalcs run against stale khoản-thu/phiếu-thu data.
→ Reset all four (empty collections, empty `BindingList` bound to the grid) before the early return and when the id list is empty.

**1.10 One-time grid setup is in the `Enter` event** ([FrmSoThuTien.cs:106-135](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:106)) — re-runs on every focus gain, re-hiding anything the user unhid, and has no null guards on `Columns[name]` / `.OwnerBand` / `.ParentBand`.
→ Extract to `initGridLayout()` called from `FrmSoThuTien_Load`, with a `bool isGridLayoutInitialized` guard so the Designer subscription can stay. Inside: hoist each column into a local with a null check; replace `gvMain.Columns["PhucVuBanTru"]` with `ViewBangThuTienFieldName.PhucVuBanTru`; replace the magic `gvMain.Columns[19]` with the designer field `this.colSTT2` (index 19 confirmed from `Columns.AddRange`, Designer.cs:302-334 — a by-name lookup is impossible because `colSTT2.FieldName` is also `"STT"`, duplicating `colSTT` at index 0).

**1.11 `gcMain_MouseDown`** ([FrmSoThuTien.cs:137-167](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:137)): fires on right-click too, so right-clicking a band header toggles the sort *and* opens the context menu.
→ First line `if (e.Button != MouseButtons.Left) { return; }`. Delete the dead `Point pt` (`:139`) and the always-true `hit != null` tests. Replace the positional `gridBandHoTen.Columns[1]` / `gridBandSTT.Columns[0]` with the designer fields `this.colTen` / `this.colSTT` (verified: `gridBand17.Columns == [colHoTen, colTen]`, Designer.cs:370-371).

---

## Phase 2 — Save / Cancel integrity

**2.1 Rewrite `onSaving` to persist through `bangthutien`.** Shape:

```
if (ListUtil.IsEmpty(this.DataTable)) return;
try {
    using (DbContextTransaction transaction = Entities.Database.BeginTransaction()) {
        // build bangThuTienIds + Dictionary<int, viewbangthutien> in one pass (1.2 recompute here)
        // (a) mapped columns -> Entities.bangthutiens.Where(b => ids.Contains(b.BangThuTienId))
        //     copyToBangThuTien(viewRow, bangThuTienRow) for the 14 shared columns
        // (b) khoanthu rows (1.4 + 1.5 logic)
        // (c) Entities.SaveChanges()
        // (d) truy-thu stored proc per changed row, on the SAME context (2.2)
        transaction.Commit();
    }
    FormMainFacade.SetStatusCaption(this.FormKey, StatusCaptions.SavedCaption);
} catch (Exception ex) {
    this.onSavingError(ex);
    return;                       // never fall through to SavedCaption
}
btnTimKiem_Click(null, null);     // reload outside the transaction
```

New private helper `copyToBangThuTien(viewbangthutien source, bangthutien target)` assigning: `SXThangTruoc, SoTienSXThangTruoc, AnSangThangTruoc, SoTienAnSangThangTruoc, SoTienAnSangThangNay, AnToiThangTruoc, SoTienAnToiThangTruoc, SoTienAnToiThangNay, SoTienNangKhieu, SoTienTruyThu, SoTienDoDung, SoTienDieuHoa, STT, GhiChu`.

**Do not call `base.onSaving()`.** Its only extra behaviour over our own `SaveChanges()` is `Detached → Added` ([CRUDForm.cs:236-239](QLMamNon/Forms/CRUDForm.cs:236)), which on a `DefiningQuery` set is an attempted INSERT into a view — guaranteed to throw.

**2.2 Make the truy-thu stored-proc calls share the transaction.** Today each call opens its own context ([UpdateSoTienTruyThuOfBangThuTienCommand.cs:14](QLMamNon/Components/Command/QLMNDao/UpdateSoTienTruyThuOfBangThuTienCommand.cs:14)) so a mid-save failure leaves partial writes ([FrmSoThuTien.cs:391-411](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:391)).
- Use **`Entities.Database.BeginTransaction()`**, not `TransactionScope`: every generated proc wrapper goes through `ObjectContext.ExecuteFunction` on the same `EntityConnection` and enlists in an active `DbContextTransaction` automatically. `TransactionScope` would not capture the separate connections and needs MySQL distributed-transaction support.
- Shared file, change non-breakingly: add `public const string ParameterEntities = "Entities";` and use the passed context when the key is present, else fall back to `StaticDataFacade.GetQLMNEntities()`. The other call site, [FrmTaoPhieuThu.Service.cs:20-48](QLMamNon/Forms/ThuChi/FrmTaoPhieuThu.Service.cs:20), does not pass the key → behaviour unchanged.
- The proc itself is transaction-safe: a single `UPDATE bangthutien SET SoTienTruyThu = SoTienTruyThu + ? WHERE HocSinhId = ? AND NgayTinh > ?` ([DB/qlmamnon.sql:310](QLMamNon/DB/qlmamnon.sql:310)), no internal COMMIT. (Note for a later DB task: `NULL + n = NULL`, so rows with NULL `SoTienTruyThu` stay NULL — wants `COALESCE`.)

**2.3 `CommandParameter` reuse** ([FrmSoThuTien.cs:405-408](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:405)) only works because of the two `Remove` calls — it is a plain `Dictionary` that `Execute` never clears, so a reorder or a mid-loop exception yields `ArgumentException` or silently reuses the previous row's parameters.
→ Indexer assignment (`CommandParameter[Key] = value`), plus the new `ParameterEntities`. Keep `QLMNDaoJobInvoker` (same shape as FrmTaoPhieuThu, minimal diff). Also set `viewBangThuTienRow.OriginalThanhTien = currentVersionToCompare;` after each successful proc call so a second Lưu without a reload cannot double-apply the delta.

**2.4 Row delete: DB-first, transactional, handle-free.** Today [FrmSoThuTien.cs:447-450](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:447) removes the grid row **before** two untransacted deletes with no try/catch → UI/DB divergence on failure; and `RowInfo.RowHandle` is captured at menu-build time, so re-sorting between right-click and click deletes the wrong row.
→ `RowInfo`: replace `int RowHandle` with `viewbangthutien Row` (object identity survives sorting; `view.LocateByValue(TablePrimaryKey, id)` is the fallback if a handle is ever needed). New order: confirm → `using (DbContextTransaction …)` → `deleteBangThuTienKhoanThuByBangThuTienId` → `deleteBangThuTienById` → `Commit()` → **only then** `this.DataTable.Remove(ri.Row)` (the `BindingList` raises `ListChanged`; no `DeleteRow`, no handle). Then purge the stale caches: `bTTKTDataTable.RemoveAll(b => b.BangThuTienId == ri.BangThuTienId)` and `prevMonthRowDictionary.Remove(ri.Row.HocSinhId)`, both null-guarded. On failure: rollback is automatic on dispose-without-commit; log + `XtraMessageBox.Show`, grid untouched.

**2.5 Make Cancel actually discard — load the view untracked.** `onCanceling` ([FrmSoThuTien.cs:100-104](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:100)) only re-searches, and the reload uses `ExecuteFunction` with default `MergeOption.AppendOnly`, so already-tracked instances keep their dirty values and stay Modified in the form's long-lived context — discarded edits reappear and the next save commits them.
→ In `loadViewBangThuTiens`, call the existing overload `getViewBangThuTienByNgayTinhAndLop(ngayTinh, lop, MergeOption.NoTracking)` ([QLMNModel.Context.cs:851](QLMamNon/Dao/QLMNModel.Context.cs:851); `using System.Data.Entity.Core.Objects;`).
Why `NoTracking` over `MergeOption.OverwriteChanges`: `OverwriteChanges` would also fix Cancel, but leaves every row tracked in a set that *cannot be saved*, re-arming the `UpdateException` on any stray mutation. `NoTracking` makes the view what it actually is — a read model. Detaching is the worst option (`Detached → Added` ⇒ INSERT into a view).
**Ordering: 2.1 must land before 2.5.** Keep `onCanceling` as re-search + `CanceledCaption`, and do **not** call `base.onCanceling()` — it casts `GridControl.DataSource` to `BindingSource` and calls `Entry(row).Reload()` on untracked rows. Add a comment saying why; [FrmPhieuChi.cs:33-38](QLMamNon/Forms/ThuChi/FrmPhieuChi.cs:33) is the repo's existing precedent documenting this same `AppendOnly` trap.

**2.6 Surface failures.** Base `onSaving` swallows every exception ([CRUDForm.cs:245-248](QLMamNon/Forms/CRUDForm.cs:245)) and sets `SavedCaption` **outside** the try (`:250`), so "Đã lưu" shows when nothing saved.
- `Constant/StatusCaptions.cs` has no error member → add `SaveFailedCaption = "Lưu không thành công"` (additive, no existing call site affected).
- Override `onSavingError` in FrmSoThuTien: `LoggerFacade.Error(ex, AppForms.FormSoThuTien)` **and** a second `LoggerFacade.Error(string.Format(...ngayTinh/lop...), AppForms.FormSoThuTien)` (the `Exception` overload drops the module); set `SaveFailedCaption`; `XtraMessageBox.Show(..., MessageBoxIcon.Error)`; then `base.onSavingError(ex)` to keep the `DBConcurrencyException → reloadDataTable()` path. Call shape matches [USB3GSms.cs:170](QLMamNon/Service/SMS/USB3GSms.cs:170).
- Override `reloadDataTable()` to call `btnTimKiem_Click(null, null)` so that inherited branch stops being a no-op.
- **Do not move `CRUDForm.cs:250` inside the try** — 15+ subclasses reach that line with no test coverage ([FrmUser.cs:31-50](QLMamNon/Forms/SystemSetting/FrmUser.cs:31) has the identical bug). The `return` in our catch already prevents `SavedCaption` here. File the base-class fix as a separate task.

---

## Phase 3 — Performance

**3.1 Kill the per-row DbContext.** `BangThuTienUtil.CalculateSummaryFields` ([BangThuTienUtil.cs:56-73](QLMamNon/Utils/BangThuTienUtil.cs:56)) creates a **new, never-disposed** `qlmamnonEntities` plus a `getBangThuTienGenHistoryByLopAndNgay` round trip **per row** — once per row in the load loop ([FrmSoThuTien.cs:553](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:553)) and once per cell edit (`:220`).
→ Its only caller is `EvaluateValuesForViewBangThuTienRow` ([BangThuTienUtil.cs:87](QLMamNon/Utils/BangThuTienUtil.cs:87)). Add an **8-arg overload** of `EvaluateValuesForViewBangThuTienRow` taking a gen-history cache, and keep the 7-arg signature delegating with `null` (today's behaviour) so nothing else breaks. Cache = small new `Utils/BangThuTienGenHistoryCache.cs` wrapping `Dictionary<string, bangthutiengenhistory>` keyed `LopId + "|" + NgayTinh.Ticks`, holding (not owning) a `qlmamnonEntities`, lazily calling the proc with `MergeOption.NoTracking` on a miss. Keep `CalculateSummaryFields(viewbangthutien)` as a one-line delegate to `(row, null)`.
→ Convert in this pass: FrmSoThuTien `:553` (cache per `loadViewBangThuTiens`) and `:220` (form-level cache field built at load), plus the two hottest service sites [SoThuTienService.cs:510](QLMamNon/Service/Data/SoThuTienService.cs:510) and `:576` (they already share one `entities`). The other five callers — `FrmBaoCaoThuHocPhiTheoThang.cs:84`, `FrmBaoCaoTinhHinhThuChi.cs:149`, `FrmDanhSachChuaNopHocPhi.cs:76`, `FrmTaoPhieuThu.Service.cs:32` and `:73` — stay on the 7-arg overload.

**3.2 `EvaluatePrevMonthViewBangThuTienTable`** ([SoThuTienService.cs:556](QLMamNon/Service/Data/SoThuTienService.cs:556)) ignores its `lop` argument (`:560` passes null), so every search evaluates the **whole school** for the previous month.
→ **Do not "fix" it by passing `lop` through** — see Known non-bugs below. Optimize semantics-free: the 3.1 cache + `MergeOption.NoTracking` on its two fetches, which removes the 2N round trips and stops the whole-school pull polluting the caller's change tracker. All five call sites (`FrmBaoCaoThuHocPhiTheoThang.cs:73`, `FrmBaoCaoTinhHinhThuChi.cs:144`, `FrmDanhSachChuaNopHocPhi.cs:65`, `FrmSoThuTien.cs:536`, `SoThuTienService.cs:504`) are unaffected.

**3.3 Freeze the grid during the load loop.** Mirror [FrmPhieuChi.cs:86-100](QLMamNon/Forms/ThuChi/FrmPhieuChi.cs:86): `ShowGridLoadingPanel()` + `gvMain.BeginUpdate()` in a `try`, `EndUpdate()` + `HideGridLoadingPanel()` in `finally`. Both helpers already exist on `CRUDForm` ([CRUDForm.cs:340-348](QLMamNon/Forms/CRUDForm.cs:340)) and are used in [GenerateSoThuTien.cs:114](QLMamNon/Forms/ThuChi/FrmSoThuTien.GenerateSoThuTien.cs:114).

**3.4 Footer totals go stale after a programmatic recalc.** `ShowFooter = true` with summaries on ~20 columns including the recomputed `TienAnSua`/`KhoanThuChinh`/`ThanhTien`/`PhucVuBanTru`. One line at the end of the `gvMain_CellValueChanged` recalc block: `gv.UpdateTotalSummary();`. Do **not** add `RefreshRow` or implement `INotifyPropertyChanged` on the generated DAO — same-row repaint already works (zero `RefreshRow` calls exist anywhere in the repo).

**3.5 `onSaving` O(N²)** — covered by 1.4.

**3.6 Dispose the leaked context** at [GenerateSoThuTien.cs:144-146](QLMamNon/Forms/ThuChi/FrmSoThuTien.GenerateSoThuTien.cs:144) with a `using`.

---

## Phase 4 — Hygiene

**4.1 `loadLopData` mutates a process-wide shared cache.** `StaticDataFacade.Get(StaticDataKeys.LopHoc)` hands the **same list instance** to every caller, and [FrmSoThuTien.cs:497-504](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:497) stamps `row.KhoiId` on each cached `lop` — one `getLopKhoiByLopId` query per class, in the constructor. **FrmSoThuTien never reads `lop.KhoiId`** (`cmbLop` uses `DisplayMember="Name"`, `ValueMember="LopId"`); the loop is dead copy-paste from [FrmLopHoc.cs:44-58](QLMamNon/Forms/DanhMuc/FrmLopHoc.cs:44), which legitimately owns its own non-cached list and persists `KhoiId` itself.
→ Delete the loop; `loadLopData` becomes a null-guarded cast + assignment. Delete the redundant re-bind of the same instance at [FrmSoThuTien.cs:94](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:94). None of the 12 other `StaticDataKeys.LopHoc` consumers read `KhoiId` (only `FrmKhoiHoc`/`FrmLopHoc` do, both on their own lists) → safe.

**4.2 Per-keystroke allocation.** Hoist `fieldsToEvaluate` ([FrmSoThuTien.cs:187-203](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:187)) to `private static readonly HashSet<string>` beside `HidePhuPhiColumns`.

**4.3 Dead code.** Delete `isNeedToUpdateBangThuTienKhoanThu` ([FrmSoThuTien.cs:465-491](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:465)) — a commented-out body plus `return true`; drop its call at `:326` and build the id list unconditionally.

**4.4 Field-name constants.** `HidePhuPhiColumns` ([FrmSoThuTien.cs:52-71](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs:52)) uses raw literals → use `ViewBangThuTienFieldName` where a constant exists, add the few missing ones. `ViewBangThuTienFieldName` has duplicates `TienAnVaSua` (`:19`) and `TienAnSua` (`:23`), both `"TienAnSua"`; the only two uses of `TienAnVaSua` are FrmSoThuTien `:197` and the `:468` comment block deleted in 4.3 → delete `TienAnVaSua`, point `:197` at `TienAnSua`.

**4.5 Correct CLAUDE.md's build/test commands** (no `src/`, no test project, MSBuild required).

---

## Known non-bugs / deliberately out of scope

- **Do not scope the prev-month query to `lop`.** `prevMonthRowDictionary` is keyed by `HocSinhId` and feeds `CalculateTruyThu` ([BangThuTienUtil.cs:41-54](QLMamNon/Utils/BangThuTienUtil.cs:41)). Narrowing to the current class would silently zero the carried-over debt of every student who changed class between months. The whole-school pull is plausibly deliberate; if narrowing is ever wanted, filter by the current month's `HocSinhId` set instead.
- `hit.Band == gridBandHoTen` is a reference compare, so a null `Band` is already safe — no guard needed.
- `generateSoThuTiens` calls `XtraMessageBox.Show` from `BackgroundWorker.DoWork` ([GenerateSoThuTien.cs:150](QLMamNon/Forms/ThuChi/FrmSoThuTien.GenerateSoThuTien.cs:150)) — a real cross-thread UI violation, but a separate task.
- `FrmThongTinHocTap` (`CRUDForm<viewhoctap>`, also a `DefiningQuery` set) has the same read-only-save defect — its Save is a silent no-op. Separate task.
- Moving `CRUDForm.cs:250` inside the try, and `FrmTaoPhieuThu`'s identical undisposed-context leak — separate tasks.

---

## Files to modify

| File | Phases |
|---|---|
| [Forms/ThuChi/FrmSoThuTien.cs](QLMamNon/Forms/ThuChi/FrmSoThuTien.cs) | all — primary target |
| [Utils/BangThuTienUtil.cs](QLMamNon/Utils/BangThuTienUtil.cs) | 1.3, 3.1 |
| [Components/Command/QLMNDao/UpdateSoTienTruyThuOfBangThuTienCommand.cs](QLMamNon/Components/Command/QLMNDao/UpdateSoTienTruyThuOfBangThuTienCommand.cs) | 2.2 |
| [Service/Data/SoThuTienService.cs](QLMamNon/Service/Data/SoThuTienService.cs) | 3.1, 3.2 |
| [Constant/StatusCaptions.cs](QLMamNon/Constant/StatusCaptions.cs) | 2.6 |
| [Dao/Constant/ViewBangThuTienFieldName.cs](QLMamNon/Dao/Constant/ViewBangThuTienFieldName.cs) | 4.4 |
| `Utils/BangThuTienGenHistoryCache.cs` (new) | 3.1 |
| [Forms/ThuChi/FrmSoThuTien.GenerateSoThuTien.cs](QLMamNon/Forms/ThuChi/FrmSoThuTien.GenerateSoThuTien.cs) | 3.6 |

Ordering: Phase 1 items are independent → land first. **2.6 before 2.1; 2.1 before 2.5.** 3.1's overload before 3.2. 4.1 is the smallest safe win, do it early.

---

## Verification

No test project exists — manual only. Build:

```bash
MSBuild.exe D:\Projects\Local\.Net\QLMamNon\QLMamNon.sln /t:Build /p:Configuration=Debug /v:m
```

Expect `0 Error(s)`, then run `QLMamNon\bin\Debug\QLMamNon.exe` against a **restored copy** of `DB/qlmamnon.sql` — the truy-thu proc writes to future months, so never test on live data.

| # | Covers | Steps | Pass |
|---|---|---|---|
| V1 | 1.1 | Open Sổ Thu Tiền, click **Lưu** before searching | No exception dialog |
| V2 | 1.8 | Type `abc` in Tháng → **Tìm Kiếm**; clear Năm → **Tìm Kiếm** | "Xin vui lòng chọn năm tháng…" both times, no crash |
| V3 | 1.2 | Search, clear the `SX Tháng Trước` cell, **Lưu** | No `InvalidOperationException`; recalcs as 0 |
| V4 | 1.3 | `DELETE FROM lop_khoi WHERE LopId=<test>`; search that class, edit `SX Tháng Trước` | No NRE; row keeps its khoản-thu values. Restore the row after |
| V5 | **headline** | Search, change `SX Tháng Trước` 2 → 5, **Lưu**, **Tìm Kiếm** | Shows 5; `SELECT SXThangTruoc FROM bangthutien WHERE BangThuTienId=…` = 5. Pre-fix: crash or silent revert |
| V6 | 1.5 | `DELETE FROM bangthutien_khoanthu WHERE BangThuTienId=<id> AND KhoanThuId=<TienSua>`; search, set `Tiền Sữa`=123456, **Lưu**, **Tìm Kiếm** | 123456 in grid **and** in `bangthutien_khoanthu`. Pre-fix: 0 |
| V7 | 1.4 | Search, delete one `bangthutien` row from another DB session, **Lưu** | No `IndexOutOfRangeException`; orphan skipped |
| V8 | 2.1/2.2/2.6 | Edit 3 rows; from another session `LOCK TABLES bangthutien_khoanthu WRITE;`, **Lưu**, then `UNLOCK TABLES` | One error box, caption **"Lưu không thành công"** (never "Đã lưu"); `SoTienTruyThu` for `NgayTinh > ngayTinh` unchanged |
| V9 | 2.6 | Repeat V8, watch the log sink (DebugView or VS Output) | Error entry with the exception **and** one with `ngayTinh`/`lop` |
| V10 | 2.5 | Search, change `Số Tiền Năng Khiếu` 0 → 99999, **Huỷ Bỏ**, **Tìm Kiếm** | Cell shows 0. Pre-fix: 99999, and the next Lưu commits it |
| V11 | 2.5 | V10, then switch class, search, switch back, search | Still 0 everywhere |
| V12 | 1.6 | Right-click the footer, then the band header area, then empty space below the last row | Menu opens with a **disabled** "Xóa dòng này", no NRE |
| V13 | 2.4 | Sort `Họ Tên` descending, right-click row 1, re-sort ascending, **then** click "Xóa dòng này" | Deletes the row you right-clicked |
| V14 | 2.4 | `LOCK TABLES bangthutien WRITE;` from another session, then delete a row → Yes | Error box; **row still in grid**; both DB deletes rolled back |
| V15 | 2.4 | After a successful delete, edit `SX Tháng Trước` on another row | No stale-cache miscalculation, no NRE |
| V16 | 1.9 | Search class A/month M (has data). Then class B/month M with no sổ generated; **Cancel** the generate dialog. Click **In** | Blocked with "Danh sách sổ thu tiền trống" or prints class B — **never** class A |
| V17 | 3.1/3.3 | Search a class with ≥40 students | Loading panel, no row-by-row flicker, measurably faster |
| V18 | 4.1 | Open Sổ Thu Tiền, then Thông Tin Học Sinh / Xếp Lớp; inspect the Lớp dropdowns | Unchanged vs a session that never opened Sổ Thu Tiền |
| V19 | 1.10 | With `IsMNHongMinh=false`: search, unhide a Phụ Phí column, focus another control, click back into the grid | Layout **not** re-applied; your column stays visible |
| V20 | 1.11 | Right-click the `Họ Tên` band header | Menu opens, sort order unchanged; left-click still toggles |
| V21 | 3.4 | Search, edit `SX Tháng Trước` on one row | Row's `Số Tiền SX`/`Khoản Thu Chính`/`Thành Tiền` **and** the footer totals update in the same gesture |
| V22 | 2.3 | Search, edit, **Lưu**, then **Lưu** again without touching anything | `SoTienTruyThu` delta applied **once** |
| V23 | regression | **In**, **In Trang 1**, **In Trang 2**, **In Giấy Báo Nộp Tiền**, **Tạo Sổ Thu Tiền**, and the STT move buttons (⏮ ◀ ▶ ⏭) | All behave as before |
