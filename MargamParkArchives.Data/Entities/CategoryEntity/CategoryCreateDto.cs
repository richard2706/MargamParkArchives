namespace MargamParkArchives.Data.Entities.CategoryEntity;

internal record CategoryCreateDto
{
    internal required string CategoryId { get; init; }
    internal required string Name { get; init; }
}
