namespace MargamParkArchives.Data.Entities.DataTransferObjects;

internal record ArtefactDto
{
    internal required string IdentifierGroupId { get; init; }
    internal required int IdentifierNumber { get; init; }
    internal string? IdentifierKey { get; init; }
    internal string? FilePath { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }
    internal string? ParentId { get; init; }
    internal string? Notes { get; init; }
    internal string? TitleEn { get; init; }
    internal string? TitleCy { get; init; }
    internal string? DescriptionEn { get; init; }
    internal string? DescriptionCy { get; init; }
    internal string? CategoryId { get; init; }
    internal string? TagsCy { get; init; }
    internal string? CultureTagEn { get; init; }
    internal int? PeriodId { get; init; }
    internal int? CreatorId { get; init; }
    internal string? LocationCoverage { get; init; }
    internal string? RightType1 { get; init; }
    internal string? RightHolder1En { get; init; }
    internal string? RightHolder1Cy { get; init; }
    internal bool? VisualArtefact { get; init; }
    internal int? GeneralLocationId { get; init; }
    internal int? SpecificLocationId { get; init; }
}
