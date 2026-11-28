// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using Mopups.Events;
using Mopups.Interfaces;
using Mopups.Pages;

namespace DeliveryPopup;

/// <summary>Keeps the popup stack in memory so the console example can run without opening a native window.</summary>
internal sealed class PreviewPopupNavigation : IPopupNavigation
{
    /// <summary>The open pages in navigation order.</summary>
    private readonly List<PopupPage> _pages = [];

    /// <inheritdoc/>
    public event EventHandler<PopupNavigationEventArgs>? Pushing;

    /// <inheritdoc/>
    public event EventHandler<PopupNavigationEventArgs>? Pushed;

    /// <inheritdoc/>
    public event EventHandler<PopupNavigationEventArgs>? Popping;

    /// <inheritdoc/>
    public event EventHandler<PopupNavigationEventArgs>? Popped;

    /// <inheritdoc/>
    public IReadOnlyList<PopupPage> PopupStack => _pages;

    /// <inheritdoc/>
    public Task PushAsync(PopupPage page, bool animate)
    {
        PopupNavigationEventArgs change = new(page, animate);
        Pushing?.Invoke(this, change);
        _pages.Add(page);
        Pushed?.Invoke(this, change);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task RemovePageAsync(PopupPage page, bool animate)
    {
        PopupNavigationEventArgs change = new(page, animate);
        Popping?.Invoke(this, change);
        _ = _pages.Remove(page);
        Popped?.Invoke(this, change);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task PopAsync(bool animate) => RemovePageAsync(_pages[^1], animate);

    /// <inheritdoc/>
    public async Task PopAllAsync(bool animate)
    {
        while (_pages.Count > 0)
        {
            await PopAsync(animate);
        }
    }
}
