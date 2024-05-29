# ReactiveUI.Maui.Plugins.Popup
ReactiveUI support for Maui Popups

## Usage

Use ConfigureReactiveUIPopup() with MauiAppBuilder to initialise ReactiveUI.Maui.Plugins.Popup

```csharp
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using ReactiveUI.Maui.Plugins.Popup;

namespace MauiApp1
{
    public class Startup : IStartup
    {
        public void Configure(IAppHostBuilder appBuilder)
        {
            appBuilder
                .UseMauiApp<App>()
                .ConfigureReactiveUIPopup();
        }
    }
}
```


The following methods are available

	PopAllPopup
	PopPopup
	PushPopup
	RemovePopupPage

The Following Observable Events are available

	PoppingObservable
	PoppedObservable
	PushingObservable
	PushedObservable

The following Controls are available

    ReactivePopupPage
    ReactivePopupPage<TViewModel>

## Sponsorship

The core team members, ReactiveUI contributors and contributors in the ecosystem do this open-source work in their free time. If you use ReactiveUI, a serious task, and you'd like us to invest more time on it, please donate. This project increases your income/productivity too. It makes development and applications faster and it reduces the required bandwidth.

[Become a sponsor](https://github.com/sponsors/reactivemarbles).

This is how we use the donations:

* Allow the core team to work on ReactiveUI
* Thank contributors if they invested a large amount of time in contributing
* Support projects in the ecosystem


## .NET Foundation

ReactiveUI is part of the [.NET Foundation](https://www.dotnetfoundation.org/). Other projects that are associated with the foundation include the Microsoft .NET Compiler Platform ("Roslyn") as well as the Microsoft ASP.NET family of projects, Microsoft .NET Core & Xamarin Forms.
