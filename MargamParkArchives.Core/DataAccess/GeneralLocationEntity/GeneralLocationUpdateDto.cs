namespace MargamParkArchives.Core.DataAccess.GeneralLocationEntity;

public record GeneralLocationUpdateDto
{
    public required int GeneralLocationId { get; init; }
    public required string Name { get; init; }
}
