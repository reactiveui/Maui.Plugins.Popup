// Copyright (c) 2024 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Mopups.Pages;

namespace ReactiveUI.Maui.Plugins.Popup;

/// <summary>
/// Base Popup page that implements <see cref="IViewFor"/>.
/// </summary>
/// <remarks>
/// This class serves as the bridge between Mopups' <see cref="PopupPage"/> and ReactiveUI's ViewModel binding infrastructure.
/// It enables declarative, reactive bindings between your popup views and ViewModels, providing automatic synchronization
/// through the <see cref="IViewFor"/> interface. The class automatically manages the relationship between the
/// <see cref="ViewModel"/> property and the underlying <see cref="Microsoft.Maui.Controls.BindableObject.BindingContext"/>.
/// </remarks>
public abstract class ReactivePopupPage : PopupPage, IViewFor
{
    /// <summary>
    /// The view model property.
    /// </summary>
    public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(
        nameof(ViewModel),
        typeof(object),
        typeof(IViewFor<object>),
        default,
        BindingMode.OneWay,
        propertyChanged: OnViewModelChanged);

    /// <summary>
    /// Initializes a new instance of the <see cref="ReactivePopupPage"/> class.
    /// </summary>
    protected ReactivePopupPage() => BackgroundClick =
            Observable.FromEvent<EventHandler, Unit>(
                    handler =>
                    {
                        void EventHandler(object? sender, EventArgs args) => handler(Unit.Default);
                        return EventHandler;
                    },
                    x => BackgroundClicked += x,
                    x => BackgroundClicked -= x)
                .Select(_ => Unit.Default);

    /// <summary>
    /// Gets or sets an observable sequence that emits a <see cref="Unit"/> value each time the user taps the background area of the popup.
    /// </summary>
    /// <value>The background click.</value>
    /// <remarks>
    /// This is a Hot Observable derived from the <see cref="PopupPage.BackgroundClicked"/> event.
    /// Hot Observables produce values regardless of subscriptions, though in this case the underlying event
    /// requires the page to be active. This allows for declarative subscription to background tap gestures,
    /// commonly used to dismiss the popup when the user clicks outside the content area.
    /// The setter is protected to allow derived classes to customize the observable behavior if needed.
    /// </remarks>
    public IObservable<Unit> BackgroundClick { get; protected set; }

    /// <summary>
    /// Gets or sets the ViewModel to display.
    /// </summary>
    /// <remarks>
    /// Setting this property automatically updates the underlying <see cref="Microsoft.Maui.Controls.BindableObject.BindingContext"/>
    /// of the MAUI page, ensuring that data bindings in XAML and code-behind remain synchronized with the ViewModel.
    /// This two-way synchronization is maintained through the <see cref="OnViewModelChanged"/> callback and
    /// <see cref="OnBindingContextChanged"/> override.
    /// </remarks>
    public object? ViewModel
    {
        get => GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    /// <summary>
    /// Gets the control binding disposable.
    /// </summary>
    protected CompositeDisposable ControlBindings { get; } = [];

    /// <summary>
    /// Called when [view model changed].
    /// </summary>
    /// <param name="bindableObject">The bindable object.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected static void OnViewModelChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
        ArgumentNullException.ThrowIfNull(bindableObject);
        bindableObject.BindingContext = newValue;
    }

    /// <inheritdoc/>
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        ViewModel = BindingContext;
    }
}
