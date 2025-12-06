// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="ReactivePopupPage{TViewModel}"/>.
/// </summary>
[TestFixture]
public class ReactivePopupPageGenericTests
{
    /// <summary>
    /// Tests that a new instance has null ViewModel.
    /// </summary>
    [Test]
    public void Constructor_WhenCalled_ViewModelIsNull()
    {
        // Arrange & Act
        var page = new TestGenericPopupPage();

        // Assert
        Assert.That(page.ViewModel, Is.Null);
    }

    /// <summary>
    /// Tests that setting ViewModel updates the property.
    /// </summary>
    [Test]
    public void ViewModel_WhenSet_UpdatesValue()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.ViewModel = viewModel;

        // Assert
        Assert.That(page.ViewModel, Is.EqualTo(viewModel));
    }

    /// <summary>
    /// Tests that setting ViewModel also sets BindingContext.
    /// </summary>
    [Test]
    public void ViewModel_WhenSet_UpdatesBindingContext()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.ViewModel = viewModel;

        // Assert
        Assert.That(page.BindingContext, Is.EqualTo(viewModel));
    }

    /// <summary>
    /// Tests that setting BindingContext with correct type updates ViewModel.
    /// </summary>
    [Test]
    public void BindingContext_WhenSetWithCorrectType_UpdatesViewModel()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.BindingContext = viewModel;

        // Assert
        Assert.That(page.ViewModel, Is.EqualTo(viewModel));
    }

    /// <summary>
    /// Tests that setting BindingContext with wrong type sets ViewModel to null.
    /// </summary>
    [Test]
    public void BindingContext_WhenSetWithWrongType_SetsViewModelToNull()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.BindingContext = "wrong type";

        // Assert
        Assert.That(page.ViewModel, Is.Null);
    }

    /// <summary>
    /// Tests that setting ViewModel to null clears the value.
    /// </summary>
    [Test]
    public void ViewModel_WhenSetToNull_ClearsValue()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.ViewModel = null;

        // Assert
        Assert.That(page.ViewModel, Is.Null);
    }

    /// <summary>
    /// Tests that changing ViewModel from one value to another works correctly.
    /// </summary>
    [Test]
    public void ViewModel_WhenChangedFromOneToAnother_UpdatesCorrectly()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel1 = new TestViewModel();
        var viewModel2 = new TestViewModel();
        page.ViewModel = viewModel1;

        // Act
        page.ViewModel = viewModel2;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(page.ViewModel, Is.EqualTo(viewModel2));
            Assert.That(page.BindingContext, Is.EqualTo(viewModel2));
        }
    }

    /// <summary>
    /// Tests that page implements IViewFor with correct TViewModel.
    /// </summary>
    [Test]
    public void Page_ImplementsIViewFor()
    {
        // Arrange & Act
        var page = new TestGenericPopupPage();

        // Assert
        Assert.That(page, Is.InstanceOf<IViewFor<TestViewModel>>());
    }

    /// <summary>
    /// Tests that setting BindingContext to null sets ViewModel to null.
    /// </summary>
    [Test]
    public void BindingContext_WhenSetToNull_SetsViewModelToNull()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.BindingContext = null;

        // Assert
        Assert.That(page.ViewModel, Is.Null);
    }

    /// <summary>
    /// Tests that BackgroundClick observable is available on generic popup page.
    /// </summary>
    [Test]
    public void BackgroundClick_IsAvailable()
    {
        // Arrange & Act
        var page = new TestGenericPopupPage();

        // Assert
        Assert.That(page.BackgroundClick, Is.Not.Null);
    }

    /// <summary>
    /// Tests ViewModelProperty is defined correctly.
    /// </summary>
    [Test]
    public void ViewModelProperty_IsDefined()
    {
        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(TestGenericPopupPage.ViewModelProperty, Is.Not.Null);
            Assert.That(TestGenericPopupPage.ViewModelProperty.PropertyName, Is.EqualTo("ViewModel"));
            Assert.That(TestGenericPopupPage.ViewModelProperty.ReturnType, Is.EqualTo(typeof(TestViewModel)));
        }
    }

    /// <summary>
    /// Tests that derived view model type works correctly.
    /// </summary>
    [Test]
    public void ViewModel_WithDerivedType_WorksCorrectly()
    {
        // Arrange
        var page = new TestPopupPageWithDerivedViewModel();
        var viewModel = new DerivedTestViewModel();

        // Act
        page.ViewModel = viewModel;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(page.ViewModel, Is.EqualTo(viewModel));
            Assert.That(page.BindingContext, Is.EqualTo(viewModel));
        }
    }

    /// <summary>
    /// Tests that setting BindingContext to derived type works.
    /// </summary>
    [Test]
    public void BindingContext_WithDerivedType_SetsViewModel()
    {
        // Arrange
        var page = new TestPopupPageWithDerivedViewModel();
        var viewModel = new DerivedTestViewModel();

        // Act
        page.BindingContext = viewModel;

        // Assert
        Assert.That(page.ViewModel, Is.EqualTo(viewModel));
    }

    /// <summary>
    /// Simple test view model.
    /// </summary>
    public sealed class TestViewModel;

    /// <summary>
    /// Base test view model for inheritance testing.
    /// </summary>
    public class TestViewModelBase;

    /// <summary>
    /// Derived test view model for inheritance testing.
    /// </summary>
    public sealed class DerivedTestViewModel : TestViewModelBase;

    /// <summary>
    /// Concrete implementation for testing the generic ReactivePopupPage.
    /// </summary>
    private sealed class TestGenericPopupPage : ReactivePopupPage<TestViewModel>;

    /// <summary>
    /// Popup page with base test view model for testing inheritance.
    /// </summary>
    private sealed class TestPopupPageWithDerivedViewModel : ReactivePopupPage<TestViewModelBase>;
}
