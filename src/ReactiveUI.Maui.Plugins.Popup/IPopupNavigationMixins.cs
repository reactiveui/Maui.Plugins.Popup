// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
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
    /// <para>
    /// This method removes all popup pages from the popup navigation stack in a single operation. The popup stack
    /// operates independently from the main MAUI navigation stack, allowing popups to be displayed and dismissed
    /// without affecting the underlying page hierarchy. All popups are removed in reverse order (last-in, first-out).
    /// </para>
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
    /// <para>
    /// This method removes only the topmost popup from the popup navigation stack using a last-in, first-out (LIFO) approach.
    /// The popup stack is separate from the main MAUI navigation stack, allowing overlay content to be managed
    /// independently. If no popups are present in the stack, the operation completes without error.
    /// </para>
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
    /// <para>
    /// This method adds a new popup to the top of the popup navigation stack. The popup stack is maintained
    /// separately from the main MAUI navigation stack (<see cref="INavigation.NavigationStack"/>), allowing
    /// popups to be displayed as overlays on top of the current page without navigating away from it.
    /// Multiple popups can be stacked, and they are managed in a last-in, first-out (LIFO) order.
    /// </para>
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
    /// <para>
    /// Unlike <see cref="PopPopup(IPopupNavigation, bool)"/>, this method removes a specific popup page from anywhere
    /// in the popup stack, not just the topmost one. This is useful for dismissing a particular popup when multiple
    /// popups are displayed, or when you need to remove a popup that is not at the top of the stack.
    /// </para>
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
