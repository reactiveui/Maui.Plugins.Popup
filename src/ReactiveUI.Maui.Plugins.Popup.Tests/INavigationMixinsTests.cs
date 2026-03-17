// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="INavigationMixins"/>.
/// </summary>
/// <remarks>
/// These tests verify the observable creation patterns.
/// The actual navigation operations depend on MopupService.Instance which is a static singleton.
/// </remarks>
public class INavigationMixinsTests
{
    private TestNavigation _navigation = null!;

    /// <summary>
    /// Sets up the test fixtures.
    /// </summary>
    [Before(Test)]
    public void SetUp() => _navigation = new TestNavigation();

    /// <summary>
    /// Tests that PopAllPopup returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PopAllPopup_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PopAllPopup();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PopAllPopup with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PopAllPopup_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PopAllPopup(animate: false);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PopPopup returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PopPopup_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PopPopup();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PopPopup with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PopPopup_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PopPopup(animate: false);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PushPopup returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PushPopup_WithPage_ReturnsNonNullObservable()
    {
        // Arrange
        var page = new Mopups.Pages.PopupPage();

        // Act
        var result = _navigation.PushPopup(page);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PushPopup with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PushPopup_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Arrange
        var page = new Mopups.Pages.PopupPage();

        // Act
        var result = _navigation.PushPopup(page, animate: false);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that RemovePopupPage returns a non-null observable.
    /// </summary>
    [Test]
    public async Task RemovePopupPage_WithPage_ReturnsNonNullObservable()
    {
        // Arrange
        var page = new Mopups.Pages.PopupPage();

        // Act
        var result = _navigation.RemovePopupPage(page);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that RemovePopupPage with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public async Task RemovePopupPage_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Arrange
        var page = new Mopups.Pages.PopupPage();

        // Act
        var result = _navigation.RemovePopupPage(page, animate: false);

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PoppingObservable returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PoppingObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PoppingObservable();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PoppedObservable returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PoppedObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PoppedObservable();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PushingObservable returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PushingObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PushingObservable();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that PushedObservable returns a non-null observable.
    /// </summary>
    [Test]
    public async Task PushedObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PushedObservable();

        // Assert
        await Assert.That(result).IsNotNull();
    }

    /// <summary>
    /// Tests that multiple subscriptions to PoppingObservable are independent.
    /// </summary>
    [Test]
    public async Task PoppingObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _navigation.PoppingObservable();
        var observable2 = _navigation.PoppingObservable();

        // Assert
        await Assert.That(observable1).IsNotNull();
        await Assert.That(observable2).IsNotNull();
        await Assert.That(observable1).IsNotSameReferenceAs(observable2);
    }

    /// <summary>
    /// Tests that multiple subscriptions to PoppedObservable are independent.
    /// </summary>
    [Test]
    public async Task PoppedObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _navigation.PoppedObservable();
        var observable2 = _navigation.PoppedObservable();

        // Assert
        await Assert.That(observable1).IsNotNull();
        await Assert.That(observable2).IsNotNull();
        await Assert.That(observable1).IsNotSameReferenceAs(observable2);
    }

    /// <summary>
    /// Tests that multiple subscriptions to PushingObservable are independent.
    /// </summary>
    [Test]
    public async Task PushingObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _navigation.PushingObservable();
        var observable2 = _navigation.PushingObservable();

        // Assert
        await Assert.That(observable1).IsNotNull();
        await Assert.That(observable2).IsNotNull();
        await Assert.That(observable1).IsNotSameReferenceAs(observable2);
    }

    /// <summary>
    /// Tests that multiple subscriptions to PushedObservable are independent.
    /// </summary>
    [Test]
    public async Task PushedObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _navigation.PushedObservable();
        var observable2 = _navigation.PushedObservable();

        // Assert
        await Assert.That(observable1).IsNotNull();
        await Assert.That(observable2).IsNotNull();
        await Assert.That(observable1).IsNotSameReferenceAs(observable2);
    }
}
