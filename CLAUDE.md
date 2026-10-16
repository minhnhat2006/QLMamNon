# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

`QLMamNon` — Vietnamese kindergarten management desktop app (students, teachers, assets, fee collection
"thu/chi", SMS, printed reports). WinForms on .NET Framework 4.6.2, DevExpress 17.1 UI/reporting,
Entity Framework 6 over MySQL. UI text, table and column names are Vietnamese; keep that vocabulary.

Single real deployment: **Trường MN Hồng Minh** (Sơn Trà, Đà Nẵng), with a second brand
**Trung tâm Anh ngữ DOMYTY** selected by the `IsMNHongMinh` setting in `app.config` (it switches the
school name/address block printed in report headers).

## Build & run

Legacy non-SDK project (`packages.config`, `ToolsVersion 12.0`), so **the `dotnet` CLI cannot build, run
or test it** — use MSBuild from a Visual Studio install. There is no `src/` directory and no test project.

- **Build**: `"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" QLMamNon.sln /t:Build /p:Configuration=Debug`
  `Setup\Setup.vdproj` is not MSBuild-buildable and always reports warning MSB4078 — expected, not a failure.
- **Restore** (only if `packages\` is missing): `MSBuild.exe /t:Restore /p:RestorePackagesConfig=true`
  — note `packages/` *is* committed, so normally there is nothing to restore.
- Building also requires a **licensed DevExpress WinForms v17.1 install on the machine**; the
  `DevExpress.*` assemblies are machine references, not NuGet packages.
- Debug builds `AnyCPU`; Release builds **x86**.
- **Run**: `QLMamNon\bin\Debug\QLMamNon.exe` — needs a reachable MySQL server; connection strings live in
  `QLMamNon\app.config` (`qlmamnonEntities`, EF metadata + `MySql.Data.MySqlClient`).
- **Tests**: none exist. Verification is manual against a restored copy of
  `DB\qlmn_backup_202610121440249028.sql` (MySqlBackup dump of 12/10/2026, MySQL 5.7) — never against
  live data. Restore it into a separate database (e.g. `CREATE DATABASE qlmamnon_test CHARACTER SET utf8;`
  then `mysql -u root -p qlmamnon_test < DB\qlmn_backup_202610121440249028.sql`) and point both
  connection strings at it while testing. State plainly when a change was not verified by running the app.

## Architecture

Six C# projects in `QLMamNon.sln` plus the `Setup` installer:

- `QLMamNon` — the application: Forms, Reports, Service, Dao, Facade, Utils, Workflow, UserControls.
- `ACG.Core.WinForm.*` — in-house infrastructure consumed through the Facade layer:
  `Mediator` (loose coupling between modules), `Logging` (log4net), `Data.Static` (`IStaticDataMap`
  in-memory cache of lookup tables), `ActivityTracking` (per-form user activity), `Util`
  (`StringUtil`, `ListUtil`, `DateTimeUtil`, `IntUtil`, `ObjectUtil`, `ArrayUtil`, `ControlUtil`,
  `PasswordUtil` — prefer these over writing new helpers).

Layering, outermost first:

1. **Forms** (`QLMamNon/Forms/<Module>/`) — one folder per domain area (`ThuChi`, `HocSinh`, `GiaoVien`,
   `DanhMuc`, `TaiSan`, `TinNhan`, `SystemSetting`, `Authenticate`, plus `Helper`). There is no
   `BaoCao` folder: the `FrmBaoCao*` / `FrmBangKe*` report screens live under `ThuChi`. `FrmMain`
   hosts them; `FormMainFacade` tracks open forms by `FormKey`, `Constant/AppForms.cs` +
   `FormPrivilegeConstant.cs` drive menu wiring and per-role access.
2. **`Forms/CRUDForm.cs`** — the base every grid form derives from. `CRUDForm<T>` owns
   `BindingList<T> DataTable`, a per-form `qlmamnonEntities` (`Entities`), `TablePrimaryKey`,
   and wires Add/Edit/Delete/Save/Cancel buttons and DevExpress `GridView` events in `InitForm` /
   `InitEvents`. Derived forms override the `onSaving`-style hooks rather than re-handling clicks.
   Large forms are split into partial files by concern (e.g. `FrmSoThuTien.cs`,
   `.MoveRow.cs`, `.GenerateSoThuTien.cs`).
3. **Facade** (`QLMamNon/Facade/`) — the only place infrastructure is reached:
   `StaticDataFacade` (lookup cache + `GetQLMNEntities()` factory), `ModuleMediatorFacade`
   (`WorkFlowActions` messages into `Workflow/Handler`), `LoggerFacade`, `MySqlFacade` (backup/restore),
   `GDriveFacade` (Google Drive backup), `ActivityTrackerFacade`.
4. **Service** (`QLMamNon/Service/Data/`) — data operations above EF: `SoThuTienService`,
   `PhieuThuService`, `PhieuChiService`, `GiaoVienService`, `AuthenService`, over `BaseDataService`.
   `Service/SMS/` abstracts SMS providers behind `ISMS` (`SpeedSMS`, `ESms`, `USB3GSms`).
5. **Dao** (`QLMamNon/Dao/`) — `QLMNModel.edmx` generated model (`qlmamnonEntities`) plus T4-generated
   entity classes.
6. **Utils** (`QLMamNon/Utils/`) — fee-calculation domain logic (`BangThuTienUtil`,
   `BangThuTienGenHistoryCache`, `ThongTinHocSinhUtil`, `StaticDataUtil`, `VNCurrency`). Shared across
   forms: check callers before changing a signature.
7. **Reports** (`QLMamNon/Reports/`) — DevExpress XtraReports (`Rpt*.cs` + `.designer.cs`).

### Startup and navigation

`Program.Main` → `FrmMain` (DevExpress `RibbonForm`, MDI parent). `FrmMain_Load` wires the mediator
(`FormChannelManager`), the workflow (`FrmMainFlow`), `FormMainFacade.InitFormMain(this)`, then
`initStaticData()` registers every lookup list and a shared `qlmamnonEntities` under the string keys in
`Components/Data.Static/StaticDataKeys.cs`.

Ribbon buttons call `FormMainFacade.ShowForm(AppForms.FormXxx)` or `ShowDialog(...)`. Form keys are
string constants in `Constant/AppForms.cs`; `Forms/Helper/FormFactory.cs` maps each key to a `Type` via
`Activator.CreateInstance` and **caches one instance per key** (recreated only after dispose). Adding a
screen therefore means: new `Frm*` in the right `Forms/<Module>/` folder → constant in `AppForms` →
entry in `FormFactory` → ribbon button in `FrmMain` → `<Compile Include>` entries in `QLMamNon.csproj`.

**Authorization**: `FormPrivilegeConstant.FormKeyToPrivilegeId` maps *some* form keys to privilege ids
(`privilege` / `user_privilege` tables). `FormMainFacade.checkPrivilegeToAccessForm` consults
`AuthenService`: a form **not** in that dictionary is open to everyone; a mapped form requires the
privilege, and an unauthenticated user gets `FrmLogin` as a dialog. The authenticated user lives in
`StaticDataFacade` under `StaticDataKeys.AuthenticatedData` (`Entity/AuthenticatedEntity`).

**Mediator**: `ModuleMediatorFacade.Invoke(this, WorkFlowActions.X, payload)`, channels declared in
`Workflow/WorkFlowActions.cs`, subscribers registered in
`Components/ModuleMediator/Channel/FormChannelManager.cs`. Only two actions exist today:
`InitFormTracker` and `AdjustPopupEditForm`.

**Report screens** are plain `XtraForm`s (not `CRUDForm`): gather filter values → call a
`Service/Data` method or EF stored procedure → build a `List<…Item>` of DTOs from `Entity/Form/` →
assign to the report's `BindingSource.DataSource` → `FormMainFacade.ShowReport(rpt)`.

### Data-access constraints that are easy to get wrong

- **View entity sets are read-only.** `viewbangthutien` is mapped with a `<DefiningQuery>` and no
  `<UpdateFunction>` (`Dao/QLMNModel.edmx`), and the repo has zero `ModificationFunctionMapping`.
  `SaveChanges()` on a Modified view instance throws `UpdateException`. Rows returned by
  `ExecuteFunction` stubs are tracked (`MergeOption.AppendOnly`), so this is a live hazard: write
  through the real table entity (`entities.bangthutiens`, see `Service/Data/SoThuTienService.cs`).
- Each `CRUDForm` holds its own `DbContext` from `StaticDataFacade.GetQLMNEntities()`; don't share
  entity instances across contexts. `FrmMain` holds yet another context for static data, so there is
  no single shared change-tracker across screens.
- `Program.cs` forces culture `vi-VN` with `.` thousands / `,` decimal separators and a Times New Roman
  DevExpress default font — number and date parsing depends on it.
- **The generated model is off-limits.** `QLMNModel.cs`, `QLMNModel.Context.cs` and the per-table
  `Dao/*.cs` files come from the `*.tt` T4 templates — never hand-edit. Schema changes go: MySQL →
  "Update Model from Database" in the EDMX designer → regenerate.
- **Add computed/display members only in `Dao/<table>.Partial.cs`**, marked `[NotMapped]`
  (`viewbangthutien.Partial.cs` adds `ThanhTien`, `HoTen`, …; `phieuthu.Partial.cs` /
  `phieuchi.Partial.cs` add `HocSinh`, `PhanLoaiChi` and a `PaymentTypeEnum` wrapper over the
  `PaymentType` string column).
- **The context is stored-procedure heavy**: ~35 `DbSet`s plus ~50 function imports
  (`getViewBangThuTienByNgayTinhAndLop`, `getPhieuThuForSoQuyTienMat`,
  `getPhieuChiByDateRangeWithGroupPhanLoaiChi`, `insertHocSinhToLop`, `deleteUserPrivileges`, …).
  Many procedures build SQL with `CONCAT` + `PREPARE`, so **changing report filtering often means
  editing a stored procedure, not C#**.
- Schema/procedure changes ship as dated migration scripts:
  **`QLMamNon/DB/scripts/<yyyyMMdd>/script.sql`** (latest `20251027`, `20260602`). Follow that pattern
  and keep the procedure bodies in sync with the EDMX.
- Money columns are `bigint` in MySQL → `long` in C#; some report DTOs use `double`/`decimal`.
  `Utils/VNCurrency.cs` renders amounts as Vietnamese words for printed vouchers.
- `Dao/New folder/` is a **stale duplicate** of the generated entity classes and is not in the csproj — ignore it.

## Domain glossary

| Vietnamese | Meaning |
|---|---|
| `hocsinh` | student |
| `lop` / `khoi` / `truong` | class / grade-block / school |
| `hocsinh_lop`, `lop_khoi`, `khoi_truong` | join tables with `DateCreated` / `DateLeft` / `DateDeleted` (history-aware) |
| `giaovien` | teacher (largely dormant — `GiaoVienService`'s body is commented out) |
| `khoanthu` | a fee item (tuition, breakfast, dinner, milk, …) |
| `khoanthuhangnam` | the per-year/per-grade amount of a fee item |
| `bangtinhphi` | fee calculation table/rules |
| `bangthutien` | the monthly fee sheet row for one student (`NgayTinh` = the month it is computed for) |
| `bangthutien_khoanthu` | per-fee-item lines of a `bangthutien` row |
| `viewbangthutien` | DB view joining student + class + fee sheet — **the main grid/report source** |
| `phieuthu` / `phieuchi` | receipt (money in) / payment voucher (money out) |
| `phanloaithu` / `phanloaichi` | receipt category / payment category |
| `sothutien` | the fee-collection ledger screen (`FrmSoThuTien`) |
| `bangke…` / `baocao…` | listing / report |
| `soquytienmat` | cash book |
| `taisan`, `bangiaotaisan` | asset, asset hand-over |
| `truythu` | arrears carried from the previous month |
| `SX` / `AnSang` / `AnToi` | meal portions (main/lunch) / breakfast / dinner |
| `MaPhieu`, `SoTien`, `Ngay`, `GhiChu`, `NgayTinh`, `STT` | voucher no., amount, date, note, calculation date, sequence no. |

## Modules (`QLMamNon/Forms/…`)

- **Authenticate** — `FrmLogin`.
- **DanhMuc** (master data) — `FrmTruongHoc`, `FrmKhoiHoc`, `FrmLopHoc`, `FrmTinhThanhPho`,
  `FrmQuanHuyen`, `FrmPhuongXa`, `FrmKhoanThu`, `FrmKhoanThuHangNam`, `FrmBangTinhPhi`,
  `FrmPhanLoaiThu`, `FrmPhanLoaiChi`.
- **HocSinh** — `FrmThongTinHocSinh` (student records), `FrmXepLop` (class assignment),
  `FrmThongTinHocTap`, `FrmThongTinCanDo`.
- **ThuChi** (the heart of the app) — `FrmSoThuTien` (+ partials `.GenerateSoThuTien`, `.MoveRow`,
  `.Report`), `FrmTaoPhieuThu` / `FrmPhieuThu`, `FrmTaoPhieuChi` / `FrmPhieuChi`, `FrmSoQuyTienMat`,
  `FrmSoQuyTienMatTheoNgay`, `FrmBangKeThuHocPhi`, `FrmBangKeThuTrongNgay`, `FrmBangKeThuTheoLop`,
  `FrmBangKeCacLoaiChi`, `FrmDanhSachChuaNopHocPhi`, `FrmBaoCaoThuHocPhiTheoThang`,
  `FrmBaoCaoSoLuongHocSinhTheoLop`, `FrmBaoCaoSiSoHocSinhTheoLop`, `FrmBaoCaoHoatDongTaiChinh`,
  `FrmBaoCaoChiTietHoatDongTaiChinh`, `FrmBaoCaoTinhHinhThuChi`.
- **TaiSan** — `FrmTaiSan`, `FrmPhanBoTaiSan`, `FrmSoTheoDoiTaiSan`.
- **TinNhan** — `FrmTinNhanPhuHuynh`: SMS to parents with `{Ten_HS}` / `{Ten_Cha}` / `{Ten_Me}`
  placeholders, templates in `smstemplate`, sent via `Service/SMS` (`SpeedSMS` / `ESms` HTTP APIs, or
  `USB3GSms`, which writes AT+PDU commands to a serial port).
- **SystemSetting** — `FrmUser` (users + privileges), `FrmPreference` (user settings), `FrmBackupDb`
  (MySqlBackup dump → upload to Google Drive via `GDriveFacade` → delete the temp file).

## Fee-calculation core (the most intricate logic)

- `Service/Data/SoThuTienService.cs` generates the monthly fee sheet
  (`GenerateSoThuTienByHocSinhRows` → `GenerateSoThuTienByHocSinhAndLopAndNgayTinh` →
  `generateBangThuTienKhoanThu`), carries the previous month forward
  (`EvaluatePrevMonthViewBangThuTienTable`, `getAndSortPrevMonthViewBangThuTienRows`) and builds the
  aggregates for receipts/reports (`GetBangKeTongHopThuTien`, `GetBangKeTongHopThuTienForTaoPhieuThu`,
  `GetDanhSachTreChuaNopHocPhiByMonth`).
- `Utils/BangThuTienUtil.cs` holds the arithmetic: portions → money (`SXToSoTienSX`,
  `SXAnSangToSoTienAnSang`, `SXAnToiToSoTienAnToi`), `CalculateThanhTien`, `CalculateTruyThu`
  (arrears), `CalculateSummaryFields`, `EvaluateValuesForViewBangThuTienRow`, `CalculateSoTienPhi`
  (pro-rating by absent days), `RecalculateBangThuTienKhoanThuList`. `CalculateSummaryFields` and
  `EvaluateValuesForViewBangThuTienRow` each have an overload taking a `BangThuTienGenHistoryCache`
  — use it when evaluating a whole grid, so the gen-history lookup is not re-queried per row.
- Fixed fee-item ids live in `Constant/BangThuTienConstant.cs` (1 TienAnSua, 2 PhuPhi, 3 BanTru,
  4 HocPhi, 5 AnSang, 6 AnToi, 7 TienSua) — **hard-coded, and must match the `khoanthu` table rows**.
- Per-portion prices come from *user settings* (`TienAnSang`, `TienAnChinh`, `TienAnToi`), not the DB.
- `PaymentType` (`CASH` / `TRANSFER`) was added to `phieuthu` (script `20251027`) and `phieuchi`
  (script `20260602`); reports split cash vs. transfer columns (`SoTienNop` / `SoTienChuyenKhoan`).
  Read it through `PaymentTypeEnum`, never by string comparison.

## Settings (`QLMamNon/app.config`)

- `connectionStrings`: `QLMamNon.Properties.Settings.qlmamnonConnectionString1` (plain MySQL, used by
  backup) and `qlmamnonEntities` (EF metadata + provider connection string). **Both must point at the
  same database**; the DB name is versioned by date (currently `qlmamnon_20250919`).
- `applicationSettings` (fixed per build): `AppLannchDate` *(sic — the typo is the real key, mirrored in
  `ConfigConstant.SettingKeyAppLaunchDate`)*, `DefaultMaPhanLoaiChi`, the SMS endpoints/credentials,
  `IsMNHongMinh` and the two school name/address blocks used in report headers.
- `userSettings` (writable at runtime via `FrmPreference`): `TienAnSang`, `TienAnChinh`, `TienAnToi`,
  `SoTienTonDauKy`, `StartYearForDropDown`, `NamSinhStart`, `PhanLoaiThuByHocSinh`,
  `ShowGiayBaoNopTien*`, `POSPrinterName`, `LastMaPhieuThu`.
- Typed access is `QLMamNon.Properties.Settings.Default.<Name>`.

## Code conventions

- **Language level**: no `<LangVersion>` is set, and the codebase stays pre-C# 6 apart from string
  interpolation. Do **not** introduce `?.`, `??`, `nameof`, or expression-bodied members. Write
  `x == null ? 0 : x.Value`, explicit `if (x != null)`, `ListUtil.IsEmpty(list)`.
- **Nullables**: `Nullable<T>.Value` on a grid-bound column is a crash waiting to happen — cells can be
  cleared and columns are NULL-able. Always coalesce.
- **Designer files**: keep `InitializeComponent()` in `.Designer.cs` machine-generated. New wiring,
  layout fixes and null guards go in the code-behind constructor or `*_Load`, not in the Designer. Never
  hand-edit `.resx`.
- **Grid events**: guard non-data rows (`e.RowHandle < 0` covers InvalidRowHandle, NewItemRow, group and
  filter rows) and null `GetRow(...)` results before use. Prefer field-name constants
  (`ViewBangThuTienFieldName.*`, `this.TablePrimaryKey`) over string literals and column indexes.
- **UI threading**: wrap non-UI-thread updates in `this.Invoke(new Action(() => { ... }))`, or use
  `async/await` with `Progress<T>`.
- **Errors**: there is no `Application.ThreadException` handler, so an unhandled exception is a crash
  dialog. Report failures with a `MessageBox` plus `LoggerFacade` logging.
- **Disposal**: `using` / `.Dispose()` for `Graphics`, `Pen`, `Brush`, modal dialogs, reports.
- **Naming**: PascalCase controls (`ButtonSubmit`, `TxtCustomerName`); Vietnamese domain names stay as-is.
  Existing DevExpress controls use prefixed names (`btnLuu`, `gvMain`, `gcMain`, `cmbLop`, `dateTuNgay`,
  `bbi`/`bsi` for ribbon items) — match the file you are editing.
- **File-local style**: private helpers are `camelCase` (unusual for C#, but consistent here), public
  members `PascalCase`, `this.` prefix on instance members, no `_` field prefixes, and
  `#region Properties / Events / Helper` blocks. Follow the surrounding file.
- UI strings, message boxes and comments are **Vietnamese** — keep it that way.
- Status-bar text comes from `Constant/StatusCaptions.cs` via `FormMainFacade.SetStatusCaption`.

## Repo notes

- Plans and design notes for in-flight work live in `.claude/plans/` (configured via
  `plansDirectory` in `.claude/settings.json`), not in the global Claude folder.
- `QLMamNon/client_secret.json` holds Google Drive credentials — do not read, print or commit changes to
  it (`.claude/settings.json` denies reading it). The MySQL password and the SMS provider keys are
  likewise committed in `QLMamNon/app.config`: don't copy them into new files, logs, commit messages or
  anything published, and if they are ever rotated they should move out of source control.
- `DB/qlmn_backup_202610121440249028.sql` is the current full schema+data dump and the reference for
  schema, views and stored procedures. It contains **real student and parent data**: restore it only
  locally, and never paste its rows into chat, logs, commits or anything published. `/DB/*.sql` is in
  `.gitignore` for that reason — never force-add a dump to git.
  `QLMamNon/DB/Dump20190701.sql` and `QLMamNon/DB/qlmamnon.sql` are **outdated** (e.g. their
  `viewbangthutien` still has placeholder `ThanhTien`/`HoTen` columns) — don't use them as a reference.
  `Documents/` holds reference screenshots of the paper forms the reports must match.
- **Many files under `Reports/` are UTF-16LE encoded** (which is why git shows them as `Bin`). Preserve
  the encoding when editing, and expect text tools to need `iconv -f UTF-16LE`. Most other sources are
  UTF-8 with BOM.
- New files must be added to `QLMamNon/QLMamNon.csproj` by hand (old-style project, no globbing),
  including `<DependentUpon>` for designer/resx partners.
- Placeholders that look like bugs but are not: `Service/Data/BaseDataService.cs` is an empty marker
  base class, `GiaoVienService`'s body is entirely commented out, and
  `Workflow/Forms/FrmMainFlow.InitWorkFlow` is a no-op.
- Stale/vendored artifacts to leave alone: `QLMamNon.zip`, `QLMamNon/Backup/*.zip`, `Dao/New folder/`,
  `Libs/*.zip`, `Properties/licenses.licx.bak`, `Forms/ThuChi/FrmSoThuTien.resx.bak`.
- `packages/` **is** committed; `.gitignore` covers `bin/`, `obj/`, `.vs/`, the built `Setup` output and
  assorted `.bak` files.

## Git workflow

Default branch `main`; single author (`minhnhat2006`), small commits with terse imperative subjects
("Update PhieuThu/PhieuChi/Reports", "Add BaoCaoSiSoHocSinhTheoLop"). Recent direction of work:
`PaymentType` (cash vs. bank transfer) threaded through receipts, payment vouchers and reports; new
daily/per-class collection reports; and a round of `FrmSoThuTien` save/cancel/delete and per-row
`DbContext` fixes.
