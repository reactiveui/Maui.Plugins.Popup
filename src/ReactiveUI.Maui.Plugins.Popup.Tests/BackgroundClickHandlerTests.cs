// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>Checks conversion of background clicks to observable notifications.</summary>
public sealed class BackgroundClickHandlerTests
{
    /// <summary>Independent subscriptions attach to and detach from the popup event.</summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task BackgroundClick_AllowsIndependentSubscriptions()
    {
        var page = new TestPopupPage();
        using var first = page.BackgroundClick.Subscribe(static _ => { });
        using var second = page.BackgroundClick.Subscribe(static _ => { });
        await Assert.That(first).IsNotSameReferenceAs(second);
    }

    /// <summary>Each click forwards exactly one unit notification.</summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task Handler_ForwardsEveryClick()
    {
        const int expectedNotifications = 2;
        var notifications = new List<Unit>();
        var handler = ReactivePopupPage.CreateBackgroundClickHandler(notifications.Add);
        handler(null, EventArgs.Empty);
        handler(new(), EventArgs.Empty);
        await Assert.That(notifications.Count).IsEqualTo(expectedNotifications);
        await Assert.That(notifications[0]).IsEqualTo(Unit.Default);
        await Assert.That(notifications[1]).IsEqualTo(Unit.Default);
    }

    /// <summary>A concrete popup for subscribing to background events.</summary>
    private sealed class TestPopupPage : ReactivePopupPage;
}
