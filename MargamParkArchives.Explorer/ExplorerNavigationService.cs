using MargamParkArchives.Windows.UI;
using MargamParkArchives.Windows.UI.SharedViews;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MargamParkArchives.Explorer;

/// <summary>
/// Navigation service for the explorer which enables navigation between pages and specifies a viewmodel for each page
/// </summary>
/// <param name="serviceProvider"></param>
public class ExplorerNavigationService(IServiceProvider serviceProvider) : NavigationServiceBase(serviceProvider)
{
    /// <summary>
    /// Register a factory corresponding to each page which that creates a viewmodel for the page
    /// </summary>
    protected override void ConfigureViewModelFactories()
    {
        _viewModelFactories[typeof(ArtefactSearchPage)] = () => _services.GetRequiredService<ExplorerArtefactSearchViewModel>();
    }
}
