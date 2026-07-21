using MargamParkArchives.Data.Services;
using MargamParkArchives.Windows.UI.SharedViewModels;
using MargamParkArchives.Windows.UI.TableRows;

namespace MargamParkArchives.Explorer;

internal partial class ExplorerArtefactSearchViewModel(IArtefactSearchService artefactSearchService)
    : ArtefactSearchViewModel(artefactSearchService)
{
    private readonly string[] _visibleColumns = [
        nameof(ArtefactRow.IdentifierKey),
        nameof(ArtefactRow.IdentifierGroupName),
        nameof(ArtefactRow.DateCreated),
        nameof(ArtefactRow.DescriptionEn),
        nameof(ArtefactRow.IsVisualArtefact),
        nameof(ArtefactRow.CategoryName),
        nameof(ArtefactRow.CreatorName),
        nameof(ArtefactRow.PeriodDates),
    ];

    public override string[] VisibleColumns => this._visibleColumns;
}
