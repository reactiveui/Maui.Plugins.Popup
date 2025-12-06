// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using FluentAssertions;
using NUnit.Framework;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="ReactivePopupPage"/>.
/// </summary>
[TestFixture]
public class ReactivePopupPageTests
{
    /// <summary>
    /// Tests that a new instance has null ViewModel.
    /// </summary>
    [Test]
    public void Constructor_WhenCalled_ViewModelIsNull()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        page.ViewModel.Should().BeNull();
    }

    /// <summary>
    /// Tests that BackgroundClick observable is initialized.
    /// </summary>
    [Test]
    public void Constructor_WhenCalled_BackgroundClickIsInitialized()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        page.BackgroundClick.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that ControlBindings is initialized as empty composite disposable.
    /// </summary>
    [Test]
    public void Constructor_WhenCalled_ControlBindingsIsInitialized()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        page.TestControlBindings.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that setting ViewModel updates the property.
    /// </summary>
    [Test]
    public void ViewModel_WhenSet_UpdatesValue()
    {
        // Arrange
        var page = new TestReactivePopupPage();
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
        var page = new TestReactivePopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.ViewModel = viewModel;

        // Assert
        page.BindingContext.Should().Be(viewModel);
    }

    /// <summary>
    /// Tests that setting BindingContext updates ViewModel.
    /// </summary>
    [Test]
    public void BindingContext_WhenSet_UpdatesViewModel()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.BindingContext = viewModel;

        // Assert
        page.ViewModel.Should().Be(viewModel);
    }

    /// <summary>
    /// Tests that setting ViewModel to null clears the value.
    /// </summary>
    [Test]
    public void ViewModel_WhenSetToNull_ClearsValue()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.ViewModel = null;

        // Assert
        page.ViewModel.Should().BeNull();
    }

    /// <summary>
    /// Tests that OnViewModelChanged throws when bindableObject is null.
    /// </summary>
    [Test]
    public void OnViewModelChanged_WhenBindableObjectIsNull_ThrowsArgumentNullException()
    {
        // Arrange & Act
        var act = () => TestReactivePopupPage.InvokeOnViewModelChanged(null!, new object(), new object());

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that OnViewModelChanged sets BindingContext to new value.
    /// </summary>
    [Test]
    public void OnViewModelChanged_WhenCalled_SetsBindingContextToNewValue()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var newViewModel = new TestViewModel();

        // Act
        TestReactivePopupPage.InvokeOnViewModelChanged(page, null!, newViewModel);

        // Assert
        page.BindingContext.Should().Be(newViewModel);
    }

    /// <summary>
    /// Tests that changing ViewModel from one value to another works correctly.
    /// </summary>
    [Test]
    public void ViewModel_WhenChangedFromOneToAnother_UpdatesCorrectly()
    {
        // Arrange
        var page = new TestReactivePopupPage();
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
    /// Tests that BindingContext change to non-object type sets ViewModel correctly.
    /// </summary>
    [Test]
    public void BindingContext_WhenSetToString_UpdatesViewModel()
    {
        // Arrange
        var page = new TestReactivePopupPage();
#pragma warning disable RCS1118 // Mark local variable as const
        var viewModel = "test string";
#pragma warning restore RCS1118 // Mark local variable as const

        // Act
        page.BindingContext = viewModel;

        // Assert
        page.ViewModel.Should().Be(viewModel);
    }

    /// <summary>
    /// Concrete implementation for testing the abstract ReactivePopupPage.
    /// </summary>
    private sealed class TestReactivePopupPage : ReactivePopupPage
    {
        /// <summary>
        /// Gets the control bindings for testing.
        /// </summary>
        public System.Reactive.Disposables.CompositeDisposable TestControlBindings => ControlBindings;

        /// <summary>
        /// Invokes the OnViewModelChanged method for testing.
        /// </summary>
        /// <param name="bindableObject">The bindable object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public static void InvokeOnViewModelChanged(BindableObject bindableObject, object oldValue, object newValue) =>
            OnViewModelChanged(bindableObject, oldValue, newValue);
    }

    /// <summary>
    /// Simple test view model.
    /// </summary>
    private sealed class TestViewModel;
}
