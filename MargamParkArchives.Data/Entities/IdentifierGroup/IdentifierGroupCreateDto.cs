namespace MargamParkArchives.Data.Entities.IdentifierGroup;

internal record IdentifierGroupCreateDto
{
    internal required string IdentifierGroupId { get; init; }
    internal required string Name { get; init; }
}
