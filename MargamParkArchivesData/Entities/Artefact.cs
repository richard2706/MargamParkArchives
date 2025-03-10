namespace MargamParkArchivesData.Entities;

public record Artefact : ArtefactBase
{
    public required int IdentifierNumber { get; init; }
    public required string IdentifierKey { get; init; }
    public DateTime? DateCreated { get; init; }
    public DateTime? DateModified { get; init; }
}
