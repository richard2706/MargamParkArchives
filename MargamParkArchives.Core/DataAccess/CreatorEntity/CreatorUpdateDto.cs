namespace MargamParkArchives.Core.DataAccess.CreatorEntity;

public record CreatorUpdateDto
{
    public required int CreatorId { get; init; }
    public required string Name { get; init; }
}
