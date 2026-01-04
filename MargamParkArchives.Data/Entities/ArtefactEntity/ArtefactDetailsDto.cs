using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;

namespace MargamParkArchives.Data.Entities.ArtefactEntity;

internal class ArtefactDetailsDto
{
    internal required string IdentifierGroupId { get; init; }
    internal required int IdentifierNumber { get; init; }
    internal string? IdentifierKey { get; init; }
    internal string? IdentiferGroupName { get; init; }
    internal string? FilePath { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }
    internal string? ParentId { get; init; }
    internal string? Notes { get; init; }
    internal string? TitleEn { get; init; }
    internal string? TitleCy { get; init; }
    internal string? DescriptionEn { get; init; }
    internal string? DescriptionCy { get; init; }
    internal string? TagsCy { get; init; }
    internal string? CultureTagEn { get; init; }
    internal string? LocationCoverage { get; init; }
    internal string? RightType1 { get; init; }
    internal string? RightHolder1En { get; init; }
    internal string? RightHolder1Cy { get; init; }
    internal bool? VisualArtefact { get; init; }
    internal string? CategoryId { get; init; }
    internal string? CategoryName { get; init; }
    internal int? CreatorId { get; init; }
    internal string? CreatorName { get; init; }
    internal int? GeneralLocationId { get; init; }
    internal string? GeneralLocationName { get; init; }
    internal int? SpecificLocationId { get; init; }
    internal string? SpecificLocationSummary { get; init; }
    internal int? PeriodId { get; init; }
    internal string? PeriodDates { get; init; }

    internal ArtefactDetailsReadModel ToArtefactDetailsReadModel() => new(IdentifierGroupId, IdentifierNumber,
        IdentifierKey, IdentiferGroupName, CategoryId, CategoryName, CreatorId, CreatorName, GeneralLocationId,
        GeneralLocationName, SpecificLocationId, SpecificLocationSummary, PeriodId, PeriodDates, FilePath, DateCreated,
        DateModified, ParentId, Notes, TitleEn, TitleCy, DescriptionEn, DescriptionCy, TagsCy, CultureTagEn,
        LocationCoverage, RightType1, RightHolder1En, RightHolder1Cy, VisualArtefact);
}
