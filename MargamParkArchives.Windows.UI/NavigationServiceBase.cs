using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace MargamParkArchives.Windows.UI;

public abstract class NavigationServiceBase : INavigationService
{
    private const string NotInitialisedMessage = "The navigation service has not been initialised using Initialise(Frame).";
    private const string ViewModelNotRegisteredMessage = "The view model for the specified page has not been registered.";

    private bool _initialised = false;
    private Frame? _frame;

    protected readonly IServiceProvider _services;
    protected readonly Dictionary<Type, Func<ObservableObject>> _viewModelFactories = new();

    /// <summary>
    /// Creates a new instance of the navigation service
    /// </summary>
    /// <param name="serviceProvider">Service provider for resolving dependencies. Provided automatically by the DI container</param>
    public NavigationServiceBase(IServiceProvider serviceProvider)
    {
        _services = serviceProvider;
        this.ConfigureViewModelFactories();
    }

    /// <summary>
    /// Initialise to set the frame object that the navigation service operates on. Must be called before any other
    /// methods can be called
    /// </summary>
    /// <param name="frame">The frame that the navigation service will operate on</param>
    public void Initialise(Frame frame)
    {
        if (_initialised)
        {
            return;
        }

        _frame = frame;
        _initialised = true;
    }

    /// <summary>
    /// Navigate the frame to the specified page
    /// </summary>
    /// <param name="pageType">Type of the page to navigate to</param>
    /// <exception cref="InvalidOperationException">If the navigation service is not initialised or there is no viewmodel
    /// registered for the specified page</exception>
    public void NavigateTo(Type pageType)
    {
        if (!_initialised)
        {
            throw new InvalidOperationException(NavigationServiceBase.NotInitialisedMessage);
        }

        if (!_viewModelFactories.TryGetValue(pageType, out Func<ObservableObject>? viewModelFactory))
        {
            throw new InvalidOperationException(NavigationServiceBase.ViewModelNotRegisteredMessage);
        }

        ObservableObject viewModel = viewModelFactory();
        _frame?.Navigate(pageType, viewModel);
    }

    /// <summary>
    /// Returns true if there is a previous page to navigate to
    /// </summary>
    /// <returns>True if there is a previous page to navigate to</returns>
    /// <exception cref="InvalidOperationException">If the navigation service has not been initialised</exception>
    public bool CanGoBack()
    {
        if (!_initialised)
        {
            throw new InvalidOperationException(NavigationServiceBase.NotInitialisedMessage);
        }

        return _frame?.CanGoBack ?? false;
    }

    /// <summary>
    /// Navigate the frame to the previous page, if there is a previous page
    /// </summary>
    /// <exception cref="InvalidOperationException">If the navigation service is not initialised</exception>
    public void NavigateBack()
    {
        if (!_initialised)
        {
            throw new InvalidOperationException(NavigationServiceBase.NotInitialisedMessage);
        }

        if (this.CanGoBack())
        {
            _frame?.GoBack();
        }
    }

    protected abstract void ConfigureViewModelFactories();
}
