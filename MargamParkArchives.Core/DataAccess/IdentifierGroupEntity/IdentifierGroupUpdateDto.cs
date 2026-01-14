namespace MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;

public record IdentifierGroupUpdateDto
{
    public required string ExistingIdentifierGroupId { get; init; }
    public required string NewIdentifierGroupId { get; init; }
    public required string Name { get; init; }
}
