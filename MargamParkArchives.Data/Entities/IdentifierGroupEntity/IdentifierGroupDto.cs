namespace MargamParkArchives.Data.Entities.IdentifierGroupEntity;

internal record IdentifierGroupDto
{
    internal required string IdentifierGroupId { get; init; }
    internal required string Name { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }
}
