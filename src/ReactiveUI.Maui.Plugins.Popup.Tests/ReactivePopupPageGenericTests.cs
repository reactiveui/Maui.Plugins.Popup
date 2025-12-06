// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using FluentAssertions;
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
        page.ViewModel.Should().BeNull();
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
        page.ViewModel.Should().Be(viewModel);
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
        page.BindingContext.Should().Be(viewModel);
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
        page.ViewModel.Should().Be(viewModel);
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
        page.ViewModel.Should().BeNull();
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
        page.ViewModel.Should().BeNull();
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
        page.ViewModel.Should().Be(viewModel2);
        page.BindingContext.Should().Be(viewModel2);
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
        page.Should().BeAssignableTo<IViewFor<TestViewModel>>();
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
        page.ViewModel.Should().BeNull();
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
        page.BackgroundClick.Should().NotBeNull();
    }

    /// <summary>
    /// Tests ViewModelProperty is defined correctly.
    /// </summary>
    [Test]
    public void ViewModelProperty_IsDefined()
    {
        // Assert
        TestGenericPopupPage.ViewModelProperty.Should().NotBeNull();
        TestGenericPopupPage.ViewModelProperty.PropertyName.Should().Be("ViewModel");
        TestGenericPopupPage.ViewModelProperty.ReturnType.Should().Be<TestViewModel>();
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
        page.ViewModel.Should().Be(viewModel);
        page.BindingContext.Should().Be(viewModel);
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
        page.ViewModel.Should().Be(viewModel);
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
