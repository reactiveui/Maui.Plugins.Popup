// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="IPopupNavigationMixins"/>.
/// </summary>
public class IPopupNavigationMixinsTests
{
    private TestPopupNavigation _popupNavigation = null!;

    /// <summary>
    /// Sets up the test fixtures.
    /// </summary>
    [Before(Test)]
    public void SetUp() => _popupNavigation = new TestPopupNavigation();

    /// <summary>
    /// Tests that PopAllPopup returns observable that completes.
    /// </summary>
    [Test]
    public async Task PopAllPopup_WithDefaultAnimation_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PopAllPopup();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PopAllPopup calls service with correct animation parameter.
    /// </summary>
    /// <param name="animate">The animate flag.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task PopAllPopup_WithAnimationParameter_CallsServiceWithCorrectParameter(bool animate)
    {
        // Act
        await _popupNavigation.PopAllPopup(animate);

        // Assert
        await Assert.That(_popupNavigation.PopAllAsyncCalled).IsTrue();
        await Assert.That(_popupNavigation.LastAnimateValue).IsEqualTo(animate);
    }

    /// <summary>
    /// Tests that PopPopup returns observable that completes.
    /// </summary>
    [Test]
    public async Task PopPopup_WithDefaultAnimation_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PopPopup();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PopPopup calls service with correct animation parameter.
    /// </summary>
    /// <param name="animate">The animate flag.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task PopPopup_WithAnimationParameter_CallsServiceWithCorrectParameter(bool animate)
    {
        // Act
        await _popupNavigation.PopPopup(animate);

        // Assert
        await Assert.That(_popupNavigation.PopAsyncCalled).IsTrue();
        await Assert.That(_popupNavigation.LastAnimateValue).IsEqualTo(animate);
    }

    /// <summary>
    /// Tests that PushPopup returns observable that completes.
    /// </summary>
    [Test]
    public async Task PushPopup_WithPage_ReturnsObservable()
    {
        // Arrange
        var page = new PopupPage();

        // Act
        var result = _popupNavigation.PushPopup(page);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PushPopup calls service with correct parameters.
    /// </summary>
    /// <param name="animate">The animate flag.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task PushPopup_WithAnimationParameter_CallsServiceWithCorrectParameter(bool animate)
    {
        // Arrange
        var page = new PopupPage();

        // Act
        await _popupNavigation.PushPopup(page, animate);

        // Assert
        await Assert.That(_popupNavigation.PushAsyncCalled).IsTrue();
        await Assert.That(_popupNavigation.LastAnimateValue).IsEqualTo(animate);
        await Assert.That(_popupNavigation.LastPage).IsEqualTo(page);
    }

    /// <summary>
    /// Tests that RemovePopupPage returns observable that completes.
    /// </summary>
    [Test]
    public async Task RemovePopupPage_WithPage_ReturnsObservable()
    {
        // Arrange
        var page = new PopupPage();

        // Act
        var result = _popupNavigation.RemovePopupPage(page);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that RemovePopupPage calls service with correct parameters.
    /// </summary>
    /// <param name="animate">The animate flag.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task RemovePopupPage_WithAnimationParameter_CallsServiceWithCorrectParameter(bool animate)
    {
        // Arrange
        var page = new PopupPage();

        // Act
        await _popupNavigation.RemovePopupPage(page, animate);

        // Assert
        await Assert.That(_popupNavigation.RemovePageAsyncCalled).IsTrue();
        await Assert.That(_popupNavigation.LastAnimateValue).IsEqualTo(animate);
        await Assert.That(_popupNavigation.LastPage).IsEqualTo(page);
    }

    /// <summary>
    /// Tests that PoppingObservable returns an observable.
    /// </summary>
    [Test]
    public async Task PoppingObservable_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PoppingObservable();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PoppingObservable fires when event is raised.
    /// </summary>
    [Test]
    public async Task PoppingObservable_WhenEventRaised_EmitsEventArgs()
    {
        // Arrange
        var page = new PopupPage();
        var eventArgs = new PopupNavigationEventArgs(page, true);
        PopupNavigationEventArgs? receivedArgs = null;

        var observable = _popupNavigation.PoppingObservable();
        using var subscription = observable.Subscribe(args => receivedArgs = args);

        // Act
        _popupNavigation.RaisePopping(eventArgs);

        // Assert
        await Assert.That(receivedArgs).IsNotNull();
        await Assert.That(receivedArgs).IsEqualTo(eventArgs);
    }

    /// <summary>
    /// Tests that PoppedObservable returns an observable.
    /// </summary>
    [Test]
    public async Task PoppedObservable_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PoppedObservable();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PoppedObservable fires when event is raised.
    /// </summary>
    [Test]
    public async Task PoppedObservable_WhenEventRaised_EmitsEventArgs()
    {
        // Arrange
        var page = new PopupPage();
        var eventArgs = new PopupNavigationEventArgs(page, false);
        PopupNavigationEventArgs? receivedArgs = null;

        var observable = _popupNavigation.PoppedObservable();
        using var subscription = observable.Subscribe(args => receivedArgs = args);

        // Act
        _popupNavigation.RaisePopped(eventArgs);

        // Assert
        await Assert.That(receivedArgs).IsNotNull();
        await Assert.That(receivedArgs).IsEqualTo(eventArgs);
    }

    /// <summary>
    /// Tests that PushingObservable returns an observable.
    /// </summary>
    [Test]
    public async Task PushingObservable_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PushingObservable();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PushingObservable fires when event is raised.
    /// </summary>
    [Test]
    public async Task PushingObservable_WhenEventRaised_EmitsEventArgs()
    {
        // Arrange
        var page = new PopupPage();
        var eventArgs = new PopupNavigationEventArgs(page, true);
        PopupNavigationEventArgs? receivedArgs = null;

        var observable = _popupNavigation.PushingObservable();
        using var subscription = observable.Subscribe(args => receivedArgs = args);

        // Act
        _popupNavigation.RaisePushing(eventArgs);

        // Assert
        await Assert.That(receivedArgs).IsNotNull();
        await Assert.That(receivedArgs).IsEqualTo(eventArgs);
    }

    /// <summary>
    /// Tests that PushedObservable returns an observable.
    /// </summary>
    [Test]
    public async Task PushedObservable_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PushedObservable();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PushedObservable fires when event is raised.
    /// </summary>
    [Test]
    public async Task PushedObservable_WhenEventRaised_EmitsEventArgs()
    {
        // Arrange
        var page = new PopupPage();
        var eventArgs = new PopupNavigationEventArgs(page, false);
        PopupNavigationEventArgs? receivedArgs = null;

        var observable = _popupNavigation.PushedObservable();
        using var subscription = observable.Subscribe(args => receivedArgs = args);

        // Act
        _popupNavigation.RaisePushed(eventArgs);

        // Assert
        await Assert.That(receivedArgs).IsNotNull();
        await Assert.That(receivedArgs).IsEqualTo(eventArgs);
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the event.
    /// </summary>
    [Test]
    public async Task PoppingObservable_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new PopupPage();
        var eventArgs = new PopupNavigationEventArgs(page, true);
        var receivedCount = 0;

        var observable = _popupNavigation.PoppingObservable();
        var subscription = observable.Subscribe(_ => receivedCount++);

        // Act - First event should be received
        _popupNavigation.RaisePopping(eventArgs);
        subscription.Dispose();

        // Second event should not be received
        _popupNavigation.RaisePopping(eventArgs);

        // Assert
        await Assert.That(receivedCount).IsEqualTo(1);
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Popped event.
    /// </summary>
    [Test]
    public async Task PoppedObservable_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new PopupPage();
        var eventArgs = new PopupNavigationEventArgs(page, true);
        var receivedCount = 0;

        var observable = _popupNavigation.PoppedObservable();
        var subscription = observable.Subscribe(_ => receivedCount++);

        // Act
        _popupNavigation.RaisePopped(eventArgs);
        subscription.Dispose();
        _popupNavigation.RaisePopped(eventArgs);

        // Assert
        await Assert.That(receivedCount).IsEqualTo(1);
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Pushing event.
    /// </summary>
    [Test]
    public async Task PushingObservable_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new PopupPage();
        var eventArgs = new PopupNavigationEventArgs(page, true);
        var receivedCount = 0;

        var observable = _popupNavigation.PushingObservable();
        var subscription = observable.Subscribe(_ => receivedCount++);

        // Act
        _popupNavigation.RaisePushing(eventArgs);
        subscription.Dispose();
        _popupNavigation.RaisePushing(eventArgs);

        // Assert
        await Assert.That(receivedCount).IsEqualTo(1);
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Pushed event.
    /// </summary>
    [Test]
    public async Task PushedObservable_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new PopupPage();
        var eventArgs = new PopupNavigationEventArgs(page, true);
        var receivedCount = 0;

        var observable = _popupNavigation.PushedObservable();
        var subscription = observable.Subscribe(_ => receivedCount++);

        // Act
        _popupNavigation.RaisePushed(eventArgs);
        subscription.Dispose();
        _popupNavigation.RaisePushed(eventArgs);

        // Assert
        await Assert.That(receivedCount).IsEqualTo(1);
    }
}
