using MargamParkArchivesData.Entities.ArtefactSubEntities;

namespace MargamParkArchivesData.Entities;

public record ArtefactBase
{
    public required IdentifierGroup IdentifierGroup { get; init; }
    public string? FilePath { get; init; }
    public string? ParentId { get; init; }
    public string? Notes { get; init; }
    public bool? IsVisualArtefact { get; init; }
    public string? LocationCoverage { get; init; }
    public TitleDescription? TitleDescription { get; init; }
    public Tags? Tags { get; init; }
    public RightsInformation? RightsDetails { get; init; }

    // Linked entities
    public Category? Category { get; init; }
    public Period? Period { get; init; }
    public Creator? Creator { get; init; }
    public GeneralLocation? GeneralLocation { get; init; }
    public SpecificLocation? SpecificLocation { get; init; }
}
