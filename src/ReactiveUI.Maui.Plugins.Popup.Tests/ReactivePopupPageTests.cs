// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>Tests for <see cref="ReactivePopupPage"/>.</summary>
public class ReactivePopupPageTests
{
    /// <summary>Tests that a new instance has null ViewModel.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task Constructor_WhenCalled_ViewModelIsNull()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        await Assert.That(page.ViewModel).IsNull();
    }

    /// <summary>Tests that BackgroundClick observable is initialized.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task Constructor_WhenCalled_BackgroundClickIsInitialized()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        await Assert.That(page.BackgroundClick).IsNotNull();
    }

    /// <summary>Tests that ControlBindings is initialized as empty composite disposable.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task Constructor_WhenCalled_ControlBindingsIsInitialized()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        await Assert.That(page.TestControlBindings).IsNotNull();
    }

    /// <summary>Tests that setting ViewModel updates the property.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WhenSet_UpdatesValue()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.ViewModel = viewModel;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel);
    }

    /// <summary>Tests that setting ViewModel also sets BindingContext.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WhenSet_UpdatesBindingContext()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.ViewModel = viewModel;

        // Assert
        await Assert.That(page.BindingContext).IsEqualTo(viewModel);
    }

    /// <summary>Tests that setting BindingContext updates ViewModel.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task BindingContext_WhenSet_UpdatesViewModel()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.BindingContext = viewModel;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel);
    }

    /// <summary>Tests that setting ViewModel to null clears the value.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WhenSetToNull_ClearsValue()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.ViewModel = null;

        // Assert
        await Assert.That(page.ViewModel).IsNull();
    }

    /// <summary>Tests that OnViewModelChanged throws when bindableObject is null.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task OnViewModelChanged_WhenBindableObjectIsNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.That(static () => TestReactivePopupPage.InvokeOnViewModelChanged(null!, new(), new())).ThrowsExactly<ArgumentNullException>();
    }

    /// <summary>Tests that OnViewModelChanged sets BindingContext to new value.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task OnViewModelChanged_WhenCalled_SetsBindingContextToNewValue()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var newViewModel = new TestViewModel();

        // Act
        TestReactivePopupPage.InvokeOnViewModelChanged(page, null!, newViewModel);

        // Assert
        await Assert.That(page.BindingContext).IsEqualTo(newViewModel);
    }

    /// <summary>Tests that changing ViewModel from one value to another works correctly.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WhenChangedFromOneToAnother_UpdatesCorrectly()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var viewModel1 = new TestViewModel();
        var viewModel2 = new TestViewModel();
        page.ViewModel = viewModel1;

        // Act
        page.ViewModel = viewModel2;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel2);
        await Assert.That(page.BindingContext).IsEqualTo(viewModel2);
    }

    /// <summary>Tests that BindingContext change to non-object type sets ViewModel correctly.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task BindingContext_WhenSetToString_UpdatesViewModel()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        const string? viewModel = "test string";

        // Act
        page.BindingContext = viewModel;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel);
    }

    /// <summary>Tests that ViewModelProperty is defined with correct metadata.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModelProperty_IsDefined()
    {
        // Assert
        await Assert.That(ReactivePopupPage.ViewModelProperty).IsNotNull();
        await Assert.That(ReactivePopupPage.ViewModelProperty.PropertyName).IsEqualTo("ViewModel");
        await Assert.That(ReactivePopupPage.ViewModelProperty.ReturnType).IsSameReferenceAs(typeof(object));
    }

    /// <summary>Tests that ControlBindings is initially empty.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ControlBindings_WhenConstructed_IsEmpty()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        await Assert.That(page.TestControlBindings.Count).IsEqualTo(0);
    }

    /// <summary>Tests that setting ViewModel to null also clears BindingContext.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WhenSetToNull_ClearsBindingContext()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.ViewModel = null;

        // Assert
        await Assert.That(page.BindingContext).IsNull();
    }

    /// <summary>Concrete implementation for testing the abstract ReactivePopupPage.</summary>
    private sealed class TestReactivePopupPage : ReactivePopupPage
    {
        /// <summary>Gets the control bindings for testing.</summary>
        public CompositeDisposable TestControlBindings => ControlBindings;

        /// <summary>Invokes the OnViewModelChanged method for testing.</summary>
        /// <param name="bindableObject">The bindable object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeOnViewModelChanged(BindableObject bindableObject, object oldValue, object newValue) =>
            OnViewModelChanged(bindableObject, oldValue, newValue);
    }

    /// <summary>Simple test view model.</summary>
    private sealed class TestViewModel
    {
        /// <summary>Gets or sets the popup title.</summary>
        public string Title { get; set; } = string.Empty;
    }
}
