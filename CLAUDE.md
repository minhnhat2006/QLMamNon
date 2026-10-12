# QLMamNon — Project Context

Desktop app for managing a Vietnamese kindergarten (**Q**uản **L**ý **Mầm Non**): students,
classes, fee calculation, receipts/payments, assets, SMS to parents, and printed reports.
All UI text, column names, and domain vocabulary are Vietnamese. Single real deployment:
**Trường MN Hồng Minh** (Sơn Trà, Đà Nẵng) + a second brand **Trung tâm Anh ngữ DOMYTY**
(toggled by the `IsMNHongMinh` setting in `app.config`).

## Stack

| Item | Value |
|---|---|
| Language / runtime | C# on **.NET Framework 4.6.2** (`WinExe`) |
| UI | WinForms + **DevExpress WinForms v17.1.3** (XtraEditors/XtraGrid/XtraBars/XtraReports) |
| Data access | **Entity Framework 6** database-first EDMX over **MySQL** (`MySql.Data` 9.5, `MySql.Data.EntityFramework`) |
| Reports | DevExpress XtraReports (`QLMamNon/Reports/Rpt*.cs`) |
| Other | MySqlBackup.NET (DB dump), Google.Apis.Drive.v3 (backup upload), log4net/Trace, SSH.NET, SMSPDULib |
| Solution | `QLMamNon.sln` — Visual Studio 2017 format (old-style csproj) |

## Build & run

- **Open `QLMamNon.sln` in Visual Studio (2017+) and build**, or `msbuild QLMamNon.sln /p:Configuration=Debug`.
  `dotnet build` / `dotnet run` do **not** work — these are old-style (non-SDK) csproj files.
- Requires a **licensed DevExpress v17.1 install on the machine** (assemblies are referenced from
  the GAC/DevExpress install path, not NuGet). Without it the build fails on `DevExpress.*` references.
- NuGet packages restore into `/packages` (`packages.config` style).
- Debug builds `AnyCPU`; Release builds **x86**.
- `Setup/Setup.vdproj` is a Visual Studio Installer (MSI) project; it only builds with the
  "Microsoft Visual Studio Installer Projects" extension.
- **There are no automated tests** and no CI. Verification is manual, by running the app.
- A MySQL server must be reachable per the connection string in `QLMamNon/app.config`
  (currently `127.0.0.1`, database `qlmamnon_20250919`).

## Repository layout

```
QLMamNon.sln
QLMamNon/                        ← main WinForms app (everything below is relative to it)
  Program.cs                     ← entry point: forces vi-VN culture + Times New Roman 12, runs FrmMain
  app.config                     ← connection strings + application/user settings (see "Settings")
  Forms/                         ← all screens, grouped by module (see "Modules")
    FrmMain.cs                   ← DevExpress RibbonForm, MDI parent, registers all static data
    CRUDForm.cs                  ← CRUDForm / CRUDForm<T> base class for grid-based screens
    Helper/FormFactory.cs        ← form-key → Type map, caches one instance per form
  UserControls/                  ← UCEditForm* popup editors used by the grids
  Reports/                       ← DevExpress XtraReport classes (Rpt*)
  Dao/                           ← EF6 generated model + partial extensions (see "Data layer")
  Entity/Form/                   ← plain DTOs that feed reports/grids (not EF entities)
  Service/Data/                  ← business logic services (SoThuTien, PhieuThu, PhieuChi, Authen)
  Service/SMS/                   ← ISMS + ESms / SpeedSMS / USB3GSms implementations
  Facade/                        ← static entry points: FormMain, StaticData, MySql, GDrive, Logger, Mediator, ActivityTracker
  Components/                    ← Data.Static (cached lookups), Command, ModuleMediator, ActivityTracker
  Workflow/                      ← mediator channels/handlers wired at startup
  Constant/                      ← AppForms, FormPrivilegeConstant, BangThuTienConstant, DanhMucConstant, …
  Utils/                         ← BangThuTienUtil (fee math), StaticDataUtil (lookups), VNCurrency (amount → Vietnamese words)
  DB/                            ← schema dumps + dated migration scripts under DB/scripts/<yyyyMMdd>/script.sql
ACG.Core.WinForm.*/              ← five tiny in-house helper libraries (see "ACG.Core libraries")
DB/, Documents/, Libs/, Setup/   ← older dumps, screenshots/specs, vendored DLLs, MSI project
```

## Domain glossary (essential for reading this codebase)

| Vietnamese | Meaning |
|---|---|
| `hocsinh` | student |
| `lop` / `khoi` / `truong` | class / grade-block / school |
| `hocsinh_lop`, `lop_khoi`, `khoi_truong` | join tables with `DateCreated` / `DateLeft` / `DateDeleted` (history-aware) |
| `giaovien` | teacher (largely dormant — `GiaoVienService` is entirely commented out) |
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

## Architecture & conventions

**Startup.** `Program.Main` → `FrmMain` (RibbonForm, MDI parent). `FrmMain_Load` wires the mediator
(`FormChannelManager`), the workflow (`FrmMainFlow`), `FormMainFacade.InitFormMain(this)`, then
`initStaticData()` registers every lookup list and a shared `qlmamnonEntities` under the string keys
in `Components/Data.Static/StaticDataKeys.cs`.

**Navigation.** Ribbon buttons call `FormMainFacade.ShowForm(AppForms.FormXxx)` or `ShowDialog(...)`.
Form keys are string constants in `Constant/AppForms.cs`; `Forms/Helper/FormFactory.cs` maps each key
to a `Type` and **caches one instance per key** (recreated only after dispose). So adding a screen means:
new `Frm*` under the right `Forms/<Module>/` folder → constant in `AppForms` → entry in `FormFactory`
→ ribbon button in `FrmMain` → `<Compile Include>` entries in `QLMamNon.csproj`.

**Authorization.** `FormPrivilegeConstant.FormKeyToPrivilegeId` maps *some* form keys to privilege ids
(`privilege` / `user_privilege` tables). `FormMainFacade.checkPrivilegeToAccessForm` consults
`AuthenService`: a form **not** in that dictionary is open to everyone; a mapped form requires the
privilege, and an unauthenticated user gets `FrmLogin` as a dialog. The authenticated user lives in
`StaticDataFacade` under `StaticDataKeys.AuthenticatedData` (`Entity/AuthenticatedEntity`).

**Grid screens.** Derive from `CRUDForm<T>` (`Forms/CRUDForm.cs`). The constructor gets its own
`qlmamnonEntities` via `StaticDataFacade.GetQLMNEntities()` (guarded against design mode), then the
screen calls `InitForm(btnAdd, btnEdit, btnDelete, btnSave, btnCancel, gridView, bindingSource.DataSource)`
which hooks all button/grid events and overridable `onAdding()` / `onEditing()` / … hooks. Set
`FormKey`, `DanhMuc`, `TablePrimaryKey` in the constructor. Status-bar captions come from
`Constant/StatusCaptions.cs` via `FormMainFacade.SetStatusCaption`.

**Report screens.** Report/export forms (`FrmBangKe*`, `FrmBaoCao*`, `FrmSoQuyTienMat*`) are plain
`XtraForm`s: gather filter values → call a `Service/Data` method or EF stored procedure → build a
`List<…Item>` from `Entity/Form/` → assign to the report's `BindingSource.DataSource` →
`FormMainFacade.ShowReport(rpt)`.

**Facades** are static singletons wrapping cross-cutting concerns: `StaticDataFacade` (lookup cache +
`GetQLMNEntities()`), `FormMainFacade` (navigation/captions/report preview), `MySqlFacade` (MySqlBackup
dump), `GDriveFacade` (upload dump to a hard-coded Drive folder), `LoggerFacade` (Trace →
`application.log`, configured in `app.config`), `ModuleMediatorFacade`, `ActivityTrackerFacade`.

**Mediator / workflow.** `ModuleMediatorFacade.Invoke(this, WorkFlowActions.X, payload)` with channels
declared in `Workflow/WorkFlowActions.cs` and subscribers registered in
`Components/ModuleMediator/Channel/FormChannelManager.cs`. Currently only two actions:
`InitFormTracker` (attaches DevExpress control activity logging) and `AdjustPopupEditForm`.

## Data layer

- `Dao/QLMNModel.edmx` is a **database-first EF6 model**; `QLMNModel.cs`, `QLMNModel.Context.cs` and
  the per-table `*.cs` files are **generated by the T4 templates (`*.tt`) — do not hand-edit them**.
  Schema changes go: MySQL → "Update Model from Database" in the EDMX designer → regenerate.
- **Add computed/display members only in `Dao/<table>.Partial.cs`**, marked `[NotMapped]`
  (e.g. `viewbangthutien.Partial.cs` adds `ThanhTien`, `HoTen`, …; `phieuthu.Partial.cs` /
  `phieuchi.Partial.cs` add `HocSinh`, `PhanLoaiChi`, and a `PaymentTypeEnum` wrapper over the
  `PaymentType` string column).
- The context is **stored-procedure heavy**: `qlmamnonEntities` exposes ~35 `DbSet`s plus ~50 function
  imports (`getViewBangThuTienByNgayTinhAndLop`, `getPhieuThuForSoQuyTienMat`,
  `getPhieuChiByDateRangeWithGroupPhanLoaiChi`, `insertHocSinhToLop`, `deleteUserPrivileges`, …).
  Many procedures build SQL with `CONCAT` + `PREPARE` — so **a change to report filtering often means
  editing a stored procedure, not C#**.
- Procedures/views live in the dumps under `QLMamNon/DB/` and `DB/`. Schema changes are shipped as
  dated migration scripts: **`QLMamNon/DB/scripts/<yyyyMMdd>/script.sql`** (latest: `20251027`, `20260602`).
  Follow that pattern for any new DDL/procedure change, and keep the procedure bodies in sync with the EDMX.
- `Dao/New folder/` is a **stale duplicate** of the generated entity classes and is *not* in the csproj — ignore it.
- Money columns are `bigint` in MySQL → `long` in C#; some report DTOs use `double`/`decimal`.
  `Utils/VNCurrency.cs` renders amounts as Vietnamese words for printed vouchers.

## Modules (Forms/…)

- **Authenticate** — `FrmLogin`.
- **DanhMuc** (master data) — `FrmTruongHoc`, `FrmKhoiHoc`, `FrmLopHoc`, `FrmTinhThanhPho`,
  `FrmQuanHuyen`, `FrmPhuongXa`, `FrmKhoanThu`, `FrmKhoanThuHangNam`, `FrmBangTinhPhi`,
  `FrmPhanLoaiThu`, `FrmPhanLoaiChi`.
- **HocSinh** — `FrmThongTinHocSinh` (student records), `FrmXepLop` (class assignment),
  `FrmThongTinHocTap`, `FrmThongTinCanDo`.
- **ThuChi** (the heart of the app) — `FrmSoThuTien` (+ partials `.GenerateSoThuTien`, `.MoveRow`,
  `.Report`), `FrmTaoPhieuThu` / `FrmPhieuThu`, `FrmTaoPhieuChi` / `FrmPhieuChi`,
  `FrmSoQuyTienMat`, `FrmSoQuyTienMatTheoNgay`, `FrmBangKeThuHocPhi`, `FrmBangKeThuTrongNgay`,
  `FrmBangKeThuTheoLop`, `FrmBangKeCacLoaiChi`, `FrmDanhSachChuaNopHocPhi`,
  `FrmBaoCaoThuHocPhiTheoThang`, `FrmBaoCaoSoLuongHocSinhTheoLop`, `FrmBaoCaoSiSoHocSinhTheoLop`,
  `FrmBaoCaoHoatDongTaiChinh`, `FrmBaoCaoChiTietHoatDongTaiChinh`, `FrmBaoCaoTinhHinhThuChi`.
- **TaiSan** — `FrmTaiSan`, `FrmPhanBoTaiSan`, `FrmSoTheoDoiTaiSan`.
- **TinNhan** — `FrmTinNhanPhuHuynh`: SMS to parents with `{Ten_HS}` / `{Ten_Cha}` / `{Ten_Me}`
  placeholders, templates in `smstemplate`, sent via `Service/SMS` (`SpeedSMS` / `ESms` HTTP APIs, or
  `USB3GSms` which writes AT+PDU commands to a serial port).
- **SystemSetting** — `FrmUser` (users + privileges), `FrmPreference` (user settings),
  `FrmBackupDb` (MySqlBackup dump → upload to Google Drive via `GDriveFacade`, then delete temp file).

## Fee-calculation core (the most intricate logic)

- `Service/Data/SoThuTienService.cs` (~600 lines) generates the monthly fee sheet
  (`GenerateSoThuTienByHocSinhRows` → `GenerateSoThuTienByHocSinhAndLopAndNgayTinh` →
  `generateBangThuTienKhoanThu`), carries the previous month forward
  (`EvaluatePrevMonthViewBangThuTienTable`, `getAndSortPrevMonthViewBangThuTienRows`), and builds the
  aggregates for receipts/reports (`GetBangKeTongHopThuTien`, `GetBangKeTongHopThuTienForTaoPhieuThu`,
  `GetDanhSachTreChuaNopHocPhiByMonth`).
- `Utils/BangThuTienUtil.cs` holds the arithmetic: portions → money (`SXToSoTienSX`,
  `SXAnSangToSoTienAnSang`, `SXAnToiToSoTienAnToi`), `CalculateThanhTien`, `CalculateTruyThu`
  (arrears), `CalculateSummaryFields`, `EvaluateValuesForViewBangThuTienRow`, `CalculateSoTienPhi`
  (pro-rating by absent days), `RecalculateBangThuTienKhoanThuList`.
- Fixed fee-item ids are in `Constant/BangThuTienConstant.cs`
  (1 TienAnSua, 2 PhuPhi, 3 BanTru, 4 HocPhi, 5 AnSang, 6 AnToi, 7 TienSua) — **these are hard-coded
  and must match the `khoanthu` table rows**.
- Per-portion prices come from *user settings* (`TienAnSang`, `TienAnChinh`, `TienAnToi`), not the DB.
- `PaymentType` (`CASH` / `TRANSFER`) was added to `phieuthu` (script `20251027`) and `phieuchi`
  (script `20260602`); reports split cash vs. transfer columns (`SoTienNop` / `SoTienChuyenKhoan`).
  Read it through `PaymentTypeEnum`, never by string comparison.

## ACG.Core libraries (separate projects in the solution)

- `ACG.Core.WinForm.Util` — `StringUtil`, `ListUtil`, `DateTimeUtil`, `IntUtil`, `ObjectUtil`,
  `ArrayUtil`, `ControlUtil`, `PasswordUtil`. **Prefer these over writing new helpers.**
- `ACG.Core.WinForm.Data.Static` — `IStaticData` / `StaticDataMap` lazy lookup cache behind `StaticDataFacade`.
- `ACG.Core.WinForm.Mediator` — `IModuleMediator`, `ModuleMediator`, `Action` (publish/subscribe by channel name).
- `ACG.Core.WinForm.Logging` — `ILogger`, `TraceListenerLogger`.
- `ACG.Core.WinForm.ActivityTracking` — base activity tracker + per-control handlers; the DevExpress
  specialization is `QLMamNon/Components/ActivityTracker/DEActivityTracker.cs`.

## Settings (`QLMamNon/app.config`)

- `connectionStrings`: `QLMamNon.Properties.Settings.qlmamnonConnectionString1` (plain MySQL, used by
  backup) and `qlmamnonEntities` (EF metadata + provider connection string). **Both must point at the
  same database**; the DB name is versioned by date (`qlmamnon_20250919`).
- `applicationSettings` (read-only, per build): `AppLannchDate` *(sic — the typo is the real key,
  mirrored in `ConfigConstant.SettingKeyAppLaunchDate`)*, `DefaultMaPhanLoaiChi`, SMS endpoints/keys,
  `IsMNHongMinh` and the two school name/address blocks used in report headers.
- `userSettings` (writable at runtime, via `FrmPreference`): `TienAnSang`, `TienAnChinh`, `TienAnToi`,
  `SoTienTonDauKy`, `StartYearForDropDown`, `NamSinhStart`, `PhanLoaiThuByHocSinh`,
  `ShowGiayBaoNopTien*`, `POSPrinterName`, `LastMaPhieuThu`.
- Typed access is `QLMamNon.Properties.Settings.Default.<Name>`.

## Code style & gotchas

- Follow the surrounding style: `#region Properties / Events / Helper` blocks, `private` helpers in
  `camelCase` (unusual for C#, but consistent here), public members `PascalCase`, `this.` prefix on
  instance members, `_`-free field names. Controls use DevExpress prefixes (`btn`, `gv`, `gc`, `cmb`,
  `date`, `txt`, `lbl`, `bbi`/`bsi` for ribbon items).
- UI strings, message boxes and comments are **Vietnamese** — keep it that way.
- `Program.Main` pins culture to `vi-VN` with `dd/MM/yyyy` and `.`/`,` separators; don't re-introduce
  invariant-culture parsing of user input.
- Never hand-edit `*.Designer.cs` / `*.designer.cs` or `.resx` — those are designer-owned. Put logic in
  the code-behind or a partial file (the project already uses partials for large screens, e.g.
  `FrmSoThuTien.*.cs`, `FrmTaoPhieuThu.Service.cs`).
- **Many files under `Reports/` are UTF-16LE encoded** (that's why git shows them as `Bin`). Preserve the
  encoding when editing, and expect text tools to need `iconv -f UTF-16LE`. Most other sources are UTF-8 with BOM.
- New files must be added to `QLMamNon/QLMamNon.csproj` manually (old-style project — no globbing),
  including `<DependentUpon>` for designer/resx partners.
- Each `CRUDForm<T>` creates its **own** `qlmamnonEntities`; `FrmMain` holds yet another one for static
  data. Don't assume a single shared change-tracker across screens.
- `Service/Data/BaseDataService.cs` is an empty marker base class; `GiaoVienService` is fully
  commented out; `Workflow/Forms/FrmMainFlow.InitWorkFlow` is a no-op. These are placeholders, not bugs to "fix".
- Stale/vendored artifacts to leave alone: `QLMamNon.zip`, `QLMamNon/Backup/*.zip`, `Dao/New folder/`,
  `Libs/*.zip`, `Properties/licenses.licx.bak`, `Forms/ThuChi/FrmSoThuTien.resx.bak`.
- **Secrets are committed to this repo** (DB root password and SMS API keys in `app.config`,
  `QLMamNon/client_secret.json` for Google Drive). Don't copy them into new files, logs, commit
  messages or anything published; if they are ever rotated, they should move out of source control.

## Git workflow

- Default branch `main`; single author (`minhnhat2006`), small commits with terse imperative
  subjects ("Update PhieuThu/PhieuChi/Reports", "Add BaoCaoSiSoHocSinhTheoLop").
- `.gitignore` covers only `bin/`, `obj/` and `.vs/` — `packages/` **is** committed.
- Recent direction of work: `PaymentType` (cash vs. bank transfer) threaded through receipts, payment
  vouchers and reports; new daily/per-class collection reports (`FrmSoQuyTienMatTheoNgay`,
  `FrmBangKeThuTrongNgay`, `FrmBangKeThuTheoLop`, `FrmBaoCaoSiSoHocSinhTheoLop`).
