namespace MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;

public record IdentifierGroupCreateDto
{
    public required string IdentifierGroupId { get; init; }
    public required string Name { get; init; }
}
