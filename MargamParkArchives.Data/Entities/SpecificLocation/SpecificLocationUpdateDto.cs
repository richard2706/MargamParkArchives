namespace MargamParkArchives.Data.Entities.SpecificLocation;

internal record SpecificLocationUpdateDto
{
    internal required int SpecificLocationId { get; init; }
    internal required string Summary { get; init; }
}
