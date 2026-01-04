namespace MargamParkArchives.Data.Entities.CategoryEntity;

internal record CategoryUpdateDto
{
    internal required string ExistingCategoryId { get; init; }
    internal required string NewCategoryId { get; init; }
    internal required string Name { get; init; }
}
