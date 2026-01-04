namespace MargamParkArchives.Data.Entities.GeneralLocationEntity;

internal record GeneralLocationUpdateDto
{
    internal required int GeneralLocationId { get; init; }
    internal required string Name { get; init; }
}
