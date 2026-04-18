using MargamParkArchives.Core.Entities.GeneralLocationEntity;

namespace MargamParkArchives.Data.Entities.GeneralLocationEntity;

internal record GeneralLocationDto
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names

    internal required int general_location_id { get; init; }
    internal required string name { get; init; }
    internal DateTime? date_created { get; init; }
    internal DateTime? date_modified { get; init; }

#pragma warning restore IDE1006

    internal GeneralLocation ToGeneralLocation() => new(general_location_id, name, date_created, date_modified);
}
