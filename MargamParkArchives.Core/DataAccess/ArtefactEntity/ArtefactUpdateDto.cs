namespace MargamParkArchives.Core.DataAccess.ArtefactEntity;

public record ArtefactUpdateDto
{
    // Identify the artefact. These values together are the primary key of an Artefact.
    public required string IdentifierGroupId { get; init; }
    public required int IdentifierNumber { get; init; }

    // Values to update
    public string? FilePath { get; init; }
    public string? ParentId { get; init; }
    public string? Notes { get; init; }
    public string? TitleEn { get; init; }
    public string? TitleCy { get; init; }
    public string? DescriptionEn { get; init; }
    public string? DescriptionCy { get; init; }
    public string? CategoryId { get; init; }
    public string? TagsCy { get; init; }
    public string? CultureTagEn { get; init; }
    public int? PeriodId { get; init; }
    public int? CreatorId { get; init; }
    public string? LocationCoverage { get; init; }
    public string? RightType1 { get; init; }
    public string? RightHolder1En { get; init; }
    public string? RightHolder1Cy { get; init; }
    public bool? VisualArtefact { get; init; }
    public int? GeneralLocationId { get; init; }
    public int? SpecificLocationId { get; init; }
}
