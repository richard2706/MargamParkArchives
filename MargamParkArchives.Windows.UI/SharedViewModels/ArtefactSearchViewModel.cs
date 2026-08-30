using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using MargamParkArchives.Data.Entities.ArtefactEntity;
using MargamParkArchives.Data.Services;
using MargamParkArchives.Windows.UI.TableRows;

namespace MargamParkArchives.Windows.UI.SharedViewModels;

/// <summary>
/// View model for searching artefacts and displaying the results in a table
/// </summary>
/// <remarks>
/// This is a base view model so must be implemented by a derived view model that specifies the visible columns in the table
/// </remarks>
/// <param name="searchService">Service for searching artefacts</param>
public abstract partial class ArtefactSearchViewModel(IArtefactSearchService searchService) : ObservableObject
{
    private const string SearchPrompt = "Use the tools above to search the archives";

    private readonly IArtefactSearchService _searchService = searchService;

    /// <summary>
    /// List of columns to show in the results table. Must be specified by the derived class.
    /// </summary>
    public abstract string[] VisibleColumns { get; }

    /// <summary>
    /// 
    /// </summary>
    [ObservableProperty]
    private bool showSearchPrompt = true;

    /// <summary>
    /// 
    /// </summary>
    [ObservableProperty]
    private bool showSearchLoadingIndicator = false;

    /// <summary>
    /// 
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ArtefactRow> artefactRows = [];

    /// <summary>
    /// The search text the user has typed into the search textbox
    /// </summary>
    /// <remarks>
    /// No need for ObservableProperty as this is only for reading the value from the view
    /// </remarks>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Show the results table only if there are artefacts to show
    /// </summary>
    public bool ShowResultsTable => !this.ShowSearchLoadingIndicator && (this.ArtefactRows?.Count > 0);

    /// <summary>
    /// 
    /// </summary>
    public bool ShowNoResultsMessage => !this.ShowSearchLoadingIndicator && (this.ArtefactRows?.Count == 0);

    /// <summary>
    /// Text to show in place of the results table before the user has performed a search.
    /// </summary>
    /// <remarks>
    /// Note this cannot be static as the data binding then does not work
    /// </remarks>
    public string SearchPromptText => SearchPrompt;

    /// <summary>
    /// Perform a search from the user's input and show the results.
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task Search()
    {
        if (string.IsNullOrEmpty(this.SearchTerm))
        {
            return; // should show a message to the user instead of doing nothing
        }

        this.ShowSearchPrompt = false;
        this.ShowSearchLoadingIndicator = true;

        this.ArtefactRows.Clear();

        try
        {
            // Result query type is ArtefactRowQueryResultBase, but actual query result will depend on which search service was injected
            IEnumerable<ArtefactRowQueryResultBase> results = await _searchService.SearchArtefactsAsync(this.SearchTerm);   

            foreach (ArtefactRowQueryResultBase queryResult in results)
            {
                ArtefactRow row = queryResult is AdminArtefactRowQueryResult adminQueryResult ? new(adminQueryResult) : new(queryResult);
                this.ArtefactRows.Add(row);
            }
        }
        catch
        {
            this.ShowSearchPrompt = true;
            // show error message
        }
        finally
        {
            this.ShowSearchLoadingIndicator = false;
        }
    }

    partial void OnArtefactRowsChanged(ObservableCollection<ArtefactRow> value)
    {
        OnPropertyChanged(nameof(this.ShowResultsTable));
        OnPropertyChanged(nameof(this.ShowNoResultsMessage));
    }

    partial void OnShowSearchLoadingIndicatorChanged(bool value)
    {
        OnPropertyChanged(nameof(this.ShowResultsTable));
        OnPropertyChanged(nameof(this.ShowNoResultsMessage));
    }
}
