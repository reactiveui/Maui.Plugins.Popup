// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Microsoft.Maui.Controls;
using ReactiveUI.Maui.Plugins.Popup;
using Splat;

namespace DeliveryPopup;

/// <summary>Displays the delivery note and tracks taps outside it while the page is visible.</summary>
internal sealed class DeliveryNotePage : ReactivePopupPage<string>
{
    /// <summary>Initializes a new instance of the <see cref="DeliveryNotePage"/> class.</summary>
    /// <param name="note">The delivery note shown in the popup.</param>
    internal DeliveryNotePage(string note)
    {
        ViewModel = note;
        Label label = new();
        label.SetBinding(Label.TextProperty, static (string value) => value);
        Content = label;
    }

    /// <inheritdoc/>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ControlBindings.Add(BackgroundClick.Subscribe(static _ => LogHost.Default.Info("Tapped outside the delivery note.")));
    }

    /// <inheritdoc/>
    protected override void OnDisappearing()
    {
        ControlBindings.Clear();
        base.OnDisappearing();
    }
}
