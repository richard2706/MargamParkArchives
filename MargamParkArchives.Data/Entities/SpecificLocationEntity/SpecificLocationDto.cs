using MargamParkArchives.Core.Entities.SpecificLocationEntity;

namespace MargamParkArchives.Data.Entities.SpecificLocationEntity;

internal record SpecificLocationDto
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names

    internal required int specific_location_id { get; init; }
    internal required string summary { get; init; }
    internal DateTime? date_created { get; init; }
    internal DateTime? date_modified { get; init; }

#pragma warning restore IDE1006

    internal SpecificLocation ToSpecificLocation() => new(specific_location_id, summary, date_created, date_modified);
}
