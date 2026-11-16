// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>Tests for <see cref="ReactivePopupPage{TViewModel}"/>.</summary>
public class ReactivePopupPageGenericTests
{
    /// <summary>Tests that a new instance has null ViewModel.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task Constructor_WhenCalled_ViewModelIsNull()
    {
        // Arrange & Act
        var page = new TestGenericPopupPage();

        // Assert
        await Assert.That(page.ViewModel).IsNull();
    }

    /// <summary>Tests that setting ViewModel updates the property.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WhenSet_UpdatesValue()
    {
        // Arrange
        var page = new TestGenericPopupPage();
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
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.ViewModel = viewModel;

        // Assert
        await Assert.That(page.BindingContext).IsEqualTo(viewModel);
    }

    /// <summary>Tests that setting BindingContext with correct type updates ViewModel.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task BindingContext_WhenSetWithCorrectType_UpdatesViewModel()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();

        // Act
        page.BindingContext = viewModel;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel);
    }

    /// <summary>Tests that setting BindingContext with wrong type sets ViewModel to null.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task BindingContext_WhenSetWithWrongType_SetsViewModelToNull()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.BindingContext = "wrong type";

        // Assert
        await Assert.That(page.ViewModel).IsNull();
    }

    /// <summary>Tests that setting ViewModel to null clears the value.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WhenSetToNull_ClearsValue()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.ViewModel = null;

        // Assert
        await Assert.That(page.ViewModel).IsNull();
    }

    /// <summary>Tests that changing ViewModel from one value to another works correctly.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WhenChangedFromOneToAnother_UpdatesCorrectly()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel1 = new TestViewModel();
        var viewModel2 = new TestViewModel();
        page.ViewModel = viewModel1;

        // Act
        page.ViewModel = viewModel2;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel2);
        await Assert.That(page.BindingContext).IsEqualTo(viewModel2);
    }

    /// <summary>Tests that page implements IViewFor with correct TViewModel.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task Page_ImplementsIViewFor()
    {
        // Arrange & Act
        var page = new TestGenericPopupPage();

        // Assert
        await Assert.That(page is IViewFor<TestViewModel>).IsTrue();
    }

    /// <summary>Tests that setting BindingContext to null sets ViewModel to null.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task BindingContext_WhenSetToNull_SetsViewModelToNull()
    {
        // Arrange
        var page = new TestGenericPopupPage();
        var viewModel = new TestViewModel();
        page.ViewModel = viewModel;

        // Act
        page.BindingContext = null;

        // Assert
        await Assert.That(page.ViewModel).IsNull();
    }

    /// <summary>Tests that BackgroundClick observable is available on generic popup page.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task BackgroundClick_IsAvailable()
    {
        // Arrange & Act
        var page = new TestGenericPopupPage();

        // Assert
        await Assert.That(page.BackgroundClick).IsNotNull();
    }

    /// <summary>Tests ViewModelProperty is defined correctly.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModelProperty_IsDefined()
    {
        // Assert
        await Assert.That(TestGenericPopupPage.ViewModelProperty).IsNotNull();
        await Assert.That(TestGenericPopupPage.ViewModelProperty.PropertyName).IsEqualTo("ViewModel");
        await Assert.That(TestGenericPopupPage.ViewModelProperty.ReturnType).IsSameReferenceAs(typeof(TestViewModel));
    }

    /// <summary>Tests that derived view model type works correctly.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ViewModel_WithDerivedType_WorksCorrectly()
    {
        // Arrange
        var page = new TestPopupPageWithDerivedViewModel();
        var viewModel = new DerivedTestViewModel();

        // Act
        page.ViewModel = viewModel;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel);
        await Assert.That(page.BindingContext).IsEqualTo(viewModel);
    }

    /// <summary>Tests that setting BindingContext to derived type works.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task BindingContext_WithDerivedType_SetsViewModel()
    {
        // Arrange
        var page = new TestPopupPageWithDerivedViewModel();
        var viewModel = new DerivedTestViewModel();

        // Act
        page.BindingContext = viewModel;

        // Assert
        await Assert.That(page.ViewModel).IsEqualTo(viewModel);
    }

    /// <summary>Simple test view model.</summary>
    [System.Diagnostics.DebuggerDisplay("{Title}")]
    public sealed class TestViewModel
    {
        /// <summary>Gets or sets the popup title.</summary>
        public string Title { get; set; } = string.Empty;
    }

    /// <summary>Base test view model for inheritance testing.</summary>
    [System.Diagnostics.DebuggerDisplay("{Title}")]
    public class TestViewModelBase
    {
        /// <summary>Gets or sets the popup title.</summary>
        public string Title { get; set; } = string.Empty;
    }

    /// <summary>Derived test view model for inheritance testing.</summary>
    public sealed class DerivedTestViewModel : TestViewModelBase;

    /// <summary>Concrete implementation for testing the generic ReactivePopupPage.</summary>
    private sealed class TestGenericPopupPage : ReactivePopupPage<TestViewModel>
    {
    }

    /// <summary>Popup page with base test view model for testing inheritance.</summary>
    private sealed class TestPopupPageWithDerivedViewModel : ReactivePopupPage<TestViewModelBase>;
}
