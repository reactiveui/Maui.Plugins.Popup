// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Mopups.Services;
using ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>Checks navigation through the configured popup service.</summary>
public sealed class INavigationMixinsTests
{
    /// <summary>The test mode that explicitly enables animation.</summary>
    private const int AnimatedMode = 2;

    /// <summary>PopAllPopup defers navigation and forwards its animation setting.</summary>
    /// <param name="mode">Zero uses the default; one disables animation; two enables it.</param>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    public async Task PopAllPopup_ForwardsOnSubscription(int mode)
    {
        var navigation = new TestNavigation();
        var service = (TestPopupNavigation)MopupService.Instance;
        var observable = mode == 0 ? navigation.PopAllPopup() : navigation.PopAllPopup(mode == AnimatedMode);
        await Assert.That(service.PopAllAsyncCalled).IsFalse();
        await observable.ToTask();
        await Assert.That(service.PopAllAsyncCalled).IsTrue();
        await Assert.That(service.LastAnimateValue).IsEqualTo(mode != 1);
    }

    /// <summary>PopPopup defers navigation and forwards its animation setting.</summary>
    /// <param name="mode">Zero uses the default; one disables animation; two enables it.</param>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    public async Task PopPopup_ForwardsOnSubscription(int mode)
    {
        var navigation = new TestNavigation();
        var service = (TestPopupNavigation)MopupService.Instance;
        var observable = mode == 0 ? navigation.PopPopup() : navigation.PopPopup(mode == AnimatedMode);
        await Assert.That(service.PopAsyncCalled).IsFalse();
        await observable.ToTask();
        await Assert.That(service.PopAsyncCalled).IsTrue();
        await Assert.That(service.LastAnimateValue).IsEqualTo(mode != 1);
    }

    /// <summary>PushPopup defers navigation and forwards its animation setting.</summary>
    /// <param name="mode">Zero uses the default; one disables animation; two enables it.</param>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    public async Task PushPopup_ForwardsOnSubscription(int mode)
    {
        var navigation = new TestNavigation();
        var service = (TestPopupNavigation)MopupService.Instance;
        var page = new PopupPage();
        var observable = mode == 0 ? navigation.PushPopup(page) : navigation.PushPopup(page, mode == AnimatedMode);
        await Assert.That(service.PushAsyncCalled).IsFalse();
        await observable.ToTask();
        await Assert.That(service.PushAsyncCalled).IsTrue();
        await Assert.That(service.LastAnimateValue).IsEqualTo(mode != 1);
        await Assert.That(service.LastPage).IsSameReferenceAs(page);
    }

    /// <summary>RemovePopupPage defers navigation and forwards its animation setting.</summary>
    /// <param name="mode">Zero uses the default; one disables animation; two enables it.</param>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    public async Task RemovePopupPage_ForwardsOnSubscription(int mode)
    {
        var navigation = new TestNavigation();
        var service = (TestPopupNavigation)MopupService.Instance;
        var page = new PopupPage();
        var observable = mode == 0 ? navigation.RemovePopupPage(page) : navigation.RemovePopupPage(page, mode == AnimatedMode);
        await Assert.That(service.RemovePageAsyncCalled).IsFalse();
        await observable.ToTask();
        await Assert.That(service.RemovePageAsyncCalled).IsTrue();
        await Assert.That(service.LastAnimateValue).IsEqualTo(mode != 1);
        await Assert.That(service.LastPage).IsSameReferenceAs(page);
    }

    /// <summary>Popping subscriptions forward event data and detach on disposal.</summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task PoppingObservable_ForwardsAndDetaches()
    {
        var navigation = new TestNavigation();
        var service = (TestPopupNavigation)MopupService.Instance;
        var notifications = new List<PopupNavigationEventArgs>();
        var eventArgs = new PopupNavigationEventArgs(new PopupPage(), true);
        var observable = navigation.PoppingObservable();
        var subscription = observable.Subscribe(notifications.Add);
        service.RaisePopping(eventArgs);
        subscription.Dispose();
        service.RaisePopping(eventArgs);
        await Assert.That(notifications.Count).IsEqualTo(1);
        await Assert.That(notifications[0]).IsSameReferenceAs(eventArgs);
    }

    /// <summary>Popped subscriptions forward event data and detach on disposal.</summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task PoppedObservable_ForwardsAndDetaches()
    {
        var navigation = new TestNavigation();
        var service = (TestPopupNavigation)MopupService.Instance;
        var notifications = new List<PopupNavigationEventArgs>();
        var eventArgs = new PopupNavigationEventArgs(new PopupPage(), true);
        var observable = navigation.PoppedObservable();
        var subscription = observable.Subscribe(notifications.Add);
        service.RaisePopped(eventArgs);
        subscription.Dispose();
        service.RaisePopped(eventArgs);
        await Assert.That(notifications.Count).IsEqualTo(1);
        await Assert.That(notifications[0]).IsSameReferenceAs(eventArgs);
    }

    /// <summary>Pushing subscriptions forward event data and detach on disposal.</summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task PushingObservable_ForwardsAndDetaches()
    {
        var navigation = new TestNavigation();
        var service = (TestPopupNavigation)MopupService.Instance;
        var notifications = new List<PopupNavigationEventArgs>();
        var eventArgs = new PopupNavigationEventArgs(new PopupPage(), true);
        var observable = navigation.PushingObservable();
        var subscription = observable.Subscribe(notifications.Add);
        service.RaisePushing(eventArgs);
        subscription.Dispose();
        service.RaisePushing(eventArgs);
        await Assert.That(notifications.Count).IsEqualTo(1);
        await Assert.That(notifications[0]).IsSameReferenceAs(eventArgs);
    }

    /// <summary>Pushed subscriptions forward event data and detach on disposal.</summary>
    /// <returns>A task representing the asynchronous test.</returns>
    [Test]
    public async Task PushedObservable_ForwardsAndDetaches()
    {
        var navigation = new TestNavigation();
        var service = (TestPopupNavigation)MopupService.Instance;
        var notifications = new List<PopupNavigationEventArgs>();
        var eventArgs = new PopupNavigationEventArgs(new PopupPage(), true);
        var observable = navigation.PushedObservable();
        var subscription = observable.Subscribe(notifications.Add);
        service.RaisePushed(eventArgs);
        subscription.Dispose();
        service.RaisePushed(eventArgs);
        await Assert.That(notifications.Count).IsEqualTo(1);
        await Assert.That(notifications[0]).IsSameReferenceAs(eventArgs);
    }
}
