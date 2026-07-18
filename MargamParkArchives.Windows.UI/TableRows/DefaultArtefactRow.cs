using MargamParkArchives.Data.Entities.ArtefactEntity;
using System;

namespace MargamParkArchives.Windows.UI.TableRows;

/// <summary>
/// Holds the data for a row in a default Artefact TableView from a query result.
/// </summary>
public class DefaultArtefactRow : ArtefactRowBase
{
    private ArtefactRowQueryResultBase RowQueryResult { get; init; }

    public override string? IdentifierKey => this.RowQueryResult.identifier_key;
    public override string? IdentifierGroupName => this.RowQueryResult.identifier_group_name;
    public override string? FilePath => this.RowQueryResult.file_path;
    public override DateTime? DateCreated => this.RowQueryResult.date_created;
    public override string? DescriptionEn => this.RowQueryResult.description_en;
    public override bool? VisualArtefact => this.RowQueryResult.visual_artefact;
    public override string? CategoryName => this.RowQueryResult.category_name;
    public override string? CreatorName => this.RowQueryResult.creator_name;
    public override string? PeriodDates => this.RowQueryResult.period_dates;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryResult"></param>
    public DefaultArtefactRow(ArtefactRowQueryResultBase queryResult)
    {
        ArgumentNullException.ThrowIfNull(queryResult);

        this.RowQueryResult = queryResult;
    }
}
