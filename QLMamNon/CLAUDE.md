# WinForms C# Project Guidelines

Full project context (architecture, domain glossary, data layer, modules, gotchas) lives in
[`../CLAUDE.md`](../CLAUDE.md) at the repository root. Read that first; this file is only the
coding rules for the app project.

## Build & Test Commands
- **Build**: open `QLMamNon.sln` in Visual Studio 2017+ and build, or
  `msbuild QLMamNon.sln /p:Configuration=Debug`. Requires a licensed **DevExpress WinForms v17.1**
  install on the machine.
- `dotnet build` / `dotnet run` do **not** work — these are old-style (non-SDK) .NET Framework 4.6.2 projects.
- **Run App**: start the `QLMamNon` project (output `QLMamNon/bin/<Config>/QLMamNon.exe`).
  It needs a reachable MySQL database per `QLMamNon/app.config`.
- **Tests**: there is no test project and no CI; changes are verified by running the app.

## WinForms & UI Code Rules
- **UI Threading**: Always wrap non-UI thread updates in `this.Invoke(new Action(() => { ... }))` or use `async/await` with `Progress<T>`.
- **Form Controls**: Keep `InitializeComponent()` inside `.Designer.cs` clean. Do not manually write code inside `.Designer.cs` files—put event handlers in `.cs` code-behind.
- **Resource Cleanup**: Always use `using` statements or call `.Dispose()` on disposable UI objects (`Graphics`, `Pen`, `Brush`, modal dialogs).
- **Architecture**: Separate business/data logic from Form code-behind files. Use the existing
  `Service/Data/*` service classes (or add one) rather than putting logic in a `Frm*`.
- **New files** must be added to `QLMamNon.csproj` by hand (no globbing), with `<DependentUpon>` for
  designer/resx partners.
- **EF model**: `Dao/QLMNModel.*` is T4-generated — never hand-edit. Add `[NotMapped]` members in
  `Dao/<table>.Partial.cs`, and ship DDL/stored-procedure changes as `DB/scripts/<yyyyMMdd>/script.sql`.

## Code Style
- Use PascalCase for controls (e.g., `ButtonSubmit`, `TxtCustomerName`); existing DevExpress controls
  use prefixed names (`btnLuu`, `gvMain`, `cmbLop`, `dateTuNgay`) — match the file you are editing.
- Private methods in this codebase are `camelCase`; public members are `PascalCase`. Keep the
  `#region Properties / Events / Helper` layout of the surrounding file.
- UI text, messages and comments are **Vietnamese**.
- Target Framework: .NET 4.6.2
