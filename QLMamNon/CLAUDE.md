# WinForms C# Project Guidelines

## Build & Test Commands
- **Build Solution**: `dotnet build`
- **Run App**: `dotnet run --project src/QLMamNon.csproj`
- **Run Tests**: `dotnet test`

## WinForms & UI Code Rules
- **UI Threading**: Always wrap non-UI thread updates in `this.Invoke(new Action(() => { ... }))` or use `async/await` with `Progress<T>`.
- **Form Controls**: Keep `InitializeComponent()` inside `.Designer.cs` clean. Do not manually write code inside `.Designer.cs` files—put event handlers in `.cs` code-behind.
- **Resource Cleanup**: Always use `using` statements or call `.Dispose()` on disposable UI objects (`Graphics`, `Pen`, `Brush`, modal dialogs).
- **Architecture**: Separate business/data logic from Form code-behind files. Use MVP (Model-View-Presenter) or Service classes where possible.

## Code Style
- Use PascalCase for controls (e.g., `ButtonSubmit`, `TxtCustomerName`).
- Target Framework: .NET 4.6.2