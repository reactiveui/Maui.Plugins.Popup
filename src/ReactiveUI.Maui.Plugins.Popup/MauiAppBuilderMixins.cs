// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Mopups.Hosting;
using ReactiveUI.Builder;
using Splat;

namespace Microsoft.Maui.Hosting;

/// <summary>
/// INavigation Mixins.
/// </summary>
public static class MauiAppBuilderMixins
{
    /// <summary>
    /// Registers all the default registrations that are needed by the Splat module.
    /// Initialize resolvers with the default ReactiveUI types.
    /// Configures ReactiveUI Maui Mopups.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>MauiAppBuilder.</returns>
    public static MauiAppBuilder ConfigureReactiveUIPopup(this MauiAppBuilder builder)
    {
        builder.ConfigureMopups();
        AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithMaui().BuildApp();
        return builder;
    }

    /// <summary>
    /// Configures ReactiveUI integration with popup support for a .NET MAUI application using the specified builder.
    /// </summary>
    /// <remarks>This method sets up the necessary components to use ReactiveUI with popup functionality in a
    /// .NET MAUI application. It should be called during application startup as part of the builder configuration
    /// pipeline.</remarks>
    /// <param name="builder">The MAUI application builder used to configure the application and register services.</param>
    /// <param name="configureReactiveUI">An optional action to further configure the ReactiveUI builder, allowing customization of ReactiveUI features
    /// and services.</param>
    /// <returns>The same MAUI application builder instance, enabling method chaining.</returns>
    public static MauiAppBuilder ConfigureReactiveUIPopup(this MauiAppBuilder builder, Action<IReactiveUIBuilder>? configureReactiveUI)
    {
        builder.ConfigureMopups();
        var rxuiBuilder = AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithMaui();
        configureReactiveUI?.Invoke(rxuiBuilder);
        rxuiBuilder.BuildApp();
        return builder;
    }

    /// <summary>
    /// Configures the reactive UI mopups.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="backPressHandler">The back press handler.</param>
    /// <returns>MauiAppBuilder.</returns>
    public static MauiAppBuilder ConfigureReactiveUIPopup(this MauiAppBuilder builder, Action? backPressHandler)
    {
        builder.ConfigureMopups(backPressHandler);
        AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithMaui().BuildApp();
        return builder;
    }

    /// <summary>
    /// Configures ReactiveUI popup support for the specified .NET MAUI application builder, enabling integration of
    /// reactive popups and optional back press handling.
    /// </summary>
    /// <remarks>Call this method during application startup to enable ReactiveUI-based popups in your MAUI
    /// app. This method also allows customization of back press handling and additional ReactiveUI configuration
    /// through the provided delegates.</remarks>
    /// <param name="builder">The MAUI application builder to configure with ReactiveUI popup functionality.</param>
    /// <param name="backPressHandler">An optional action to invoke when the back button is pressed while a popup is active. If null, the default back
    /// press behavior is used.</param>
    /// <param name="configureReactiveUI">An optional action to further configure the ReactiveUI builder before the application is built.</param>
    /// <returns>The same MAUI application builder instance, allowing for method chaining.</returns>
    public static MauiAppBuilder ConfigureReactiveUIPopup(this MauiAppBuilder builder, Action? backPressHandler, Action<IReactiveUIBuilder>? configureReactiveUI)
    {
        builder.ConfigureMopups(backPressHandler);
        var rxuiBuilder = AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithMaui();
        configureReactiveUI?.Invoke(rxuiBuilder);
        rxuiBuilder.BuildApp();
        return builder;
    }
}
