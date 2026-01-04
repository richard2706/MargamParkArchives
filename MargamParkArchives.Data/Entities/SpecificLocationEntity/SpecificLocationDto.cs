using MargamParkArchives.Core.Entities.SpecificLocationEntity;

namespace MargamParkArchives.Data.Entities.SpecificLocationEntity;

internal record SpecificLocationDto
{
    internal required int SpecificLocationId { get; init; }
    internal required string Summary { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }

    internal SpecificLocation ToSpecificLocation() => new(SpecificLocationId, Summary, DateCreated, DateModified);
}
