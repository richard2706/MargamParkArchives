using MargamParkArchivesData.Entities.ArtefactSubEntities;
using System;

namespace MargamParkArchivesData.Entities
{
    public record ArtefactBase
    {
        public required string IdentifierGroup { get; init; }
        public string? FilePath { get; init; }
        public string? ParentId { get; init; }
        public string? Notes { get; init; }
        public bool? IsVisualArtefact { get; init; }
        public string? LocationCoverage { get; init; }
        public TitleDescription? TitleDescription { get; init; }
        public Tags? Tags { get; init; }
        public RightsInformation? RightsDetails { get; init; }

        // Foreign keys
        public string? CategoryId { get; init; }
        public int? PeriodId { get; init; }
        public int? CreatorId { get; init; }
        public int? GeneralLocationId { get; init; }
        public int? SpecificLocationId { get; init; }
    }
}
