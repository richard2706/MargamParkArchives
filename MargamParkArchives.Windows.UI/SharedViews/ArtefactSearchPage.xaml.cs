using MargamParkArchives.Windows.UI.SharedViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace MargamParkArchives.Windows.UI.SharedViews;

/// <summary>
/// Page for the user to search the artefact database and filter the results.
/// </summary>
public sealed partial class ArtefactSearchPage : Page
{
    private const string ViewModelInvalidMessage = $"Navigation to {nameof(ArtefactSearchPage)} requires a ViewModel of type {nameof(ArtefactSearchViewModel)}.";
    private ArtefactSearchViewModel? _viewModel;

    public ArtefactSearchPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is ArtefactSearchViewModel viewModel)
        {
            _viewModel = viewModel;
            //this.DataContext = _viewModel; // Only needed when using {Binding ...} in XAML
        }
        else
        {
            throw new ArgumentException(ArtefactSearchPage.ViewModelInvalidMessage);
        }
    }
}
