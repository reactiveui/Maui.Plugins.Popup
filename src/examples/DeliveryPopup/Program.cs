// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive.Threading.Tasks;
using Microsoft.Maui.Controls;
using Mopups.Interfaces;
using Mopups.Services;
using ReactiveUI.Maui.Plugins.Popup;

namespace DeliveryPopup;

/// <summary>Shows delivery notes through page navigation and through an injected popup service.</summary>
internal static class Program
{
    /// <summary>Checks popup binding and runs an in-memory navigation session.</summary>
    /// <returns>Completion after both navigation paths close their pages.</returns>
    internal static async Task Main()
    {
        var preview = new PreviewPopupNavigation();
        MopupService.SetInstance(preview);
        try
        {
            await ShowNotesAsync(new ContentPage().Navigation, preview, CancellationToken.None);
        }
        finally
        {
            MopupService.RestoreDefaultInstance();
        }
    }

    /// <summary>Opens delivery notes, observes changes to the stack and clears the stack before returning.</summary>
    /// <param name="navigation">The host page's navigation.</param>
    /// <param name="popups">The popup service used by the host.</param>
    /// <param name="cancellationToken">Cancels waiting for a navigation operation.</param>
    /// <returns>Completion after the last popup closes.</returns>
    /// <exception cref="InvalidOperationException">A page did not bind its note or the stack changed out of order.</exception>
    internal static async Task ShowNotesAsync(INavigation navigation, IPopupNavigation popups, CancellationToken cancellationToken)
    {
        const string Note = "Leave the parcel at reception.";
        var first = new DeliveryNotePage(Note);
        var second = new DeliveryNotePage("Signature required.");
        List<string> changes = [];
        using var opening = navigation.PushingObservable().Subscribe(_ => changes.Add("opening"));
        using var opened = popups.PushedObservable().Subscribe(_ => changes.Add("opened"));
        using var closing = popups.PoppingObservable().Subscribe(_ => changes.Add("closing"));
        using var closed = navigation.PoppedObservable().Subscribe(_ => changes.Add("closed"));

        var show = navigation.PushPopup(first);
        if (!Equals(first.BindingContext, Note) || changes.Count != 0)
        {
            throw new InvalidOperationException("A bound page should wait for a subscription before opening.");
        }

        _ = await show.ToTask(cancellationToken);
        _ = await popups.PopPopup(animate: false).ToTask(cancellationToken);
        if (changes is not ["opening", "opened", "closing", "closed"])
        {
            throw new InvalidOperationException("Each before event should precede its after event.");
        }

        _ = await popups.PushPopup(first, animate: false).ToTask(cancellationToken);
        _ = await navigation.PushPopup(second, animate: false).ToTask(cancellationToken);
        _ = await navigation.RemovePopupPage(first).ToTask(cancellationToken);
        _ = await popups.RemovePopupPage(second, animate: false).ToTask(cancellationToken);
        _ = await popups.PushPopup(first).ToTask(cancellationToken);
        _ = await navigation.PopAllPopup().ToTask(cancellationToken);
        if (popups.PopupStack.Count != 0)
        {
            throw new InvalidOperationException("All delivery popups should be closed.");
        }
    }
}
