// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;

/// <summary>Simple test implementation of INavigation.</summary>
internal sealed class TestNavigation : INavigation
{
    /// <inheritdoc/>
    public IReadOnlyList<Page> ModalStack => [];

    /// <inheritdoc/>
    public IReadOnlyList<Page> NavigationStack => [];

    /// <inheritdoc/>
    public void InsertPageBefore(Page page, Page before)
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Page> PopAsync() => Task.FromResult<Page>(null!);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Page> PopAsync(bool animated) => Task.FromResult<Page>(null!);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Page> PopModalAsync() => PopAsync();

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Page> PopModalAsync(bool animated) => PopAsync(animated);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task PopToRootAsync() => Task.CompletedTask;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task PopToRootAsync(bool animated) => Task.CompletedTask;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task PushAsync(Page page) => Task.CompletedTask;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task PushAsync(Page page, bool animated) => Task.CompletedTask;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task PushModalAsync(Page page) => Task.CompletedTask;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task PushModalAsync(Page page, bool animated) => Task.CompletedTask;

    /// <inheritdoc/>
    public void RemovePage(Page page)
    {
    }
}
