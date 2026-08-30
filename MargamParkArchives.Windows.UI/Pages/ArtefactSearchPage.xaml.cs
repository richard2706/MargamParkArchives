using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace MargamParkArchives.Windows.UI.Pages;

/// <summary>
/// Page for the user to search the artefact database and filter the results.
/// </summary>
public sealed partial class ArtefactSearchPage : Page
{
    private ArtefactSearchViewModel? _viewModel;

    /// <summary>
    /// 
    /// </summary>
    public ArtefactSearchPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <exception cref="ArgumentException"></exception>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is ArtefactSearchViewModel viewModel)
        {
            _viewModel = viewModel;
            this.DataContext = _viewModel; // Only needed when using {Binding ...} in XAML
        }
        else
        {
            string message = string.Format(WindowsUIConstants.ViewModelInvalidMessage, nameof(ArtefactSearchPage), nameof(ArtefactSearchViewModel));
            throw new ArgumentException(message);
        }
    }

    /// <summary>
    /// Gets the visibility of a column based on the view model's ColumnVisibility dictionary
    /// </summary>
    /// <param name="columnName">The name of the column to check</param>
    /// <returns>The visibility of the column</returns>
    private Visibility GetColumnVisibility(string columnName)
    {
        if (_viewModel?.VisibleColumns != null && _viewModel.VisibleColumns.Contains(columnName))
        {
            return Visibility.Visible;
        }

        return Visibility.Collapsed;
    }
}
