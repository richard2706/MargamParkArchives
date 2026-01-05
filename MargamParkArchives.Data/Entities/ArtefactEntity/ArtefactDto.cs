using MargamParkArchives.Core.Entities.ArtefactEntity;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Core.Entities.PeriodEntity;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;

namespace MargamParkArchives.Data.Entities.ArtefactEntity;

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

    internal Artefact ToArtefact(IdentifierGroup identifierGroup, Category? category, Period? period, Creator? creator,
        GeneralLocation? generalLocation, SpecificLocation? specificLocation)
    {
        ArtefactRightsInformation rightsInformation = new(RightType1, RightHolder1En, RightHolder1Cy);
        ArtefactContent content = new(TitleEn, TitleCy, DescriptionEn, DescriptionCy, Notes);
        ArtefactClassification classification = new(ParentId, TagsCy, CultureTagEn, LocationCoverage, VisualArtefact);

        return new(identifierGroup, IdentifierNumber, IdentifierKey, category, period, creator, generalLocation,
            specificLocation, FilePath, DateCreated, DateModified, rightsInformation, content, classification);
    }
}
