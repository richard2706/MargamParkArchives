namespace MargamParkArchives.Data.Entities;

internal record SpecificLocationDto
{
    internal required int SpecificLocationId { get; init; }
    internal required string Summary { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }
}
