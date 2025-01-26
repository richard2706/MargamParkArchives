using MargamParkArchives.Entities.ArtefactSubEntities;
using System;

namespace MargamParkArchives.Entities
{
    internal record ArtefactBase
    {
        internal required string IdentifierGroup { get; init; }
        internal string? FilePath { get; init; }
        internal string? ParentId { get; init; }
        internal string? Notes { get; init; }
        internal bool? IsVisualArtefact { get; init; }
        internal string? LocationCoverage { get; init; }
        internal TitleDescription? TitleDescription { get; init; }
        internal Tags? Tags { get; init; }
        internal RightsInformation? RightsDetails { get; init; }

        // Foreign keys
        internal string? CategoryId { get; init; }
        internal int? PeriodId { get; init; }
        internal int? CreatorId { get; init; }
        internal int? GeneralLocationId { get; init; }
        internal int? SpecificLocationId { get; init; }
    }
}
