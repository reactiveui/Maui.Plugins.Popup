// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;

/// <summary>
/// Simple test implementation of INavigation.
/// </summary>
internal sealed class TestNavigation : INavigation
{
    /// <inheritdoc/>
    public IReadOnlyList<Page> ModalStack => new List<Page>();

    /// <inheritdoc/>
    public IReadOnlyList<Page> NavigationStack => new List<Page>();

    /// <inheritdoc/>
    public void InsertPageBefore(Page page, Page before)
    {
    }

    /// <inheritdoc/>
    public Task<Page> PopAsync() => Task.FromResult<Page>(null!);

    /// <inheritdoc/>
    public Task<Page> PopAsync(bool animated) => Task.FromResult<Page>(null!);

    /// <inheritdoc/>
    public Task<Page> PopModalAsync() => Task.FromResult<Page>(null!);

    /// <inheritdoc/>
    public Task<Page> PopModalAsync(bool animated) => Task.FromResult<Page>(null!);

    /// <inheritdoc/>
    public Task PopToRootAsync() => Task.CompletedTask;

    /// <inheritdoc/>
    public Task PopToRootAsync(bool animated) => Task.CompletedTask;

    /// <inheritdoc/>
    public Task PushAsync(Page page) => Task.CompletedTask;

    /// <inheritdoc/>
    public Task PushAsync(Page page, bool animated) => Task.CompletedTask;

    /// <inheritdoc/>
    public Task PushModalAsync(Page page) => Task.CompletedTask;

    /// <inheritdoc/>
    public Task PushModalAsync(Page page, bool animated) => Task.CompletedTask;

    /// <inheritdoc/>
    public void RemovePage(Page page)
    {
    }
}
