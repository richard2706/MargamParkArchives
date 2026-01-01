namespace MargamParkArchives.Data.Entities.DataTransferObjects;

internal record CreatorDto
{
    internal required int CreatorId { get; init; }
    internal required string Name { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }
}
