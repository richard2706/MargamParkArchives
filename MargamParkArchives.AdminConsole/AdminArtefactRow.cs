using MargamParkArchives.Data.Entities.ArtefactEntity;
using MargamParkArchives.Windows.UI.TableRows;
using System;

namespace MargamParkArchives.AdminConsole;

internal class AdminArtefactRow : ArtefactRowBase
{
    private AdminArtefactRowQueryResult RowQueryResult { get; init; }

    // Base class properties
    public override string? IdentifierKey => this.RowQueryResult.identifier_key;
    public override string? IdentifierGroupName => this.RowQueryResult.identifier_group_name;
    public override string? FilePath => this.RowQueryResult.file_path;
    public override DateTime? DateCreated => this.RowQueryResult.date_created;
    public override string? DescriptionEn => this.RowQueryResult.description_en;
    public override bool? VisualArtefact => this.RowQueryResult.visual_artefact;
    public override string? CategoryName => this.RowQueryResult.category_name;
    public override string? CreatorName => this.RowQueryResult.creator_name;
    public override string? PeriodDates => this.RowQueryResult.period_dates;

    // Admin Console specific properties
    public DateTime? DateModified => this.RowQueryResult.date_modified;
    public string? GeneralLocationName => this.RowQueryResult.general_location_name;
    public string? SpecificLocationSummary => this.RowQueryResult.specific_location_summary;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryResult"></param>
    public AdminArtefactRow(AdminArtefactRowQueryResult queryResult)
    {
        ArgumentNullException.ThrowIfNull(queryResult);

        this.RowQueryResult = queryResult;
    }
}
