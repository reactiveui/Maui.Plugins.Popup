// Copyright (c) 2019-2026 ReactiveUI and Contributors. All rights reserved.
// ReactiveUI and Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>Tests for <see cref="MauiAppBuilderMixins"/>.</summary>
public class MauiAppBuilderMixinsTests
{
    /// <summary>Tests that ConfigureReactiveUIPopup without backPressHandler configures services and returns builder.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ConfigureReactiveUIPopup_WhenCalled_ReturnsSameBuilder()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act
        var result = builder.ConfigureReactiveUIPopup();

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
    }

    /// <summary>Tests that ConfigureReactiveUIPopup with configureReactiveUI invokes the callback and returns builder.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithConfigureReactiveUI_InvokesCallbackAndReturnsSameBuilder()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();
        var callbackInvoked = false;

        // Act
        var result = builder.ConfigureReactiveUIPopup(rxBuilder => callbackInvoked = true);

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
        await Assert.That(callbackInvoked).IsTrue();
    }

    /// <summary>Tests that ConfigureReactiveUIPopup with null configureReactiveUI does not throw.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithNullConfigureReactiveUI_DoesNotThrow()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act
        var result = builder.ConfigureReactiveUIPopup(configureReactiveUI: null);

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
    }

    /// <summary>Tests that ConfigureReactiveUIPopup with backPressHandler returns same builder.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandler_ReturnsSameBuilder()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act
        var result = builder.ConfigureReactiveUIPopup(backPressHandler: static () => { });

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
    }

    /// <summary>Tests that ConfigureReactiveUIPopup with null backPressHandler does not throw.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithNullBackPressHandler_DoesNotThrow()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act
        var result = builder.ConfigureReactiveUIPopup(backPressHandler: null);

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
    }

    /// <summary>Tests that ConfigureReactiveUIPopup with both backPressHandler and configureReactiveUI returns same builder.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandlerAndConfigureReactiveUI_ReturnsSameBuilder()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();
        var callbackInvoked = false;

        // Act
        var result = builder.ConfigureReactiveUIPopup(
            backPressHandler: static () => { },
            configureReactiveUI: rxBuilder => callbackInvoked = true);

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
        await Assert.That(callbackInvoked).IsTrue();
    }

    /// <summary>Tests that ConfigureReactiveUIPopup with null backPressHandler and null configureReactiveUI does not throw.</summary>
    /// <returns>A task representing the asynchronous unit test.</returns>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithNullBackPressHandlerAndNullConfigureReactiveUI_DoesNotThrow()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act
        var result = builder.ConfigureReactiveUIPopup(backPressHandler: null, configureReactiveUI: null);

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
    }
}
