namespace MargamParkArchives.Data.Entities.GeneralLocation;

internal record GeneralLocationUpdateDto
{
    internal required int GeneralLocationId { get; init; }
    internal required string Name { get; init; }
}
