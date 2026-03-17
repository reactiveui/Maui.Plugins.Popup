// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="ReactivePopupPage"/>.
/// </summary>
public class ReactivePopupPageTests
{
    /// <summary>
    /// Tests that a new instance has null ViewModel.
    /// </summary>
    [Test]
    public async Task Constructor_WhenCalled_ViewModelIsNull()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        await Assert.That(page.ViewModel).IsNull();
    }

    /// <summary>
    /// Tests that BackgroundClick observable is initialized.
    /// </summary>
    [Test]
    public async Task Constructor_WhenCalled_BackgroundClickIsInitialized()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        await Assert.That(page.BackgroundClick).IsNotNull();
    }

    /// <summary>
    /// Tests that ControlBindings is initialized as empty composite disposable.
    /// </summary>
    [Test]
    public async Task Constructor_WhenCalled_ControlBindingsIsInitialized()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        await Assert.That(page.TestControlBindings).IsNotNull();
    }

    /// <summary>
    /// Tests that setting ViewModel updates the property.
    /// </summary>
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

    /// <summary>
    /// Tests that setting ViewModel also sets BindingContext.
    /// </summary>
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

    /// <summary>
    /// Tests that setting BindingContext updates ViewModel.
    /// </summary>
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

    /// <summary>
    /// Tests that setting ViewModel to null clears the value.
    /// </summary>
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

    /// <summary>
    /// Tests that OnViewModelChanged throws when bindableObject is null.
    /// </summary>
    [Test]
    public async Task OnViewModelChanged_WhenBindableObjectIsNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.That(() => TestReactivePopupPage.InvokeOnViewModelChanged(null!, new object(), new object())).ThrowsExactly<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that OnViewModelChanged sets BindingContext to new value.
    /// </summary>
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

    /// <summary>
    /// Tests that changing ViewModel from one value to another works correctly.
    /// </summary>
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

    /// <summary>
    /// Tests that BindingContext change to non-object type sets ViewModel correctly.
    /// </summary>
    [Test]
    public async Task BindingContext_WhenSetToString_UpdatesViewModel()
    {
        // Arrange
        var page = new TestReactivePopupPage();
#pragma warning disable RCS1118 // Mark local variable as const
        var viewModel = "test string";
#pragma warning restore RCS1118 // Mark local variable as const

        // Act
        page.BindingContext = viewModel;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel);
    }

    /// <summary>
    /// Tests that subscribing to BackgroundClick observable succeeds.
    /// </summary>
    [Test]
    public async Task BackgroundClick_WhenSubscribed_ReceivesBackgroundClickEvent()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        Unit? receivedValue = null;

        using var subscription = page.BackgroundClick.Subscribe(unit => receivedValue = unit);

        // Act
        page.SimulateBackgroundClick();

        // Assert
        await Assert.That(receivedValue).IsNotNull();
        await Assert.That(receivedValue!.Value).IsEqualTo(Unit.Default);
    }

    /// <summary>
    /// Tests that BackgroundClick observable emits Unit.Default on each click.
    /// </summary>
    [Test]
    public async Task BackgroundClick_WhenClickedMultipleTimes_EmitsForEachClick()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var receivedCount = 0;

        using var subscription = page.BackgroundClick.Subscribe(_ => receivedCount++);

        // Act
        page.SimulateBackgroundClick();
        page.SimulateBackgroundClick();
        page.SimulateBackgroundClick();

        // Assert
        await Assert.That(receivedCount).IsEqualTo(3);
    }

    /// <summary>
    /// Tests that disposing BackgroundClick subscription stops receiving events.
    /// </summary>
    [Test]
    public async Task BackgroundClick_WhenDisposed_DoesNotReceiveEvents()
    {
        // Arrange
        var page = new TestReactivePopupPage();
        var receivedCount = 0;

        var subscription = page.BackgroundClick.Subscribe(_ => receivedCount++);

        // Act
        page.SimulateBackgroundClick();
        subscription.Dispose();
        page.SimulateBackgroundClick();

        // Assert
        await Assert.That(receivedCount).IsEqualTo(1);
    }

    /// <summary>
    /// Tests that ViewModelProperty is defined with correct metadata.
    /// </summary>
    [Test]
    public async Task ViewModelProperty_IsDefined()
    {
        // Assert
        await Assert.That(ReactivePopupPage.ViewModelProperty).IsNotNull();
        await Assert.That(ReactivePopupPage.ViewModelProperty.PropertyName).IsEqualTo("ViewModel");
        await Assert.That(ReactivePopupPage.ViewModelProperty.ReturnType).IsSameReferenceAs(typeof(object));
    }

    /// <summary>
    /// Tests that ControlBindings is initially empty.
    /// </summary>
    [Test]
    public async Task ControlBindings_WhenConstructed_IsEmpty()
    {
        // Arrange & Act
        var page = new TestReactivePopupPage();

        // Assert
        await Assert.That(page.TestControlBindings.Count).IsEqualTo(0);
    }

    /// <summary>
    /// Tests that setting ViewModel to null also clears BindingContext.
    /// </summary>
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

        /// <summary>
        /// Simulates a background click by raising the BackgroundClicked event via reflection.
        /// </summary>
        public void SimulateBackgroundClick()
        {
            var field = typeof(PopupPage).GetField(
                "BackgroundClicked",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var handler = (EventHandler?)field?.GetValue(this);
            handler?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Simple test view model.
    /// </summary>
    private sealed class TestViewModel;
}
