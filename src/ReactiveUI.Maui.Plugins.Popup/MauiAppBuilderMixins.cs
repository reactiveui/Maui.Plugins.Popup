// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Mopups.Hosting;
using ReactiveUI.Builder;
using Splat;

namespace Microsoft.Maui.Hosting;

/// <summary>Configures ReactiveUI and Mopups for a MAUI application.</summary>
public static class MauiAppBuilderMixins
{
    /// <summary>Extension members for <c>MauiAppBuilder</c>.</summary>
    /// <param name="builder">The application builder.</param>
    extension(MauiAppBuilder builder)
    {
        /// <summary>Registers Mopups and the default ReactiveUI MAUI services.</summary>
        /// <returns>The application builder.</returns>
        public MauiAppBuilder ConfigureReactiveUIPopup()
        {
            _ = builder.ConfigureMopups();
            _ = AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithMaui().BuildApp();
            return builder;
        }

        /// <summary>Registers Mopups and configures ReactiveUI MAUI services before building the app.</summary>
        /// <param name="configureReactiveUI">The optional ReactiveUI configuration callback.</param>
        /// <returns>The application builder.</returns>
        public MauiAppBuilder ConfigureReactiveUIPopup(Action<IReactiveUIBuilder>? configureReactiveUI)
        {
            _ = builder.ConfigureMopups();
            var rxuiBuilder = AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithMaui();
            configureReactiveUI?.Invoke(rxuiBuilder);
            _ = rxuiBuilder.BuildApp();
            return builder;
        }

        /// <summary>Registers ReactiveUI and Mopups with the specified back button handler.</summary>
        /// <param name="backPressHandler">The optional back button handler.</param>
        /// <returns>The application builder.</returns>
        public MauiAppBuilder ConfigureReactiveUIPopup(Action? backPressHandler)
        {
            _ = builder.ConfigureMopups(backPressHandler);
            _ = AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithMaui().BuildApp();
            return builder;
        }

        /// <summary>Registers Mopups and configures ReactiveUI with optional back button handling.</summary>
        /// <param name="backPressHandler">The optional back button handler.</param>
        /// <param name="configureReactiveUI">The optional ReactiveUI configuration callback.</param>
        /// <returns>The application builder.</returns>
        public MauiAppBuilder ConfigureReactiveUIPopup(Action? backPressHandler, Action<IReactiveUIBuilder>? configureReactiveUI)
        {
            _ = builder.ConfigureMopups(backPressHandler);
            var rxuiBuilder = AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithMaui();
            configureReactiveUI?.Invoke(rxuiBuilder);
            _ = rxuiBuilder.BuildApp();
            return builder;
        }
    }
}
