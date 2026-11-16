// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Mopups.Events;
using Mopups.Interfaces;
using Mopups.Pages;

namespace ReactiveUI.Maui.Plugins.Popup;

/// <summary>Provides observable popup navigation operations.</summary>
public static class IPopupNavigationMixins
{
    /// <summary>Provides popup operations for the navigation service.</summary>
    /// <param name="service">The navigation service.</param>
    extension(IPopupNavigation service)
    {
        /// <summary>Removes all popups on subscription.</summary>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PopAllPopup() => service.PopAllPopup(true);

        /// <summary>Removes all popups on subscription.</summary>
        /// <param name="animate">Whether to animate the operation.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PopAllPopup(bool animate)
        {
            ArgumentNullException.ThrowIfNull(service);
            return Observable.FromAsync(() => service.PopAllAsync(animate));
        }

        /// <summary>Removes the top popup on subscription.</summary>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PopPopup() => service.PopPopup(true);

        /// <summary>Removes the top popup on subscription.</summary>
        /// <param name="animate">Whether to animate the operation.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PopPopup(bool animate)
        {
            ArgumentNullException.ThrowIfNull(service);
            return Observable.FromAsync(() => service.PopAsync(animate));
        }

        /// <summary>Displays the popup on subscription.</summary>
        /// <typeparam name="T">The popup page type.</typeparam>
        /// <param name="page">The popup page.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PushPopup<T>(T page)
            where T : PopupPage => service.PushPopup(page, true);

        /// <summary>Displays the popup on subscription.</summary>
        /// <typeparam name="T">The popup page type.</typeparam>
        /// <param name="page">The popup page.</param>
        /// <param name="animate">Whether to animate the operation.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PushPopup<T>(T page, bool animate)
            where T : PopupPage
        {
            ArgumentNullException.ThrowIfNull(service);
            ArgumentNullException.ThrowIfNull(page);
            return Observable.FromAsync(() => service.PushAsync(page, animate));
        }

        /// <summary>Removes the specified popup on subscription.</summary>
        /// <typeparam name="T">The popup page type.</typeparam>
        /// <param name="page">The popup page.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> RemovePopupPage<T>(T page)
            where T : PopupPage => service.RemovePopupPage(page, true);

        /// <summary>Removes the specified popup on subscription.</summary>
        /// <typeparam name="T">The popup page type.</typeparam>
        /// <param name="page">The popup page.</param>
        /// <param name="animate">Whether to animate the operation.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> RemovePopupPage<T>(T page, bool animate)
            where T : PopupPage
        {
            ArgumentNullException.ThrowIfNull(service);
            ArgumentNullException.ThrowIfNull(page);
            return Observable.FromAsync(() => service.RemovePageAsync(page, animate));
        }

        /// <summary>Observes the Popping popup event.</summary>
        /// <returns>The popup navigation events until disposal.</returns>
        public IObservable<PopupNavigationEventArgs> PoppingObservable()
        {
            ArgumentNullException.ThrowIfNull(service);
            return Observable.FromEvent<EventHandler<PopupNavigationEventArgs>, PopupNavigationEventArgs>(
                static handler => (_, args) => handler(args),
                handler => service.Popping += handler,
                handler => service.Popping -= handler);
        }

        /// <summary>Observes the Popped popup event.</summary>
        /// <returns>The popup navigation events until disposal.</returns>
        public IObservable<PopupNavigationEventArgs> PoppedObservable()
        {
            ArgumentNullException.ThrowIfNull(service);
            return Observable.FromEvent<EventHandler<PopupNavigationEventArgs>, PopupNavigationEventArgs>(
                static handler => (_, args) => handler(args),
                handler => service.Popped += handler,
                handler => service.Popped -= handler);
        }

        /// <summary>Observes the Pushing popup event.</summary>
        /// <returns>The popup navigation events until disposal.</returns>
        public IObservable<PopupNavigationEventArgs> PushingObservable()
        {
            ArgumentNullException.ThrowIfNull(service);
            return Observable.FromEvent<EventHandler<PopupNavigationEventArgs>, PopupNavigationEventArgs>(
                static handler => (_, args) => handler(args),
                handler => service.Pushing += handler,
                handler => service.Pushing -= handler);
        }

        /// <summary>Observes the Pushed popup event.</summary>
        /// <returns>The popup navigation events until disposal.</returns>
        public IObservable<PopupNavigationEventArgs> PushedObservable()
        {
            ArgumentNullException.ThrowIfNull(service);
            return Observable.FromEvent<EventHandler<PopupNavigationEventArgs>, PopupNavigationEventArgs>(
                static handler => (_, args) => handler(args),
                handler => service.Pushed += handler,
                handler => service.Pushed -= handler);
        }

    }
}
