namespace MargamParkArchives.Data.Entities.GeneralLocation;

internal record GeneralLocationDto
{
    internal required int GeneralLocationId { get; init; }
    internal required string Name { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }
}
