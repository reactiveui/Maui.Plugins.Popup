// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Mopups.Services;
using ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;
using Splat;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>Installs a fresh navigation service and resolver for each test.</summary>
public sealed class PopupNavigationTestExecutor : ITestExecutor
{
    /// <inheritdoc/>
    public async ValueTask ExecuteTest(TestContext context, Func<ValueTask> action)
    {
        var previousResolver = AppLocator.GetLocator();
        using var resolver = new ModernDependencyResolver();
        AppLocator.SetLocator(resolver);
        MopupService.SetInstance(new TestPopupNavigation());
        try
        {
            await action();
        }
        finally
        {
            MopupService.RestoreDefaultInstance();
            AppLocator.SetLocator(previousResolver);
        }
    }
}
