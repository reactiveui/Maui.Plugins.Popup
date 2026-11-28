# Delivery popup

[DeliveryNotePage](DeliveryPopup/DeliveryNotePage.cs) displays a delivery note. It subscribes to background taps when it appears and releases that subscription when it disappears.

[ShowNotesAsync](DeliveryPopup/Program.cs) accepts a MAUI page's `INavigation` and an `IPopupNavigation`. In a MAUI app, call `builder.ConfigureReactiveUIPopup()` on the UI thread during startup. Then pass `Navigation` and `MopupService.Instance` to the example. The console runner supplies an [in-memory popup stack](DeliveryPopup/PreviewPopupNavigation.cs) so it can check navigation events without opening a window.

Run from `src` with the repository's MAUI workloads installed.

```shell
dotnet run --project examples/DeliveryPopup -f net11.0
```

The same example targets .NET 10 with `-f net10.0`.

## Configure startup

Import `Microsoft.Maui.Hosting` in `MauiProgram.cs`. Choose one registration when you create the app. For example, to keep a delivery screen's view model in the resolver and handle a device back press:

```csharp
builder.ConfigureReactiveUIPopup(
    backPressHandler: HandleBackPress,
    configureReactiveUI: rx => rx.RegisterConstantViewModel<DeliveryViewModel>());
```

Supply just `backPressHandler: HandleBackPress` for a custom back action, or just `configureReactiveUI: rx => rx.RegisterConstantViewModel<DeliveryViewModel>()` for app registrations. Use `builder.ConfigureReactiveUIPopup()` for the defaults. These calls need the MAUI UI dispatcher. The console runner starts at the navigation step.

## Choose a navigation operation

The same operations work with a page's `Navigation` and an injected `IPopupNavigation`. The former resolves `MopupService.Instance` when you subscribe. Pass `animate: false` to skip the transition. Omitting it enables animation.

| What the user should see | Method | Events |
| --- | --- | --- |
| A new popup above the current one | `PushPopup(page)` | `PushingObservable()`, then `PushedObservable()` |
| The top popup closes | `PopPopup()` | `PoppingObservable()`, then `PoppedObservable()` |
| One specific popup closes | `RemovePopupPage(page)` | Closing events for that page |
| The whole stack closes | `PopAllPopup()` | Closing events for the pages removed |
