// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="INavigationMixins"/>.
/// </summary>
/// <remarks>
/// These tests verify the observable creation patterns.
/// The actual navigation operations depend on MopupService.Instance which is a static singleton.
/// </remarks>
[TestFixture]
public class INavigationMixinsTests
{
    private Mock<INavigation> _mockNavigation = null!;

    /// <summary>
    /// Sets up the test fixtures.
    /// </summary>
    [SetUp]
    public void SetUp() => _mockNavigation = new Mock<INavigation>();

    /// <summary>
    /// Tests that PopAllPopup returns a non-null observable.
    /// </summary>
    [Test]
    public void PopAllPopup_ReturnsNonNullObservable()
    {
        // Act
        var result = _mockNavigation.Object.PopAllPopup();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PopAllPopup with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public void PopAllPopup_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Act
        var result = _mockNavigation.Object.PopAllPopup(animate: false);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PopPopup returns a non-null observable.
    /// </summary>
    [Test]
    public void PopPopup_ReturnsNonNullObservable()
    {
        // Act
        var result = _mockNavigation.Object.PopPopup();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PopPopup with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public void PopPopup_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Act
        var result = _mockNavigation.Object.PopPopup(animate: false);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PushPopup returns a non-null observable.
    /// </summary>
    [Test]
    public void PushPopup_WithPage_ReturnsNonNullObservable()
    {
        // Arrange
        var page = new Mopups.Pages.PopupPage();

        // Act
        var result = _mockNavigation.Object.PushPopup(page);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PushPopup with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public void PushPopup_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Arrange
        var page = new Mopups.Pages.PopupPage();

        // Act
        var result = _mockNavigation.Object.PushPopup(page, animate: false);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that RemovePopupPage returns a non-null observable.
    /// </summary>
    [Test]
    public void RemovePopupPage_WithPage_ReturnsNonNullObservable()
    {
        // Arrange
        var page = new Mopups.Pages.PopupPage();

        // Act
        var result = _mockNavigation.Object.RemovePopupPage(page);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that RemovePopupPage with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public void RemovePopupPage_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Arrange
        var page = new Mopups.Pages.PopupPage();

        // Act
        var result = _mockNavigation.Object.RemovePopupPage(page, animate: false);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PoppingObservable returns a non-null observable.
    /// </summary>
    [Test]
    public void PoppingObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _mockNavigation.Object.PoppingObservable();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PoppedObservable returns a non-null observable.
    /// </summary>
    [Test]
    public void PoppedObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _mockNavigation.Object.PoppedObservable();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PushingObservable returns a non-null observable.
    /// </summary>
    [Test]
    public void PushingObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _mockNavigation.Object.PushingObservable();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that PushedObservable returns a non-null observable.
    /// </summary>
    [Test]
    public void PushedObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _mockNavigation.Object.PushedObservable();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that multiple subscriptions to PoppingObservable are independent.
    /// </summary>
    [Test]
    public void PoppingObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _mockNavigation.Object.PoppingObservable();
        var observable2 = _mockNavigation.Object.PoppingObservable();

        // Assert
        observable1.Should().NotBeNull();
        observable2.Should().NotBeNull();
        observable1.Should().NotBeSameAs(observable2);
    }

    /// <summary>
    /// Tests that multiple subscriptions to PoppedObservable are independent.
    /// </summary>
    [Test]
    public void PoppedObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _mockNavigation.Object.PoppedObservable();
        var observable2 = _mockNavigation.Object.PoppedObservable();

        // Assert
        observable1.Should().NotBeNull();
        observable2.Should().NotBeNull();
        observable1.Should().NotBeSameAs(observable2);
    }

    /// <summary>
    /// Tests that multiple subscriptions to PushingObservable are independent.
    /// </summary>
    [Test]
    public void PushingObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _mockNavigation.Object.PushingObservable();
        var observable2 = _mockNavigation.Object.PushingObservable();

        // Assert
        observable1.Should().NotBeNull();
        observable2.Should().NotBeNull();
        observable1.Should().NotBeSameAs(observable2);
    }

    /// <summary>
    /// Tests that multiple subscriptions to PushedObservable are independent.
    /// </summary>
    [Test]
    public void PushedObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _mockNavigation.Object.PushedObservable();
        var observable2 = _mockNavigation.Object.PushedObservable();

        // Assert
        observable1.Should().NotBeNull();
        observable2.Should().NotBeNull();
        observable1.Should().NotBeSameAs(observable2);
    }
}
