<div align="center">

<a href="https://github.com/reactiveui/Maui.Plugins.Popup"><img src="https://raw.githubusercontent.com/reactiveui/Maui.Plugins.Popup/main/images/logo.png" alt="ReactiveUI.Maui.Plugins.Popup" width="128"></a>

# ReactiveUI.Maui.Plugins.Popup

[![Build](https://img.shields.io/github/actions/workflow/status/reactiveui/Maui.Plugins.Popup/ci-build.yml?branch=main&label=build&logo=githubactions&logoColor=white)](https://github.com/reactiveui/Maui.Plugins.Popup/actions/workflows/ci-build.yml)
[![NuGet](https://img.shields.io/nuget/v/ReactiveUI.Maui.Plugins.Popup?label=nuget&logo=nuget&logoColor=white&color=004880)](https://www.nuget.org/packages/ReactiveUI.Maui.Plugins.Popup)
[![Downloads](https://img.shields.io/nuget/dt/ReactiveUI.Maui.Plugins.Popup?label=downloads&logo=nuget&logoColor=white&color=004880)](https://www.nuget.org/packages/ReactiveUI.Maui.Plugins.Popup)
[![License](https://img.shields.io/github/license/reactiveui/Maui.Plugins.Popup?color=blue)](https://github.com/reactiveui/Maui.Plugins.Popup/blob/main/LICENSE)
[![Discussions](https://img.shields.io/badge/discussions-ask%20a%20question-8A2BE2?logo=github&logoColor=white)](https://github.com/reactiveui/ReactiveUI/discussions)

</div>

This package provides [ReactiveUI](https://reactiveui.net/) bindings and helpers for the [Mopups](https://github.com/LuckyDucko/Mopups) library in .NET MAUI. It enables you to build composable, reactive popup views using the Model-View-ViewModel (MVVM) pattern.

## Packages

Install the package into your MAUI project:

- [![ReactiveUI.Maui.Plugins.Popup](https://img.shields.io/nuget/v/ReactiveUI.Maui.Plugins.Popup.svg)](https://www.nuget.org/packages/ReactiveUI.Maui.Plugins.Popup/)

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

## Controls

### `ReactivePopupPage`
A base popup page that implements `IViewFor` for use without a specific ViewModel type.

### `ReactivePopupPage<TViewModel>`
A strongly-typed popup page that implements `IViewFor<TViewModel>`, providing compile-time safety and IntelliSense support for your ViewModel.

## Contribute

ReactiveUI.Maui.Plugins.Popup is part of [ReactiveUI](https://github.com/reactiveui) and is released under the [MIT license](https://github.com/reactiveui/Maui.Plugins.Popup/blob/main/LICENSE). We welcome contributions of every size, from bug reports and documentation to features and tests.

- **Questions:** ask in [GitHub Discussions](https://github.com/reactiveui/ReactiveUI/discussions).
- **Bugs and feature requests:** open a [GitHub issue](https://github.com/reactiveui/Maui.Plugins.Popup/issues/new/choose).
- **Larger changes:** open an issue or discussion first, so we can agree on the approach before you start.
- **Contributor guide:** read [reactiveui.net/contribute](https://www.reactiveui.net/contribute/) for [building and testing](https://www.reactiveui.net/contribute/building-and-testing/) and [pull requests](https://www.reactiveui.net/contribute/pull-requests/).

### Coding standards

Every pull request must meet these standards. The build checks most of them.

- Follow the [ReactiveUI C# style guide](https://www.reactiveui.net/contribute/software-style-guide/code-style/).
- The repository's `.editorconfig` and the StyleSharp, PerformanceSharp and SecuritySharp analyzers run on every build. Fix each finding in the code. Do not suppress it or relax the `.editorconfig`.
- Add tests for every bug fix and new feature.
- Document public APIs with XML documentation comments.
- Write commit messages in the [Conventional Commits](https://www.reactiveui.net/contribute/software-style-guide/commit-message-convention/) format.
- Follow the [Code of Conduct](https://github.com/reactiveui/.github/blob/main/CODE_OF_CONDUCT.md).

### Support the project

- Sponsor the maintainers on GitHub Sponsors: [reactivemarbles](https://github.com/sponsors/reactivemarbles), [glennawatson](https://github.com/sponsors/glennawatson) and [chrispulman](https://github.com/sponsors/chrispulman).
- Ask your employer to support open source, for example with the [balanced employee IP agreement](https://github.com/github/balanced-employee-ip-agreement).

## Sponsors

[JetBrains](https://www.jetbrains.com/) gives ReactiveUI's maintainers licences for its tools through its
[open source support programme](https://www.jetbrains.com/community/opensource/).
[Anthropic](https://www.anthropic.com/) supports them with [Claude](https://claude.com/) through
[Claude for Open Source](https://claude.com/contact-sales/claude-for-oss).
[OpenAI](https://openai.com/) supports them with [Codex](https://openai.com/codex/) through
[Codex for Open Source](https://developers.openai.com/community/codex-for-oss).
[Snyk](https://snyk.io/) gives the maintainers access to security tools through its
[Secure Developer Program](https://snyk.io/open-source/).

[![JetBrains](https://raw.githubusercontent.com/reactiveui/website/main/docs/images/sponsors/jetbrains.svg)](https://www.jetbrains.com/)
[![Claude by Anthropic](https://raw.githubusercontent.com/reactiveui/website/main/docs/images/sponsors/claude.svg)](https://claude.com/)
[![OpenAI](https://raw.githubusercontent.com/reactiveui/website/main/docs/images/sponsors/openai.svg)](https://openai.com/codex/)
[![Sponsored by Snyk](https://raw.githubusercontent.com/reactiveui/website/e37a112f97ea2e9ea54b21bb7ed9e56df034d618/docs/images/sponsors/snyk.svg)](https://snyk.io/)

See [our sponsors](https://www.reactiveui.net/sponsors/) for more information.
JetBrains, Claude, Anthropic, OpenAI and Codex names and logos are trademarks of their respective owners.
Snyk and the Snyk logo are trademarks of Snyk Ltd.
