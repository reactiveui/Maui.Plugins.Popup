// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace ReactiveUI.Maui.Plugins.Popup.Tests;

/// <summary>
/// Tests for <see cref="MauiAppBuilderMixins"/>.
/// </summary>
/// <remarks>
/// These tests verify the extension methods exist and are properly defined.
/// Full integration testing would require a MAUI host environment.
/// </remarks>
[TestFixture]
public class MauiAppBuilderMixinsTests
{
    /// <summary>
    /// Tests that ConfigureReactiveUIPopup extension method exists.
    /// </summary>
    [Test]
    public void ConfigureReactiveUIPopup_ExtensionMethodExists()
    {
        // Assert - The test passes if the method exists (compilation succeeds)
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder) });

        Assert.That(methodInfo, Is.Not.Null);
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler exists.
    /// </summary>
    [Test]
    public void ConfigureReactiveUIPopup_WithBackPressHandler_ExtensionMethodExists()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action) });

        Assert.That(methodInfo, Is.Not.Null);
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup returns MauiAppBuilder for chaining.
    /// </summary>
    [Test]
    public void ConfigureReactiveUIPopup_ReturnsCorrectType()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder) });

        using (Assert.EnterMultipleScope())
        {
            Assert.That(methodInfo, Is.Not.Null);
            Assert.That(methodInfo!.ReturnType, Is.SameAs(typeof(MauiAppBuilder)));
        }
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler returns MauiAppBuilder.
    /// </summary>
    [Test]
    public void ConfigureReactiveUIPopup_WithBackPressHandler_ReturnsCorrectType()
    {
        // Assert
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action) });

        using (Assert.EnterMultipleScope())
        {
            Assert.That(methodInfo, Is.Not.Null);
            Assert.That(methodInfo!.ReturnType, Is.SameAs(typeof(MauiAppBuilder)));
        }
    }

    /// <summary>
    /// Tests that MauiAppBuilderMixins is a static class.
    /// </summary>
    [Test]
    public void MauiAppBuilderMixins_IsStaticClass()
    {
        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(typeof(MauiAppBuilderMixins).IsAbstract, Is.True);
            Assert.That(typeof(MauiAppBuilderMixins).IsSealed, Is.True);
        }
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup first parameter is MauiAppBuilder.
    /// </summary>
    [Test]
    public void ConfigureReactiveUIPopup_FirstParameterIsMauiAppBuilder()
    {
        // Arrange
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder) });

        // Assert
        Assert.That(methodInfo, Is.Not.Null);
        var parameters = methodInfo!.GetParameters();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(parameters, Has.Length.EqualTo(1));
            Assert.That(parameters[0].ParameterType, Is.SameAs(typeof(MauiAppBuilder)));
            Assert.That(parameters[0].Name, Is.EqualTo("builder"));
        }
    }

    /// <summary>
    /// Tests that ConfigureReactiveUIPopup with backPressHandler has correct parameters.
    /// </summary>
    [Test]
    public void ConfigureReactiveUIPopup_WithBackPressHandler_HasCorrectParameters()
    {
        // Arrange
        var methodInfo = typeof(MauiAppBuilderMixins).GetMethod(
            "ConfigureReactiveUIPopup",
            new[] { typeof(MauiAppBuilder), typeof(Action) });

        // Assert
        Assert.That(methodInfo, Is.Not.Null);
        var parameters = methodInfo!.GetParameters();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(parameters, Has.Length.EqualTo(2));
            Assert.That(parameters[0].ParameterType, Is.SameAs(typeof(MauiAppBuilder)));
            Assert.That(parameters[0].Name, Is.EqualTo("builder"));
            Assert.That(parameters[1].ParameterType, Is.SameAs(typeof(Action)));
            Assert.That(parameters[1].Name, Is.EqualTo("backPressHandler"));
        }
    }

    /// <summary>
    /// Tests that MauiAppBuilderMixins is in correct namespace.
    /// </summary>
    [Test]
    public void MauiAppBuilderMixins_IsInCorrectNamespace() =>
        Assert.That(typeof(MauiAppBuilderMixins).Namespace, Is.EqualTo("Microsoft.Maui.Hosting"));
}
