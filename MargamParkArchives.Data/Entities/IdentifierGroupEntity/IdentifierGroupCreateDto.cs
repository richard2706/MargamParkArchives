namespace MargamParkArchives.Data.Entities.IdentifierGroupEntity;

internal record IdentifierGroupCreateDto
{
    internal required string IdentifierGroupId { get; init; }
    internal required string Name { get; init; }
}
