using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.ArtefactDetails;

/// <summary>
/// Holds all details of an artefact and its linked entities. Corresponds to the ArtefactDetails database view.
/// </summary>
/// <remarks>
/// This class is only a data container, business logic (including validation) is found only in the relevant entity class.
/// </remarks>
public record ArtefactDetails
{
    // Identifying attributes
    public string IdentifierGroupId { get; }
    public int IdentifierNumber { get; }
    public string IdentifierKey { get; }
    public string? IdentifierGroupName { get; }

    // Linked entity details
    public string? CategoryId { get; }
    public string? CategoryName { get; }
    public int? CreatorId { get; }
    public string? CreatorName { get; }
    public int? GeneralLocationId { get; }
    public string? GeneralLocationName { get; }
    public int? SpecificLocationId { get; }
    public string? SpecificLocationSummary { get; }
    public int? PeriodId { get; }
    public string? PeriodDates { get; }

    // Artefact details
    public string? FilePath { get; }
    public DateTime? DateArtefactCreated { get; }
    public DateTime? DateArtefactModified { get; }
    public string? ParentId { get; }
    public string? Notes { get; }
    public string? TitleEn { get; }
    public string? TitleCy { get; }
    public string? DescriptionEn { get; }
    public string? DescriptionCy { get; }
    public string? TagsCy { get; }
    public string? CultureTagEn { get; }
    public string? LocationCoverage { get; }
    public string? RightType1 { get; }
    public string? RightHolder1En { get; }
    public string? RightHolder1Cy { get; }
    public bool? VisualArtefact { get; }

    public ArtefactDetails(string identifierGroupId, int identifierNumber, string? identifierKey,
        string? identifierGroupName, string? categoryId = null, string? categoryName = null, int? creatorId = null,
        string? creatorName = null, int? generalLocationId = null, string? generalLocationName = null,
        int? specificLocationId = null, string? specificLocationSummary = null, int? periodId = null,
        string? periodDates = null, string? filePath = null, DateTime? dateArtefactCreated = null,
        DateTime? dateArtefactModified = null, string? parentId = null, string? notes = null, string? titleEn = null,
        string? titleCy = null, string? descriptionEn = null, string? descriptionCy = null, string? tagsCy = null,
        string? cultureTagEn = null, string? locationCoverage = null, string? rightType1 = null,
        string? rightHolder1En = null, string? rightHolder1Cy = null, bool? visualArtefact = null)
    {
        IdentifierGroupId = identifierGroupId;
        IdentifierNumber = identifierNumber;
        IdentifierKey = identifierKey ?? IdentifierKeyHelper.BuildIdentifierKey(identifierGroupId, identifierNumber);
        IdentifierGroupName = identifierGroupName;
        CategoryId = categoryId;
        CategoryName = categoryName;
        CreatorId = creatorId;
        CreatorName = creatorName;
        GeneralLocationId = generalLocationId;
        GeneralLocationName = generalLocationName;
        SpecificLocationId = specificLocationId;
        SpecificLocationSummary = specificLocationSummary;
        PeriodId = periodId;
        PeriodDates = periodDates;
        FilePath = filePath;
        DateArtefactCreated = dateArtefactCreated;
        DateArtefactModified = dateArtefactModified;
        ParentId = parentId;
        Notes = notes;
        TitleEn = titleEn;
        TitleCy = titleCy;
        DescriptionEn = descriptionEn;
        DescriptionCy = descriptionCy;
        TagsCy = tagsCy;
        CultureTagEn = cultureTagEn;
        LocationCoverage = locationCoverage;
        RightType1 = rightType1;
        RightHolder1En = rightHolder1En;
        RightHolder1Cy = rightHolder1Cy;
        VisualArtefact = visualArtefact;
    }
}
