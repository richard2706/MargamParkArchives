namespace MargamParkArchives.Core.DataAccess.CategoryEntity;

public record CategoryUpdateDto
{
    public required string ExistingCategoryId { get; init; }
    public required string NewCategoryId { get; init; }
    public required string Name { get; init; }
}
