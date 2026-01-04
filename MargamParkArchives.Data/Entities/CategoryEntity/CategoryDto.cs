namespace MargamParkArchives.Data.Entities.CategoryEntity;

internal record CategoryDto
{
    internal required string CategoryId { get; init; }
    internal required string Name { get; init; }
    internal DateTime? DateCreated { get; init; }
    internal DateTime? DateModified { get; init; }
}
