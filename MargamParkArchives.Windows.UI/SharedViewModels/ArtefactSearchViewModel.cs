using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MargamParkArchives.Core.DataAccess.ArtefactEntity;
using MargamParkArchives.Core.Entities.ArtefactDetails;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MargamParkArchives.Windows.UI.SharedViewModels;

public partial class ArtefactSearchViewModel(IArtefactDetailsReader artefactDetailsReader) : ObservableObject
{
    private IArtefactDetailsReader _artefactReader = artefactDetailsReader;

    [ObservableProperty]
    private bool showSearchPrompt = true;

    [ObservableProperty]
    private bool showSearchLoadingIndicator = false;

    [ObservableProperty]
    private ObservableCollection<ArtefactDetails> artefacts;

    /// <summary>
    /// Show the results table only if there are artefacts to show.
    /// </summary>
    public bool ShowResultsTable => !this.ShowSearchLoadingIndicator && (this.Artefacts?.Count > 0);

    [RelayCommand]
    private async Task LoadRandomArtefacts()
    {
        ArtefactDetails[] artefactsArray = await _artefactReader.GetRandomArtefactsAsync();
        this.Artefacts = new ObservableCollection<ArtefactDetails>(artefactsArray);
    }

    /// <summary>
    /// Perform a search from the user's input and show the results.
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task Search()
    {
        this.ShowSearchPrompt = false;
        this.ShowSearchLoadingIndicator = true;
        await this.LoadRandomArtefacts();
        this.ShowSearchLoadingIndicator = false;
    }

    partial void OnArtefactsChanged(ObservableCollection<ArtefactDetails> value)
    {
        OnPropertyChanged(nameof(this.ShowResultsTable));
    }

    partial void OnShowSearchLoadingIndicatorChanged(bool value)
    {
        OnPropertyChanged(nameof(this.ShowResultsTable));
    }
}
