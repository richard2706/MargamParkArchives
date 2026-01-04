namespace MargamParkArchives.Data.Entities.CreatorEntity;

internal record CreatorUpdateDto
{
    internal required int CreatorId { get; init; }
    internal required string Name { get; init; }
}
