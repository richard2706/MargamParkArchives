using MargamParkArchives.Data.Services;
using MargamParkArchives.Windows.UI.SharedViewModels;
using MargamParkArchives.Windows.UI.TableRows;

namespace MargamParkArchives.AdminConsole;

/// <summary>
/// 
/// </summary>
/// <param name="artefactSearchService"></param>
internal partial class AdminArtefactSearchViewModel(IArtefactSearchService artefactSearchService) :
    ArtefactSearchViewModel(artefactSearchService)
{
    private readonly string[] _visibleColumns = [
        nameof(ArtefactRow.IdentifierKey),
        nameof(ArtefactRow.IdentifierGroupName),
        nameof(ArtefactRow.DateCreated),
        nameof(ArtefactRow.DateModified),
        nameof(ArtefactRow.DescriptionEn),
        nameof(ArtefactRow.IsVisualArtefact),
        nameof(ArtefactRow.CategoryName),
        nameof(ArtefactRow.CreatorName),
        nameof(ArtefactRow.PeriodDates),
        nameof(ArtefactRow.GeneralLocationName),
        nameof(ArtefactRow.SpecificLocationSummary),
    ];

    public override string[] VisibleColumns => this._visibleColumns;
}
