namespace MargamParkArchives.Data.Entities.SpecificLocationEntity;

internal record SpecificLocationUpdateDto
{
    internal required int SpecificLocationId { get; init; }
    internal required string Summary { get; init; }
}
