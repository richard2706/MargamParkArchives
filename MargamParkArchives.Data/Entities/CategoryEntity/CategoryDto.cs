using MargamParkArchives.Core.Entities.CategoryEntity;

namespace MargamParkArchives.Data.Entities.CategoryEntity;

internal record CategoryDto
{
#pragma warning disable IDE1006 // Disable name violation warning as property names must match db field names

    internal required string category_id { get; init; }
    internal required string name { get; init; }
    internal DateTime? date_created { get; init; }
    internal DateTime? date_modified { get; init; }

#pragma warning restore IDE1006

    internal Category ToCategory() => new(category_id, name, date_created, date_modified);
}
