using MargamParkArchives.Core.Entities.IdentifierGroupEntity;

namespace MargamParkArchives.Data.Entities.IdentifierGroupEntity;

internal record IdentifierGroupDto
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names
    internal required string identifier_group_id { get; init; }
    internal required string name { get; init; }
    internal DateTime? date_created { get; init; }
    internal DateTime? date_modified { get; init; }

#pragma warning restore IDE1006

    internal IdentifierGroup ToIdentifierGroup() => new(identifier_group_id, name, date_created, date_modified);
}
