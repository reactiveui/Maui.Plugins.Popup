// Copyright (c) 2024-2025 ReactiveUI and Contributors. All rights reserved.
// Licensed to the ReactiveUI under one or more agreements.
// ReactiveUI licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveUI.Maui.Plugins.Popup;

/// <summary>
/// Base Popup page that implements <see cref="IViewFor{TViewModel}"/>.
/// </summary>
/// <typeparam name="TViewModel">The type of the ViewModel that this page will bind to. Must be a reference type.</typeparam>
/// <remarks>
/// This generic variant of <see cref="ReactivePopupPage"/> provides strongly-typed ViewModel binding,
/// offering compile-time type safety for your popup views. It inherits all the reactive binding capabilities
/// from the base <see cref="ReactivePopupPage"/> class while constraining the ViewModel to a specific type.
/// The type parameter ensures that your view is always bound to the correct ViewModel type, preventing
/// runtime type errors.
/// </remarks>
public abstract class ReactivePopupPage<TViewModel> : ReactivePopupPage, IViewFor<TViewModel>
    where TViewModel : class
{
    /// <summary>
    /// The view model property.
    /// </summary>
    public static new readonly BindableProperty ViewModelProperty = BindableProperty.Create(
         nameof(ViewModel),
         typeof(TViewModel),
         typeof(ReactivePopupPage<TViewModel>),
         default(TViewModel),
         BindingMode.OneWay,
         propertyChanged: OnViewModelChanged);

    /// <summary>
    /// Gets or sets the ViewModel to display.
    /// </summary>
    /// <remarks>
    /// Setting this property automatically updates the underlying <see cref="Microsoft.Maui.Controls.BindableObject.BindingContext"/>
    /// of the MAUI page. This property provides strongly-typed access to the ViewModel, ensuring type safety
    /// when accessing ViewModel properties and methods from code-behind or XAML.
    /// </remarks>
    public new TViewModel? ViewModel
    {
        get => (TViewModel?)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    /// <inheritdoc/>
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        ViewModel = BindingContext as TViewModel;
    }
}
