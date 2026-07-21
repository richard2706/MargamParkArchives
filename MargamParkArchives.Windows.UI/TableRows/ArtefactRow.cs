using MargamParkArchives.Data.Entities.ArtefactEntity;
using System;

namespace MargamParkArchives.Windows.UI.TableRows;

/// <summary>
/// Specifies all the available columns that may be displayed in a TableView row for an Artefact
/// </summary>
/// <remarks>
/// Nullable properties specified here because we should account for missing values when displaying them (e.g. value
/// may be null if the database was manually edited)
/// </remarks>
public class ArtefactRow
{
    // Core fields used in all tables
    public string? IdentifierKey { get; }
    public string? IdentifierGroupName { get; }
    public string? FilePath { get; }
    public DateTime? DateCreated { get; }
    public string? DescriptionEn { get; }
    public bool? IsVisualArtefact { get; }
    public string? CategoryName { get; }
    public string? CreatorName { get; }
    public string? PeriodDates { get; }
    
    // Admin fields
    public DateTime? DateModified { get; }
    public string? GeneralLocationName { get; }
    public string? SpecificLocationSummary { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryResult"></param>
    public ArtefactRow(ArtefactRowQueryResultBase queryResult)
    {
        // consider storing original then having properties access, but may not be possible with this structure, so may need to set each value individually and use null as default for all

        this.IdentifierKey = queryResult.identifier_key;
        this.IdentifierGroupName = queryResult.identifier_group_name;
        this.FilePath = queryResult.file_path;
        this.DateCreated = queryResult.date_created;
        this.DescriptionEn = queryResult.description_en;
        this.IsVisualArtefact = queryResult.visual_artefact;
        this.CategoryName = queryResult.category_name;
        this.CreatorName = queryResult.creator_name;
        this.PeriodDates = queryResult.period_dates;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="adminQueryResult"></param>
    public ArtefactRow(AdminArtefactRowQueryResult adminQueryResult) :
        this((ArtefactRowQueryResultBase)adminQueryResult)
    {
        this.DateModified = adminQueryResult.date_modified;
        this.GeneralLocationName = adminQueryResult.general_location_name;
        this.SpecificLocationSummary = adminQueryResult.specific_location_summary;
    }
}
