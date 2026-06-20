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
    private ObservableCollection<ArtefactDetails> artefacts;

    [RelayCommand]
    private async Task LoadRandomArtefacts()
    {
        ArtefactDetails[] artefactsArray = await _artefactReader.GetRandomArtefactsAsync();
        this.Artefacts = new ObservableCollection<ArtefactDetails>(artefactsArray);
    }
}
