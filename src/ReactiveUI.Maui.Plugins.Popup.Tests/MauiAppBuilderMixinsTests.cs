// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using ReactiveUI.Builder;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="MauiAppBuilderMixins"/>.
/// </summary>
/// <remarks>
/// These tests verify the extension methods exist and are properly defined.
/// Full integration testing would require a MAUI host environment.
/// </remarks>
public class MauiAppBuilderMixinsTests
{
    /// <summary>
    /// Tests that ConfigureReactiveUIPopup extension method exists.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_ExtensionMethodExists()
    {
        // Assert - The test passes if the method exists (compilation succeeds)
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder) });

        await Assert.That(methodInfo).IsNotNull();
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler exists.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandler_ExtensionMethodExists()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action) });

        await Assert.That(methodInfo).IsNotNull();
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with configureReactiveUI exists.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithConfigureReactiveUI_ExtensionMethodExists()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action<IReactiveUIBuilder>) });

        await Assert.That(methodInfo).IsNotNull();
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler and configureReactiveUI exists.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandlerAndConfigureReactiveUI_ExtensionMethodExists()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action), typeof(Action<IReactiveUIBuilder>) });

        await Assert.That(methodInfo).IsNotNull();
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup returns MauiAppBuilder for chaining.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_ReturnsCorrectType()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder) });

        await Assert.That(methodInfo).IsNotNull();
        await Assert.That(methodInfo!.ReturnType).IsSameReferenceAs(typeof(MauiAppBuilder));
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler returns MauiAppBuilder.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandler_ReturnsCorrectType()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action) });

        await Assert.That(methodInfo).IsNotNull();
        await Assert.That(methodInfo!.ReturnType).IsSameReferenceAs(typeof(MauiAppBuilder));
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with configureReactiveUI returns MauiAppBuilder.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithConfigureReactiveUI_ReturnsCorrectType()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action<IReactiveUIBuilder>) });

        await Assert.That(methodInfo).IsNotNull();
        await Assert.That(methodInfo!.ReturnType).IsSameReferenceAs(typeof(MauiAppBuilder));
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler and configureReactiveUI returns MauiAppBuilder.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandlerAndConfigureReactiveUI_ReturnsCorrectType()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action), typeof(Action<IReactiveUIBuilder>) });

        await Assert.That(methodInfo).IsNotNull();
        await Assert.That(methodInfo!.ReturnType).IsSameReferenceAs(typeof(MauiAppBuilder));
    }

    /// <summary>
    /// Tests that MauiAppBuilderMixins is a static class.
    /// </summary>
    [Test]
    public async Task MauiAppBuilderMixins_IsStaticClass()
    {
        // Assert
        await Assert.That(typeof(MauiAppBuilderMixins).IsAbstract).IsTrue();
        await Assert.That(typeof(MauiAppBuilderMixins).IsSealed).IsTrue();
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup first parameter is MauiAppBuilder.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_FirstParameterIsMauiAppBuilder()
    {
        // Arrange
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder) });

        // Assert
        await Assert.That(methodInfo).IsNotNull();
        var parameters = methodInfo!.GetParameters();
        await Assert.That(parameters.Length).IsEqualTo(1);
        await Assert.That(parameters[0].ParameterType).IsSameReferenceAs(typeof(MauiAppBuilder));
        await Assert.That(parameters[0].Name).IsEqualTo("builder");
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler has correct parameters.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandler_HasCorrectParameters()
    {
        // Arrange
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action) });

        // Assert
        await Assert.That(methodInfo).IsNotNull();
        var parameters = methodInfo!.GetParameters();
        await Assert.That(parameters.Length).IsEqualTo(2);
        await Assert.That(parameters[0].ParameterType).IsSameReferenceAs(typeof(MauiAppBuilder));
        await Assert.That(parameters[0].Name).IsEqualTo("builder");
        await Assert.That(parameters[1].ParameterType).IsSameReferenceAs(typeof(Action));
        await Assert.That(parameters[1].Name).IsEqualTo("backPressHandler");
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with configureReactiveUI has correct parameters.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithConfigureReactiveUI_HasCorrectParameters()
    {
        // Arrange
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action<IReactiveUIBuilder>) });

        // Assert
        await Assert.That(methodInfo).IsNotNull();
        var parameters = methodInfo!.GetParameters();
        await Assert.That(parameters.Length).IsEqualTo(2);
        await Assert.That(parameters[0].ParameterType).IsSameReferenceAs(typeof(MauiAppBuilder));
        await Assert.That(parameters[0].Name).IsEqualTo("builder");
        await Assert.That(parameters[1].ParameterType).IsSameReferenceAs(typeof(Action<IReactiveUIBuilder>));
        await Assert.That(parameters[1].Name).IsEqualTo("configureReactiveUI");
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler and configureReactiveUI has correct parameters.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandlerAndConfigureReactiveUI_HasCorrectParameters()
    {
        // Arrange
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action), typeof(Action<IReactiveUIBuilder>) });

        // Assert
        await Assert.That(methodInfo).IsNotNull();
        var parameters = methodInfo!.GetParameters();
        await Assert.That(parameters.Length).IsEqualTo(3);
        await Assert.That(parameters[0].ParameterType).IsSameReferenceAs(typeof(MauiAppBuilder));
        await Assert.That(parameters[0].Name).IsEqualTo("builder");
        await Assert.That(parameters[1].ParameterType).IsSameReferenceAs(typeof(Action));
        await Assert.That(parameters[1].Name).IsEqualTo("backPressHandler");
        await Assert.That(parameters[2].ParameterType).IsSameReferenceAs(typeof(Action<IReactiveUIBuilder>));
        await Assert.That(parameters[2].Name).IsEqualTo("configureReactiveUI");
    }

    /// <summary>
    /// Tests that MauiAppBuilderMixins is in correct namespace.
    /// </summary>
    [Test]
    public async Task MauiAppBuilderMixins_IsInCorrectNamespace() =>
        await Assert.That(typeof(MauiAppBuilderMixins).Namespace).IsEqualTo("Microsoft.Maui.Hosting");

    /// <summary>
    /// Tests that there are exactly four ConfigureReactiveUIPopup overloads.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_HasFourOverloads()
    {
        // Arrange
        var methods = typeof(MauiAppBuilderMixins)
            .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(m => m.Name == "ConfigureReactiveUIPopup")
            .ToArray();

        // Assert
        await Assert.That(methods.Length).IsEqualTo(4);
    }

    /// <summary>
    /// Tests that all ConfigureReactiveUIPopup overloads are static methods.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_AllOverloads_AreStaticMethods()
    {
        // Arrange
        var methods = typeof(MauiAppBuilderMixins)
            .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(m => m.Name == "ConfigureReactiveUIPopup")
            .ToArray();

        // Assert
        foreach (var method in methods)
        {
            await Assert.That(method.IsStatic).IsTrue();
        }
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup without backPressHandler configures services and returns builder.
    /// </summary>
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

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with configureReactiveUI invokes the callback and returns builder.
    /// </summary>
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

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with null configureReactiveUI does not throw.
    /// </summary>
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

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler returns same builder.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandler_ReturnsSameBuilder()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act
        var result = builder.ConfigureReactiveUIPopup(backPressHandler: () => { });

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with null backPressHandler does not throw.
    /// </summary>
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

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with both backPressHandler and configureReactiveUI returns same builder.
    /// </summary>
    [Test]
    public async Task ConfigureReactiveUIPopup_WithBackPressHandlerAndConfigureReactiveUI_ReturnsSameBuilder()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();
        var callbackInvoked = false;

        // Act
        var result = builder.ConfigureReactiveUIPopup(
            backPressHandler: () => { },
            configureReactiveUI: rxBuilder => callbackInvoked = true);

        // Assert
        await Assert.That(result).IsSameReferenceAs(builder);
        await Assert.That(callbackInvoked).IsTrue();
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with null backPressHandler and null configureReactiveUI does not throw.
    /// </summary>
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
