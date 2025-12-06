// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Mopups.Events;
using Mopups.Interfaces;
using Mopups.Pages;

namespace ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;

/// <summary>
/// Test implementation of IPopupNavigation for testing purposes.
/// </summary>
internal sealed class TestPopupNavigation : IPopupNavigation
{
    /// <inheritdoc/>
    public event EventHandler<PopupNavigationEventArgs>? Popping;

    /// <inheritdoc/>
    public event EventHandler<PopupNavigationEventArgs>? Popped;

    /// <inheritdoc/>
    public event EventHandler<PopupNavigationEventArgs>? Pushing;

    /// <inheritdoc/>
    public event EventHandler<PopupNavigationEventArgs>? Pushed;

    /// <summary>
    /// Gets a value indicating whether PopAllAsync was called.
    /// </summary>
    public bool PopAllAsyncCalled { get; private set; }

    /// <summary>
    /// Gets a value indicating whether PopAsync was called.
    /// </summary>
    public bool PopAsyncCalled { get; private set; }

    /// <summary>
    /// Gets a value indicating whether PushAsync was called.
    /// </summary>
    public bool PushAsyncCalled { get; private set; }

    /// <summary>
    /// Gets a value indicating whether RemovePageAsync was called.
    /// </summary>
    public bool RemovePageAsyncCalled { get; private set; }

    /// <summary>
    /// Gets a value indicating whether animation was enabled in the last call.
    /// </summary>
    public bool LastAnimateValue { get; private set; }

    /// <summary>
    /// Gets the last page passed to a method.
    /// </summary>
    public PopupPage? LastPage { get; private set; }

    /// <inheritdoc/>
    public IReadOnlyList<PopupPage> PopupStack { get; } = new List<PopupPage>();

    /// <inheritdoc/>
    public Task PopAllAsync(bool animate = true)
    {
        PopAllAsyncCalled = true;
        LastAnimateValue = animate;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task PopAsync(bool animate = true)
    {
        PopAsyncCalled = true;
        LastAnimateValue = animate;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task PushAsync(PopupPage page, bool animate = true)
    {
        PushAsyncCalled = true;
        LastPage = page;
        LastAnimateValue = animate;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task RemovePageAsync(PopupPage page, bool animate = true)
    {
        RemovePageAsyncCalled = true;
        LastPage = page;
        LastAnimateValue = animate;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Raises the Pushing event for testing.
    /// </summary>
    /// <param name="args">Event arguments.</param>
    public void RaisePushing(PopupNavigationEventArgs args) => Pushing?.Invoke(this, args);

    /// <summary>
    /// Raises the Pushed event for testing.
    /// </summary>
    /// <param name="args">Event arguments.</param>
    public void RaisePushed(PopupNavigationEventArgs args) => Pushed?.Invoke(this, args);

    /// <summary>
    /// Raises the Popping event for testing.
    /// </summary>
    /// <param name="args">Event arguments.</param>
    public void RaisePopping(PopupNavigationEventArgs args) => Popping?.Invoke(this, args);

    /// <summary>
    /// Raises the Popped event for testing.
    /// </summary>
    /// <param name="args">Event arguments.</param>
    public void RaisePopped(PopupNavigationEventArgs args) => Popped?.Invoke(this, args);
}
