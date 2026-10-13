# WinForms C# Project Guidelines

## Build & Test Commands
This is a legacy non-SDK project (`packages.config`, DevExpress 17.1, net462), so the `dotnet` CLI
cannot build or run it — use MSBuild from a Visual Studio install.
- **Build Solution**: `MSBuild.exe QLMamNon.sln /t:Build /p:Configuration=Debug`
  (e.g. `"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe"`).
  `Setup\Setup.vdproj` is not buildable by MSBuild and always reports warning MSB4078.
- **Restore** (only if `packages\` is missing): `MSBuild.exe /t:Restore /p:RestorePackagesConfig=true`
- **Run App**: `QLMamNon\bin\Debug\QLMamNon.exe` (needs a reachable MySQL database)
- **Tests**: there is no test project in the solution. Verification is manual, against a restored
  copy of `QLMamNon\DB\qlmamnon.sql` — never against live data.

## WinForms & UI Code Rules
- **UI Threading**: Always wrap non-UI thread updates in `this.Invoke(new Action(() => { ... }))` or use `async/await` with `Progress<T>`.
- **Form Controls**: Keep `InitializeComponent()` inside `.Designer.cs` clean. Do not manually write code inside `.Designer.cs` files—put event handlers in `.cs` code-behind.
- **Resource Cleanup**: Always use `using` statements or call `.Dispose()` on disposable UI objects (`Graphics`, `Pen`, `Brush`, modal dialogs).
- **Architecture**: Separate business/data logic from Form code-behind files. Use MVP (Model-View-Presenter) or Service classes where possible.

## Code Style
- Use PascalCase for controls (e.g., `ButtonSubmit`, `TxtCustomerName`).
- Target Framework: .NET 4.6.2