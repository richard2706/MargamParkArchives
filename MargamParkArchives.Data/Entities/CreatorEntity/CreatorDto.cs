using MargamParkArchives.Core.Entities.CreatorEntity;

namespace MargamParkArchives.Data.Entities.CreatorEntity;

internal record CreatorDto
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names
    internal required int creator_id { get; init; }
    internal required string name { get; init; }
    internal DateTime? date_created { get; init; }
    internal DateTime? date_modified { get; init; }

#pragma warning restore IDE1006

    internal Creator ToCreator() => new(creator_id, name, date_created, date_modified);
}
