# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

`QLMamNon` — Vietnamese kindergarten management desktop app (students, teachers, assets, fee collection
"thu/chi", SMS, printed reports). WinForms on .NET Framework 4.6.2, DevExpress 17.1 UI/reporting,
Entity Framework 6 over MySQL. UI text, table and column names are Vietnamese; keep that vocabulary.

## Build & run

Legacy non-SDK project (`packages.config`, `ToolsVersion 12.0`), so **the `dotnet` CLI cannot build, run
or test it** — use MSBuild from a Visual Studio install. There is no `src/` directory and no test project.

- **Build**: `"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" QLMamNon.sln /t:Build /p:Configuration=Debug`
  `Setup\Setup.vdproj` is not MSBuild-buildable and always reports warning MSB4078 — expected, not a failure.
- **Restore** (only if `packages\` is missing): `MSBuild.exe /t:Restore /p:RestorePackagesConfig=true`
- **Run**: `QLMamNon\bin\Debug\QLMamNon.exe` — needs a reachable MySQL server; connection strings live in
  `QLMamNon\app.config` (`qlmamnonEntities`, EF metadata + `MySql.Data.MySqlClient`).
- **Tests**: none exist. Verification is manual against a restored copy of `DB\qlmamnon.sql` — never
  against live data. State plainly when a change was not verified by running the app.

## Architecture

Six C# projects in `QLMamNon.sln` plus the `Setup` installer:

- `QLMamNon` — the application: Forms, Reports, Service, Dao, Facade, Utils, Workflow, UserControls.
- `ACG.Core.WinForm.*` — in-house infrastructure consumed through the Facade layer:
  `Mediator` (loose coupling between modules), `Logging` (log4net), `Data.Static` (`IStaticDataMap`
  in-memory cache of lookup tables), `ActivityTracking` (per-form user activity), `Util`.

Layering, outermost first:

1. **Forms** (`QLMamNon/Forms/<Module>/`) — one folder per domain area (`ThuChi`, `HocSinh`, `GiaoVien`,
   `DanhMuc`, `BaoCao`, `TaiSan`, `TinNhan`, `SystemSetting`, `Authenticate`). `FrmMain` hosts them;
   `FormMainFacade` tracks open forms by `FormKey`, `Constant/AppForms.cs` +
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

### Data-access constraints that are easy to get wrong

- **View entity sets are read-only.** `viewbangthutien` is mapped with a `<DefiningQuery>` and no
  `<UpdateFunction>` (`Dao/QLMNModel.edmx`), and the repo has zero `ModificationFunctionMapping`.
  `SaveChanges()` on a Modified view instance throws `UpdateException`. Rows returned by
  `ExecuteFunction` stubs are tracked (`MergeOption.AppendOnly`), so this is a live hazard: write
  through the real table entity (`entities.bangthutiens`, see `Service/Data/SoThuTienService.cs`).
- Each `CRUDForm` holds its own `DbContext` from `StaticDataFacade.GetQLMNEntities()`; don't share
  entity instances across contexts.
- `Program.cs` forces culture `vi-VN` with `.` thousands / `,` decimal separators and a Times New Roman
  DevExpress default font — number and date parsing depends on it.

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

## Repo notes

- Plans and design notes for in-flight work live in `.claude/plans/` (configured via
  `plansDirectory` in `.claude/settings.json`), not in the global Claude folder.
- `QLMamNon/client_secret.json` holds Google Drive credentials — do not read, print or commit changes to it.
- `DB/*.sql` are full schema+data dumps; `Documents/` holds reference screenshots of the paper forms the
  reports must match.
