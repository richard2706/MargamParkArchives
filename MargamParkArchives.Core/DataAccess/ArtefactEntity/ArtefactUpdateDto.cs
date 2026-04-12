namespace MargamParkArchives.Core.DataAccess.ArtefactEntity;

public record ArtefactUpdateDto : ArtefactCreateDto
{
    // IdentifierGroupId and IdentifierNumber form the primary key of an Artefact.
    public required int IdentifierNumber { get; init; }

    /// <summary>
    /// Should be provided if the artefact is to be moved to a different identifier group.
    /// </summary>
    public string? NewIdentifierGroupId { get; init; }
}
