[![Build](https://github.com/reactiveui/Maui.Plugins.Popup/actions/workflows/ci-build.yml/badge.svg)](https://github.com/reactiveui/Maui.Plugins.Popup/actions/workflows/ci-build.yml)
[![NuGet](https://img.shields.io/nuget/v/ReactiveUI.Maui.Plugins.Popup.svg)](https://www.nuget.org/packages/ReactiveUI.Maui.Plugins.Popup/)
[![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://reactiveui.net/contribute)
[![](https://img.shields.io/badge/chat-slack-blue.svg)](https://reactiveui.net/slack)

<br>
<a href="https://github.com/reactiveui/reactiveui">
  <img width="160" height="160" src="https://raw.githubusercontent.com/reactiveui/styleguide/master/logo/main.png">
</a>
<br>

# ReactiveUI.Maui.Plugins.Popup

This package provides [ReactiveUI](https://reactiveui.net/) bindings and helpers for the [Mopups](https://github.com/LuckyDucko/Mopups) library in .NET MAUI. It enables you to build composable, reactive popup views using the Model-View-ViewModel (MVVM) pattern.

---

## Packages

Install the package into your MAUI project:

- [![ReactiveUI.Maui.Plugins.Popup](https://img.shields.io/nuget/v/ReactiveUI.Maui.Plugins.Popup.svg)](https://www.nuget.org/packages/ReactiveUI.Maui.Plugins.Popup/)

---

## Recommended Setup

To initialize the plugin, you must register it within your `MauiProgram.cs` file. This ensures that the Mopups services are configured and the ReactiveUI interactions are wired up correctly.

```csharp
using Microsoft.Maui.Hosting;
using ReactiveUI.Maui.Plugins.Popup;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            // Initialize ReactiveUI Popups
            .ConfigureReactiveUIPopup(); 

        return builder.Build();
    }
}
```

---

## Quick Example: First Reactive Popup

### 1. Create a ViewModel

Inherit from `ReactiveObject` to gain access to reactive properties and commands.

```csharp
using System.Reactive;
using ReactiveUI;

public class ConfirmViewModel : ReactiveObject
{
    public ReactiveCommand<Unit, Unit> ConfirmCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public ConfirmViewModel()
    {
        ConfirmCommand = ReactiveCommand.Create(() => { /* handle confirm */ });
        CancelCommand = ReactiveCommand.Create(() => { /* handle cancel */ });
    }
}
```

### 2. Create a Popup View

Inherit from `ReactivePopupPage<TViewModel>` to create a strongly-typed popup.

```csharp
using ReactiveUI;
using ReactiveUI.Maui.Plugins.Popup;

public partial class ConfirmPopup : ReactivePopupPage<ConfirmViewModel>
{
    public ConfirmPopup()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.BindCommand(ViewModel, vm => vm.ConfirmCommand, v => v.ConfirmButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.CancelCommand, v => v.CancelButton)
                .DisposeWith(disposables);
        });
    }
}
```

### 3. Display the Popup

```csharp
var popup = new ConfirmPopup { ViewModel = new ConfirmViewModel() };
this.Navigation.PushPopup(popup).Subscribe();
```

---

## Understanding Hot and Cold Observables

This library provides both **Hot** and **Cold** observables for different use cases:

### Cold Observables (Navigation Methods)

The navigation methods (`PopAllPopup`, `PopPopup`, `PushPopup`, `RemovePopupPage`) return **Cold Observables** created from async operations.

**Key Characteristics:**
- The operation does **not** begin until the observable is subscribed to
- Each subscription triggers a **new execution** of the async operation
- The observable completes after emitting a single value
- Ideal for command-driven navigation operations

**Example:**
```csharp
// The popup is NOT pushed until Subscribe() is called
var pushOperation = this.Navigation.PushPopup(myPopup);

// Subscribing triggers the push operation
pushOperation.Subscribe();

// Each subscription triggers a new push (be careful!)
pushOperation.Subscribe(); // This would push the popup again!
```

### Hot Observables (Event Streams)

The event observables (`PoppingObservable`, `PoppedObservable`, `PushingObservable`, `PushedObservable`) return **Hot Observables** derived from events.

**Key Characteristics:**
- Produce values **regardless** of whether there are active subscriptions
- Events fire independently of subscription state
- Multiple subscribers receive the same event notifications
- Ideal for monitoring and reacting to navigation events

**Example:**
```csharp
// Monitor all popup pushes in your application
var popupService = MopupService.Instance;
popupService.PushedObservable()
    .Subscribe(args => 
    {
        Debug.WriteLine($"Popup pushed: {args}");
    });

// Multiple subscribers can observe the same events
popupService.PoppedObservable()
    .Subscribe(args => 
    {
        Debug.WriteLine($"Popup dismissed: {args}");
    });
```

---

## API Reference

### Navigation Methods (Cold Observables)

All navigation methods are available as extension methods on both `INavigation` and `IPopupNavigation`.

#### `PopAllPopup`
Removes all popup pages from the popup navigation stack in a single operation.

```csharp
// Using INavigation
this.Navigation.PopAllPopup(animate: true).Subscribe();

// Using IPopupNavigation
MopupService.Instance.PopAllPopup(animate: true).Subscribe();
```

#### `PopPopup`
Removes only the topmost popup from the popup navigation stack using a last-in, first-out (LIFO) approach.

```csharp
this.Navigation.PopPopup(animate: true).Subscribe();
```

#### `PushPopup<T>`
Adds a new popup to the top of the popup navigation stack. The popup stack is maintained separately from the main MAUI navigation stack, allowing popups to be displayed as overlays.

```csharp
var popup = new MyPopup { ViewModel = new MyViewModel() };
this.Navigation.PushPopup(popup, animate: true).Subscribe();
```

#### `RemovePopupPage<T>`
Removes a specific popup page from anywhere in the popup stack, not just the topmost one.

```csharp
this.Navigation.RemovePopupPage(specificPopup, animate: true).Subscribe();
```

### Event Observables (Hot Observables)

#### `PoppingObservable`
Fires **before** the popup is actually removed from the navigation stack, allowing for interception or logging.

```csharp
MopupService.Instance.PoppingObservable()
    .Subscribe(args => 
    {
        Console.WriteLine($"Popup {args} is about to be popped");
    });
```

#### `PoppedObservable`
Fires **after** the popup has been fully removed from the navigation stack, allowing for cleanup or tracking.

```csharp
MopupService.Instance.PoppedObservable()
    .Subscribe(args => 
    {
        Console.WriteLine($"Popup {args} has been popped");
    });
```

#### `PushingObservable`
Fires **before** the popup is actually added to the navigation stack, allowing for interception or validation.

```csharp
MopupService.Instance.PushingObservable()
    .Subscribe(args => 
    {
        Console.WriteLine($"Popup {args} is about to be pushed");
    });
```

#### `PushedObservable`
Fires **after** the popup has been fully added to the navigation stack, allowing for tracking or initialization.

```csharp
MopupService.Instance.PushedObservable()
    .Subscribe(args => 
    {
        Console.WriteLine($"Popup {args} has been pushed");
    });
```

---

## Controls

### `ReactivePopupPage`
A base popup page that implements `IViewFor` for use without a specific ViewModel type.

### `ReactivePopupPage<TViewModel>`
A strongly-typed popup page that implements `IViewFor<TViewModel>`, providing compile-time safety and IntelliSense support for your ViewModel.

---

## Sponsorship

The core team members, ReactiveUI contributors and contributors in the ecosystem do this open-source work in their free time. If you use ReactiveUI, a serious task, and you'd like us to invest more time on it, please donate. This project increases your income/productivity too. It makes development and applications faster and it reduces the required bandwidth.

[Become a sponsor](https://github.com/sponsors/reactivemarbles).

This is how we use the donations:

* Allow the core team to work on ReactiveUI
* Thank contributors if they invested a large amount of time in contributing
* Support projects in the ecosystem

---

## ReactiveUI and Contributors

ReactiveUI is developed and maintained by the ReactiveUI community and contributors. This project is part of the broader ReactiveUI ecosystem, which provides a composable, testable framework for building reactive applications across multiple platforms.
