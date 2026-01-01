namespace MargamParkArchives.Data.Entities.Creator;

internal record CreatorUpdateDto
{
    internal required int CreatorId { get; init; }
    internal required string Name { get; init; }
}
