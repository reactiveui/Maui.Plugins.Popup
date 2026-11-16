// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Mopups.Events;
using Mopups.Pages;
using Mopups.Services;

namespace ReactiveUI.Maui.Plugins.Popup;

/// <summary>Provides observable popup navigation operations.</summary>
public static class INavigationMixins
{
    /// <summary>Provides popup operations for the navigation service.</summary>
    /// <param name="navigation">The navigation service.</param>
    extension(INavigation navigation)
    {
        /// <summary>Removes all popups on subscription.</summary>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PopAllPopup() => navigation.PopAllPopup(true);

        /// <summary>Removes all popups on subscription.</summary>
        /// <param name="animate">Whether to animate the operation.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PopAllPopup(bool animate)
        {
            ArgumentNullException.ThrowIfNull(navigation);
            return Observable.Defer(() => MopupService.Instance.PopAllPopup(animate));
        }

        /// <summary>Removes the top popup on subscription.</summary>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PopPopup() => navigation.PopPopup(true);

        /// <summary>Removes the top popup on subscription.</summary>
        /// <param name="animate">Whether to animate the operation.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PopPopup(bool animate)
        {
            ArgumentNullException.ThrowIfNull(navigation);
            return Observable.Defer(() => MopupService.Instance.PopPopup(animate));
        }

        /// <summary>Displays the popup on subscription.</summary>
        /// <typeparam name="T">The popup page type.</typeparam>
        /// <param name="page">The popup page.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PushPopup<T>(T page)
            where T : PopupPage => navigation.PushPopup(page, true);

        /// <summary>Displays the popup on subscription.</summary>
        /// <typeparam name="T">The popup page type.</typeparam>
        /// <param name="page">The popup page.</param>
        /// <param name="animate">Whether to animate the operation.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> PushPopup<T>(T page, bool animate)
            where T : PopupPage
        {
            ArgumentNullException.ThrowIfNull(navigation);
            ArgumentNullException.ThrowIfNull(page);
            return Observable.Defer(() => MopupService.Instance.PushPopup(page, animate));
        }

        /// <summary>Removes the specified popup on subscription.</summary>
        /// <typeparam name="T">The popup page type.</typeparam>
        /// <param name="page">The popup page.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> RemovePopupPage<T>(T page)
            where T : PopupPage => navigation.RemovePopupPage(page, true);

        /// <summary>Removes the specified popup on subscription.</summary>
        /// <typeparam name="T">The popup page type.</typeparam>
        /// <param name="page">The popup page.</param>
        /// <param name="animate">Whether to animate the operation.</param>
        /// <returns>An observable that completes when navigation finishes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IObservable<Unit> RemovePopupPage<T>(T page, bool animate)
            where T : PopupPage
        {
            ArgumentNullException.ThrowIfNull(navigation);
            ArgumentNullException.ThrowIfNull(page);
            return Observable.Defer(() => MopupService.Instance.RemovePopupPage(page, animate));
        }

        /// <summary>Observes the Popping popup event.</summary>
        /// <returns>The popup navigation events until disposal.</returns>
        public IObservable<PopupNavigationEventArgs> PoppingObservable()
        {
            ArgumentNullException.ThrowIfNull(navigation);
            return Observable.Defer(static () => MopupService.Instance.PoppingObservable());
        }

        /// <summary>Observes the Popped popup event.</summary>
        /// <returns>The popup navigation events until disposal.</returns>
        public IObservable<PopupNavigationEventArgs> PoppedObservable()
        {
            ArgumentNullException.ThrowIfNull(navigation);
            return Observable.Defer(static () => MopupService.Instance.PoppedObservable());
        }

        /// <summary>Observes the Pushing popup event.</summary>
        /// <returns>The popup navigation events until disposal.</returns>
        public IObservable<PopupNavigationEventArgs> PushingObservable()
        {
            ArgumentNullException.ThrowIfNull(navigation);
            return Observable.Defer(static () => MopupService.Instance.PushingObservable());
        }

        /// <summary>Observes the Pushed popup event.</summary>
        /// <returns>The popup navigation events until disposal.</returns>
        public IObservable<PopupNavigationEventArgs> PushedObservable()
        {
            ArgumentNullException.ThrowIfNull(navigation);
            return Observable.Defer(static () => MopupService.Instance.PushedObservable());
        }

    }
}
