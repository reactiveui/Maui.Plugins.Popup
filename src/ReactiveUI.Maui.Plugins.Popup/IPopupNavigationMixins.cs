// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive;
using System.Reactive.Linq;
using Mopups.Events;
using Mopups.Interfaces;
using Mopups.Pages;

namespace ReactiveUI.Maui.Plugins.Popup;

/// <summary>
/// IPopupNavigation Mixins.
/// </summary>
public static class IPopupNavigationMixins
{
    /// <summary>
    /// Pops all popup pages from the popup navigation stack.
    /// </summary>
    /// <param name="service">The popup navigation service instance.</param>
    /// <param name="animate">if set to <c>true</c>, animates the popup dismissal.</param>
    /// <returns>An observable sequence that, when subscribed to, executes the navigation operation and emits a single <see cref="Unit"/> value upon completion.</returns>
    /// <remarks>
    /// This method returns a Cold Observable created from an async operation. The popup dismissal operation
    /// does not begin until the observable is subscribed to. Each subscription triggers a new execution
    /// of the async operation. The observable completes after emitting a single value.
    /// </remarks>
    public static IObservable<Unit> PopAllPopup(this IPopupNavigation service, bool animate = true) =>
               Observable.FromAsync(async _ => await service.PopAllAsync(animate).ConfigureAwait(false));

    /// <summary>
    /// Pops the topmost popup page from the navigation stack.
    /// </summary>
    /// <param name="service">The popup navigation service instance.</param>
    /// <param name="animate">if set to <c>true</c>, animates the popup dismissal.</param>
    /// <returns>An observable sequence that, when subscribed to, executes the navigation operation and emits a single <see cref="Unit"/> value upon completion.</returns>
    /// <remarks>
    /// This method returns a Cold Observable created from an async operation. The popup dismissal operation
    /// does not begin until the observable is subscribed to. Each subscription triggers a new execution
    /// of the async operation. The observable completes after emitting a single value.
    /// </remarks>
    public static IObservable<Unit> PopPopup(this IPopupNavigation service, bool animate = true) =>
        Observable.FromAsync(async _ => await service.PopAsync(animate).ConfigureAwait(false));

    /// <summary>
    /// Pushes a popup page onto the navigation stack.
    /// </summary>
    /// <typeparam name="T">The type of popup page. Must derive from <see cref="PopupPage"/>.</typeparam>
    /// <param name="service">The popup navigation service instance.</param>
    /// <param name="page">The popup page to display.</param>
    /// <param name="animate">if set to <c>true</c>, animates the popup presentation.</param>
    /// <returns>An observable sequence that, when subscribed to, executes the navigation operation and emits a single <see cref="Unit"/> value upon completion.</returns>
    /// <remarks>
    /// This method returns a Cold Observable created from an async operation. The popup presentation operation
    /// does not begin until the observable is subscribed to. Each subscription triggers a new execution
    /// of the async operation. The observable completes after emitting a single value.
    /// </remarks>
    public static IObservable<Unit> PushPopup<T>(this IPopupNavigation service, T page, bool animate = true)
        where T : PopupPage => Observable.FromAsync(async _ => await service.PushAsync(page, animate).ConfigureAwait(false));

    /// <summary>
    /// Removes a specific popup page from the navigation stack.
    /// </summary>
    /// <typeparam name="T">The type of popup page. Must derive from <see cref="PopupPage"/>.</typeparam>
    /// <param name="service">The popup navigation service instance.</param>
    /// <param name="page">The popup page to remove.</param>
    /// <param name="animate">if set to <c>true</c>, animates the popup removal.</param>
    /// <returns>An observable sequence that, when subscribed to, executes the navigation operation and emits a single <see cref="Unit"/> value upon completion.</returns>
    /// <remarks>
    /// This method returns a Cold Observable created from an async operation. The popup removal operation
    /// does not begin until the observable is subscribed to. Each subscription triggers a new execution
    /// of the async operation. The observable completes after emitting a single value.
    /// </remarks>
    public static IObservable<Unit> RemovePopupPage<T>(this IPopupNavigation service, T page, bool animate = true)
        where T : PopupPage => Observable.FromAsync(async _ => await service.RemovePageAsync(page, animate).ConfigureAwait(false));

    /// <summary>
    /// Observes when a popup page is beginning to be popped from the navigation stack.
    /// </summary>
    /// <param name="service">The popup navigation service instance.</param>
    /// <returns>An observable sequence that emits <see cref="PopupNavigationEventArgs"/> each time a popup begins to be dismissed.</returns>
    /// <remarks>
    /// This method returns a Hot Observable derived from the Popping event.
    /// Hot Observables produce values regardless of whether there are active subscriptions. The event fires
    /// before the popup is actually removed from the navigation stack, allowing for interception or logging
    /// of navigation events.
    /// </remarks>
    public static IObservable<PopupNavigationEventArgs> PoppingObservable(this IPopupNavigation service) =>
        Observable.FromEvent<EventHandler<PopupNavigationEventArgs>, PopupNavigationEventArgs>(
                    handler =>
                    {
                        void EventHandler(object? sender, PopupNavigationEventArgs args) => handler(args);
                        return EventHandler;
                    },
                    x => service.Popping += x,
                    x => service.Popping -= x);

    /// <summary>
    /// Observes when a popup page has been popped from the navigation stack.
    /// </summary>
    /// <param name="service">The popup navigation service instance.</param>
    /// <returns>An observable sequence that emits <see cref="PopupNavigationEventArgs"/> each time a popup has been dismissed.</returns>
    /// <remarks>
    /// This method returns a Hot Observable derived from the Popped event.
    /// Hot Observables produce values regardless of whether there are active subscriptions. The event fires
    /// after the popup has been fully removed from the navigation stack, allowing for cleanup or tracking
    /// of completed navigation events.
    /// </remarks>
    public static IObservable<PopupNavigationEventArgs> PoppedObservable(this IPopupNavigation service) =>
        Observable.FromEvent<EventHandler<PopupNavigationEventArgs>, PopupNavigationEventArgs>(
                    handler =>
                    {
                        void EventHandler(object? sender, PopupNavigationEventArgs args) => handler(args);
                        return EventHandler;
                    },
                    x => service.Popped += x,
                    x => service.Popped -= x);

    /// <summary>
    /// Observes when a popup page is beginning to be pushed onto the navigation stack.
    /// </summary>
    /// <param name="service">The popup navigation service instance.</param>
    /// <returns>An observable sequence that emits <see cref="PopupNavigationEventArgs"/> each time a popup begins to be presented.</returns>
    /// <remarks>
    /// This method returns a Hot Observable derived from the Pushing event.
    /// Hot Observables produce values regardless of whether there are active subscriptions. The event fires
    /// before the popup is actually added to the navigation stack, allowing for interception or validation
    /// of navigation events.
    /// </remarks>
    public static IObservable<PopupNavigationEventArgs> PushingObservable(this IPopupNavigation service) =>
        Observable.FromEvent<EventHandler<PopupNavigationEventArgs>, PopupNavigationEventArgs>(
                    handler =>
                    {
                        void EventHandler(object? sender, PopupNavigationEventArgs args) => handler(args);
                        return EventHandler;
                    },
                    x => service.Pushing += x,
                    x => service.Pushing -= x);

    /// <summary>
    /// Observes when a popup page has been pushed onto the navigation stack.
    /// </summary>
    /// <param name="service">The popup navigation service instance.</param>
    /// <returns>An observable sequence that emits <see cref="PopupNavigationEventArgs"/> each time a popup has been presented.</returns>
    /// <remarks>
    /// This method returns a Hot Observable derived from the Pushed event.
    /// Hot Observables produce values regardless of whether there are active subscriptions. The event fires
    /// after the popup has been fully added to the navigation stack, allowing for tracking or initialization
    /// of completed navigation events.
    /// </remarks>
    public static IObservable<PopupNavigationEventArgs> PushedObservable(this IPopupNavigation service) =>
        Observable.FromEvent<EventHandler<PopupNavigationEventArgs>, PopupNavigationEventArgs>(
                    handler =>
                    {
                        void EventHandler(object? sender, PopupNavigationEventArgs args) => handler(args);
                        return EventHandler;
                    },
                    x => service.Pushed += x,
                    x => service.Pushed -= x);
}
