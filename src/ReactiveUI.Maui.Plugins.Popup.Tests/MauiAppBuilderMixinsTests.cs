// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using FluentAssertions;
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

        methodInfo.Should().NotBeNull();
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

        methodInfo.Should().NotBeNull();
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

        methodInfo.Should().NotBeNull();
        methodInfo!.ReturnType.Should().Be(typeof(MauiAppBuilder));
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

        methodInfo.Should().NotBeNull();
        methodInfo!.ReturnType.Should().Be(typeof(MauiAppBuilder));
    }

    /// <summary>
    /// Tests that MauiAppBuilderMixins is a static class.
    /// </summary>
    [Test]
    public void MauiAppBuilderMixins_IsStaticClass()
    {
        // Assert
        typeof(MauiAppBuilderMixins).IsAbstract.Should().BeTrue();
        typeof(MauiAppBuilderMixins).IsSealed.Should().BeTrue();
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
        methodInfo.Should().NotBeNull();
        var parameters = methodInfo!.GetParameters();
        parameters.Should().HaveCount(1);
        parameters[0].ParameterType.Should().Be(typeof(MauiAppBuilder));
        parameters[0].Name.Should().Be("builder");
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
        methodInfo.Should().NotBeNull();
        var parameters = methodInfo!.GetParameters();
        parameters.Should().HaveCount(2);
        parameters[0].ParameterType.Should().Be(typeof(MauiAppBuilder));
        parameters[0].Name.Should().Be("builder");
        parameters[1].ParameterType.Should().Be(typeof(Action));
        parameters[1].Name.Should().Be("backPressHandler");
    }

    /// <summary>
    /// Tests that MauiAppBuilderMixins is in correct namespace.
    /// </summary>
    [Test]
    public void MauiAppBuilderMixins_IsInCorrectNamespace() =>
        typeof(MauiAppBuilderMixins).Namespace.Should().Be("Microsoft.Maui.Hosting");
}
