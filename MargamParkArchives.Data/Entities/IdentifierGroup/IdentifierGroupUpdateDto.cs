namespace MargamParkArchives.Data.Entities.IdentifierGroup;

internal record IdentifierGroupUpdateDto
{
    internal required string ExistingIdentifierGroupId { get; init; }
    internal required string NewIdentifierGroupId { get; init; }
    internal required string Name { get; init; }
}
