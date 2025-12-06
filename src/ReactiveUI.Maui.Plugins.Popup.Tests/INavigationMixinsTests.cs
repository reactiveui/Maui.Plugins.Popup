// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using NUnit.Framework;
using ReactiveUI.Maui.Plugins.Popup.Tests.Mocks;

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
    private TestNavigation _navigation = null!;

    /// <summary>
    /// Sets up the test fixtures.
    /// </summary>
    [SetUp]
    public void SetUp() => _navigation = new TestNavigation();

    /// <summary>
    /// Tests that PopAllPopup returns a non-null observable.
    /// </summary>
    [Test]
    public void PopAllPopup_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PopAllPopup();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PopAllPopup with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public void PopAllPopup_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PopAllPopup(animate: false);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PopPopup returns a non-null observable.
    /// </summary>
    [Test]
    public void PopPopup_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PopPopup();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PopPopup with animate false returns a non-null observable.
    /// </summary>
    [Test]
    public void PopPopup_WithAnimateFalse_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PopPopup(animate: false);

        // Assert
        Assert.That(result, Is.Not.Null);
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
        var result = _navigation.PushPopup(page);

        // Assert
        Assert.That(result, Is.Not.Null);
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
        var result = _navigation.PushPopup(page, animate: false);

        // Assert
        Assert.That(result, Is.Not.Null);
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
        var result = _navigation.RemovePopupPage(page);

        // Assert
        Assert.That(result, Is.Not.Null);
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
        var result = _navigation.RemovePopupPage(page, animate: false);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PoppingObservable returns a non-null observable.
    /// </summary>
    [Test]
    public void PoppingObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PoppingObservable();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PoppedObservable returns a non-null observable.
    /// </summary>
    [Test]
    public void PoppedObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PoppedObservable();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PushingObservable returns a non-null observable.
    /// </summary>
    [Test]
    public void PushingObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PushingObservable();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that PushedObservable returns a non-null observable.
    /// </summary>
    [Test]
    public void PushedObservable_ReturnsNonNullObservable()
    {
        // Act
        var result = _navigation.PushedObservable();

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    /// <summary>
    /// Tests that multiple subscriptions to PoppingObservable are independent.
    /// </summary>
    [Test]
    public void PoppingObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _navigation.PoppingObservable();
        var observable2 = _navigation.PoppingObservable();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(observable1, Is.Not.Null);
            Assert.That(observable2, Is.Not.Null);
            Assert.That(observable1, Is.Not.SameAs(observable2));
        }
    }

    /// <summary>
    /// Tests that multiple subscriptions to PoppedObservable are independent.
    /// </summary>
    [Test]
    public void PoppedObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _navigation.PoppedObservable();
        var observable2 = _navigation.PoppedObservable();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(observable1, Is.Not.Null);
            Assert.That(observable2, Is.Not.Null);
            Assert.That(observable1, Is.Not.SameAs(observable2));
        }
    }

    /// <summary>
    /// Tests that multiple subscriptions to PushingObservable are independent.
    /// </summary>
    [Test]
    public void PushingObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _navigation.PushingObservable();
        var observable2 = _navigation.PushingObservable();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(observable1, Is.Not.Null);
            Assert.That(observable2, Is.Not.Null);
            Assert.That(observable1, Is.Not.SameAs(observable2));
        }
    }

    /// <summary>
    /// Tests that multiple subscriptions to PushedObservable are independent.
    /// </summary>
    [Test]
    public void PushedObservable_MultipleSubscriptions_AreIndependent()
    {
        // Act
        var observable1 = _navigation.PushedObservable();
        var observable2 = _navigation.PushedObservable();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(observable1, Is.Not.Null);
            Assert.That(observable2, Is.Not.Null);
            Assert.That(observable1, Is.Not.SameAs(observable2));
        }
    }
}
