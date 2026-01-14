namespace MargamParkArchives.Core.DataAccess.SpecificLocationEntity;

public record SpecificLocationUpdateDto
{
    public required int SpecificLocationId { get; init; }
    public required string Summary { get; init; }
}
