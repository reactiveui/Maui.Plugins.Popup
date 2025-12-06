// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive.Linq;
using Mopups.Events;
using Mopups.Interfaces;
using Mopups.Pages;
using NUnit.Framework;
using ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="IPopupNavigationMixins"/>.
/// </summary>
[TestFixture]
public class IPopupNavigationMixinsTests
{
    private TestPopupNavigation _popupNavigation = null!;

    /// <summary>
    /// Sets up the test fixtures.
    /// </summary>
    [SetUp]
    public void SetUp() => _popupNavigation = new TestPopupNavigation();

    /// <summary>
    /// Tests that PopAllPopup returns observable that completes.
    /// </summary>
    [Test]
    public void PopAllPopup_WithDefaultAnimation_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PopAllPopup();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PopAllPopup calls service with correct animation parameter.
    /// </summary>
    /// <param name="animate">The animate flag.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task PopAllPopup_WithAnimationParameter_CallsServiceWithCorrectParameter(bool animate)
    {
        // Act
        await _popupNavigation.PopAllPopup(animate);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_popupNavigation.PopAllAsyncCalled, Is.True);
            Assert.That(_popupNavigation.LastAnimateValue, Is.EqualTo(animate));
        }
    }

    /// <summary>
    /// Tests that PopPopup returns observable that completes.
    /// </summary>
    [Test]
    public void PopPopup_WithDefaultAnimation_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PopPopup();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PopPopup calls service with correct animation parameter.
    /// </summary>
    /// <param name="animate">The animate flag.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task PopPopup_WithAnimationParameter_CallsServiceWithCorrectParameter(bool animate)
    {
        // Act
        await _popupNavigation.PopPopup(animate);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_popupNavigation.PopAsyncCalled, Is.True);
            Assert.That(_popupNavigation.LastAnimateValue, Is.EqualTo(animate));
        }
    }

    /// <summary>
    /// Tests that PushPopup returns observable that completes.
    /// </summary>
    [Test]
    public void PushPopup_WithPage_ReturnsObservable()
    {
        // Arrange
        var page = new PopupPage();

        // Act
        var result = _popupNavigation.PushPopup(page);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PushPopup calls service with correct parameters.
    /// </summary>
    /// <param name="animate">The animate flag.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task PushPopup_WithAnimationParameter_CallsServiceWithCorrectParameter(bool animate)
    {
        // Arrange
        var page = new PopupPage();

        // Act
        await _popupNavigation.PushPopup(page, animate);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_popupNavigation.PushAsyncCalled, Is.True);
            Assert.That(_popupNavigation.LastAnimateValue, Is.EqualTo(animate));
            Assert.That(_popupNavigation.LastPage, Is.EqualTo(page));
        }
    }

    /// <summary>
    /// Tests that RemovePopupPage returns observable that completes.
    /// </summary>
    [Test]
    public void RemovePopupPage_WithPage_ReturnsObservable()
    {
        // Arrange
        var page = new PopupPage();

        // Act
        var result = _popupNavigation.RemovePopupPage(page);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that RemovePopupPage calls service with correct parameters.
    /// </summary>
    /// <param name="animate">The animate flag.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task RemovePopupPage_WithAnimationParameter_CallsServiceWithCorrectParameter(bool animate)
    {
        // Arrange
        var page = new PopupPage();

        // Act
        await _popupNavigation.RemovePopupPage(page, animate);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_popupNavigation.RemovePageAsyncCalled, Is.True);
            Assert.That(_popupNavigation.LastAnimateValue, Is.EqualTo(animate));
            Assert.That(_popupNavigation.LastPage, Is.EqualTo(page));
        }
    }

    /// <summary>
    /// Tests that PoppingObservable returns an observable.
    /// </summary>
    [Test]
    public void PoppingObservable_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PoppingObservable();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PoppingObservable fires when event is raised.
    /// </summary>
    [Test]
    public void PoppingObservable_WhenEventRaised_EmitsEventArgs()
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
        using (Assert.EnterMultipleScope())
        {
            Assert.That(receivedArgs, Is.Not.Null);
            Assert.That(receivedArgs, Is.EqualTo(eventArgs));
        }
    }

    /// <summary>
    /// Tests that PoppedObservable returns an observable.
    /// </summary>
    [Test]
    public void PoppedObservable_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PoppedObservable();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PoppedObservable fires when event is raised.
    /// </summary>
    [Test]
    public void PoppedObservable_WhenEventRaised_EmitsEventArgs()
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
        Assert.That(receivedArgs, Is.Not.Null);
        Assert.That(receivedArgs, Is.EqualTo(eventArgs));
    }

    /// <summary>
    /// Tests that PushingObservable returns an observable.
    /// </summary>
    [Test]
    public void PushingObservable_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PushingObservable();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PushingObservable fires when event is raised.
    /// </summary>
    [Test]
    public void PushingObservable_WhenEventRaised_EmitsEventArgs()
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
        Assert.That(receivedArgs, Is.Not.Null);
        Assert.That(receivedArgs, Is.EqualTo(eventArgs));
    }

    /// <summary>
    /// Tests that PushedObservable returns an observable.
    /// </summary>
    [Test]
    public void PushedObservable_ReturnsObservable()
    {
        // Act
        var result = _popupNavigation.PushedObservable();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PushedObservable fires when event is raised.
    /// </summary>
    [Test]
    public void PushedObservable_WhenEventRaised_EmitsEventArgs()
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
        Assert.That(receivedArgs, Is.Not.Null);
        Assert.That(receivedArgs, Is.EqualTo(eventArgs));
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the event.
    /// </summary>
    [Test]
    public void PoppingObservable_WhenDisposed_DoesNotReceiveEvents()
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
        Assert.That(receivedCount, Is.EqualTo(1));
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Popped event.
    /// </summary>
    [Test]
    public void PoppedObservable_WhenDisposed_DoesNotReceiveEvents()
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
        Assert.That(receivedCount, Is.EqualTo(1));
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Pushing event.
    /// </summary>
    [Test]
    public void PushingObservable_WhenDisposed_DoesNotReceiveEvents()
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
        Assert.That(receivedCount, Is.EqualTo(1));
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Pushed event.
    /// </summary>
    [Test]
    public void PushedObservable_WhenDisposed_DoesNotReceiveEvents()
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
        Assert.That(receivedCount, Is.EqualTo(1));
    }
}
