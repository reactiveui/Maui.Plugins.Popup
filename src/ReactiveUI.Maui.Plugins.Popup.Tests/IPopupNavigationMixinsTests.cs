// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive.Linq;
using FluentAssertions;
using Mopups.Events;
using Mopups.Interfaces;
using Mopups.Pages;
using Moq;
using NUnit.Framework;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="IPopupNavigationMixins"/>.
/// </summary>
[TestFixture]
public class IPopupNavigationMixinsTests
{
    private Mock<IPopupNavigation> _mockPopupNavigation = null!;

    /// <summary>
    /// Sets up the test fixtures.
    /// </summary>
    [SetUp]
    public void SetUp() => _mockPopupNavigation = new Mock<IPopupNavigation>();

    /// <summary>
    /// Tests that PopAllPopup returns observable that completes.
    /// </summary>
    [Test]
    public void PopAllPopup_WithDefaultAnimation_ReturnsObservable()
    {
        // Arrange
        _mockPopupNavigation.Setup(x => x.PopAllAsync(true))
            .Returns(Task.CompletedTask);

        // Act
        var result = _mockPopupNavigation.Object.PopAllPopup();

        // Assert
        result.Should().NotBeNull();
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
        // Arrange
        _mockPopupNavigation.Setup(x => x.PopAllAsync(animate))
            .Returns(Task.CompletedTask);

        // Act
        await _mockPopupNavigation.Object.PopAllPopup(animate);

        // Assert
        _mockPopupNavigation.Verify(x => x.PopAllAsync(animate), Times.Once);
    }

    /// <summary>
    /// Tests that PopPopup returns observable that completes.
    /// </summary>
    [Test]
    public void PopPopup_WithDefaultAnimation_ReturnsObservable()
    {
        // Arrange
        _mockPopupNavigation.Setup(x => x.PopAsync(true))
            .Returns(Task.CompletedTask);

        // Act
        var result = _mockPopupNavigation.Object.PopPopup();

        // Assert
        result.Should().NotBeNull();
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
        // Arrange
        _mockPopupNavigation.Setup(x => x.PopAsync(animate))
            .Returns(Task.CompletedTask);

        // Act
        await _mockPopupNavigation.Object.PopPopup(animate);

        // Assert
        _mockPopupNavigation.Verify(x => x.PopAsync(animate), Times.Once);
    }

    /// <summary>
    /// Tests that PushPopup returns observable that completes.
    /// </summary>
    [Test]
    public void PushPopup_WithPage_ReturnsObservable()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        _mockPopupNavigation.Setup(x => x.PushAsync(page, true))
            .Returns(Task.CompletedTask);

        // Act
        var result = _mockPopupNavigation.Object.PushPopup(page);

        // Assert
        result.Should().NotBeNull();
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
        var page = new Mock<PopupPage>().Object;
        _mockPopupNavigation.Setup(x => x.PushAsync(page, animate))
            .Returns(Task.CompletedTask);

        // Act
        await _mockPopupNavigation.Object.PushPopup(page, animate);

        // Assert
        _mockPopupNavigation.Verify(x => x.PushAsync(page, animate), Times.Once);
    }

    /// <summary>
    /// Tests that RemovePopupPage returns observable that completes.
    /// </summary>
    [Test]
    public void RemovePopupPage_WithPage_ReturnsObservable()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        _mockPopupNavigation.Setup(x => x.RemovePageAsync(page, true))
            .Returns(Task.CompletedTask);

        // Act
        var result = _mockPopupNavigation.Object.RemovePopupPage(page);

        // Assert
        result.Should().NotBeNull();
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
        var page = new Mock<PopupPage>().Object;
        _mockPopupNavigation.Setup(x => x.RemovePageAsync(page, animate))
            .Returns(Task.CompletedTask);

        // Act
        await _mockPopupNavigation.Object.RemovePopupPage(page, animate);

        // Assert
        _mockPopupNavigation.Verify(x => x.RemovePageAsync(page, animate), Times.Once);
    }

    /// <summary>
    /// Tests that PoppingObservable returns an observable.
    /// </summary>
    [Test]
    public void PoppingObservable_ReturnsObservable()
    {
        // Act
        var result = _mockPopupNavigation.Object.PoppingObservable();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PoppingObservable fires when event is raised.
    /// </summary>
    [Test]
    public void PoppingObservable_WhenEventRaised_EmitsEventArgs()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        var eventArgs = new PopupNavigationEventArgs(page, true);
        PopupNavigationEventArgs? receivedArgs = null;

        var observable = _mockPopupNavigation.Object.PoppingObservable();
        using var subscription = observable.Subscribe(args => receivedArgs = args);

        // Act
        _mockPopupNavigation.Raise(x => x.Popping += null!, this, eventArgs);

        // Assert
        receivedArgs.Should().NotBeNull();
        receivedArgs.Should().Be(eventArgs);
    }

    /// <summary>
    /// Tests that PoppedObservable returns an observable.
    /// </summary>
    [Test]
    public void PoppedObservable_ReturnsObservable()
    {
        // Act
        var result = _mockPopupNavigation.Object.PoppedObservable();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PoppedObservable fires when event is raised.
    /// </summary>
    [Test]
    public void PoppedObservable_WhenEventRaised_EmitsEventArgs()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        var eventArgs = new PopupNavigationEventArgs(page, false);
        PopupNavigationEventArgs? receivedArgs = null;

        var observable = _mockPopupNavigation.Object.PoppedObservable();
        using var subscription = observable.Subscribe(args => receivedArgs = args);

        // Act
        _mockPopupNavigation.Raise(x => x.Popped += null!, this, eventArgs);

        // Assert
        receivedArgs.Should().NotBeNull();
        receivedArgs.Should().Be(eventArgs);
    }

    /// <summary>
    /// Tests that PushingObservable returns an observable.
    /// </summary>
    [Test]
    public void PushingObservable_ReturnsObservable()
    {
        // Act
        var result = _mockPopupNavigation.Object.PushingObservable();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PushingObservable fires when event is raised.
    /// </summary>
    [Test]
    public void PushingObservable_WhenEventRaised_EmitsEventArgs()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        var eventArgs = new PopupNavigationEventArgs(page, true);
        PopupNavigationEventArgs? receivedArgs = null;

        var observable = _mockPopupNavigation.Object.PushingObservable();
        using var subscription = observable.Subscribe(args => receivedArgs = args);

        // Act
        _mockPopupNavigation.Raise(x => x.Pushing += null!, this, eventArgs);

        // Assert
        receivedArgs.Should().NotBeNull();
        receivedArgs.Should().Be(eventArgs);
    }

    /// <summary>
    /// Tests that PushedObservable returns an observable.
    /// </summary>
    [Test]
    public void PushedObservable_ReturnsObservable()
    {
        // Act
        var result = _mockPopupNavigation.Object.PushedObservable();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PushedObservable fires when event is raised.
    /// </summary>
    [Test]
    public void PushedObservable_WhenEventRaised_EmitsEventArgs()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        var eventArgs = new PopupNavigationEventArgs(page, false);
        PopupNavigationEventArgs? receivedArgs = null;

        var observable = _mockPopupNavigation.Object.PushedObservable();
        using var subscription = observable.Subscribe(args => receivedArgs = args);

        // Act
        _mockPopupNavigation.Raise(x => x.Pushed += null!, this, eventArgs);

        // Assert
        receivedArgs.Should().NotBeNull();
        receivedArgs.Should().Be(eventArgs);
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the event.
    /// </summary>
    [Test]
    public void PoppingObservable_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        var eventArgs = new PopupNavigationEventArgs(page, true);
        var receivedCount = 0;

        var observable = _mockPopupNavigation.Object.PoppingObservable();
        var subscription = observable.Subscribe(_ => receivedCount++);

        // Act - First event should be received
        _mockPopupNavigation.Raise(x => x.Popping += null!, this, eventArgs);
        subscription.Dispose();

        // Second event should not be received
        _mockPopupNavigation.Raise(x => x.Popping += null!, this, eventArgs);

        // Assert
        receivedCount.Should().Be(1);
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Popped event.
    /// </summary>
    [Test]
    public void PoppedObservable_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        var eventArgs = new PopupNavigationEventArgs(page, true);
        var receivedCount = 0;

        var observable = _mockPopupNavigation.Object.PoppedObservable();
        var subscription = observable.Subscribe(_ => receivedCount++);

        // Act
        _mockPopupNavigation.Raise(x => x.Popped += null!, this, eventArgs);
        subscription.Dispose();
        _mockPopupNavigation.Raise(x => x.Popped += null!, this, eventArgs);

        // Assert
        receivedCount.Should().Be(1);
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Pushing event.
    /// </summary>
    [Test]
    public void PushingObservable_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        var eventArgs = new PopupNavigationEventArgs(page, true);
        var receivedCount = 0;

        var observable = _mockPopupNavigation.Object.PushingObservable();
        var subscription = observable.Subscribe(_ => receivedCount++);

        // Act
        _mockPopupNavigation.Raise(x => x.Pushing += null!, this, eventArgs);
        subscription.Dispose();
        _mockPopupNavigation.Raise(x => x.Pushing += null!, this, eventArgs);

        // Assert
        receivedCount.Should().Be(1);
    }

    /// <summary>
    /// Tests that disposing the subscription unsubscribes from the Pushed event.
    /// </summary>
    [Test]
    public void PushedObservable_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new Mock<PopupPage>().Object;
        var eventArgs = new PopupNavigationEventArgs(page, true);
        var receivedCount = 0;

        var observable = _mockPopupNavigation.Object.PushedObservable();
        var subscription = observable.Subscribe(_ => receivedCount++);

        // Act
        _mockPopupNavigation.Raise(x => x.Pushed += null!, this, eventArgs);
        subscription.Dispose();
        _mockPopupNavigation.Raise(x => x.Pushed += null!, this, eventArgs);

        // Assert
        receivedCount.Should().Be(1);
    }
}
