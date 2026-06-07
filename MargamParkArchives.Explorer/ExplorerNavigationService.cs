using CommunityToolkit.Mvvm.ComponentModel;
using MargamParkArchives.Windows.UI;
using MargamParkArchives.Windows.UI.SharedViewModels;
using MargamParkArchives.Windows.UI.SharedViews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace MargamParkArchives.Explorer;

public class ExplorerNavigationService : INavigationService
{
    private const string NotInitialisedMessage = "The navigation service has not been initialised using Initialise(Frame).";
    private const string ViewModelNotRegisteredMessage = "The view model for the specified page has not been registered.";

    private IServiceProvider _services;
    private readonly Dictionary<Type, Func<ObservableObject>> _viewModelFactories = new();
    private bool _initialised = false;
    private Frame? _frame;

    /// <summary>
    /// Creates a new instance of the navigation service
    /// </summary>
    /// <param name="serviceProvider">Service provider for resolving dependencies. Provided automatically by the DI container</param>
    public ExplorerNavigationService(IServiceProvider serviceProvider)
    {
        _services = serviceProvider;
        this.ConfigureViewModelFactories();
    }

    public void Initialise(Frame frame)
    {
        if (_initialised)
        {
            return;
        }

        _frame = frame;
        _initialised = true;
    }

    public void NavigateTo(Type pageType)
    {
        if (!_initialised)
        {
            throw new InvalidOperationException(ExplorerNavigationService.NotInitialisedMessage);
        }

        if (!_viewModelFactories.TryGetValue(pageType, out Func<ObservableObject>? viewModelFactory))
        {
            throw new InvalidOperationException(ExplorerNavigationService.ViewModelNotRegisteredMessage);
        }

        ObservableObject viewModel = viewModelFactory();
        _frame?.Navigate(pageType, viewModel);
    }

    public bool CanGoBack()
    {
        if (!_initialised)
        {
            throw new InvalidOperationException(ExplorerNavigationService.NotInitialisedMessage);
        }

        return _frame?.CanGoBack ?? false;
    }

    public void NavigateBack()
    {
        if (!_initialised)
        {
            throw new InvalidOperationException(ExplorerNavigationService.NotInitialisedMessage);
        }

        if (this.CanGoBack())
        {
            _frame?.GoBack();
        }
    }

    /// <summary>
    /// Register a factory corresponding to each page which that creates a viewmodel for the page
    /// </summary>
    private void ConfigureViewModelFactories()
    {
        _viewModelFactories[typeof(ArtefactSearchPage)] = () => _services.GetRequiredService<ArtefactSearchViewModel>();
    }
}
