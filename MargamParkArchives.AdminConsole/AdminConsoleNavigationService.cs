using Microsoft.Extensions.DependencyInjection;
using System;

using MargamParkArchives.Windows.UI;
using MargamParkArchives.Windows.UI.Pages;

namespace MargamParkArchives.AdminConsole;

/// <summary>
/// Navigation service for the admin console which enables navigation between pages and specifies a viewmodel for each page
/// </summary>
/// <param name="serviceProvider"></param>
internal class AdminConsoleNavigationService(IServiceProvider serviceProvider) :
    NavigationServiceBase(serviceProvider)
{
    protected override void ConfigureViewModelFactories()
    {
        _viewModelFactories[typeof(ArtefactSearchPage)] = () => _services.GetRequiredService<AdminArtefactSearchViewModel>();
        _viewModelFactories[typeof(ImportPage)] = () => _services.GetRequiredService<ImportViewModel>();
    }
}