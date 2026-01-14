namespace MargamParkArchives.Core.DataAccess.CategoryEntity;

public record CategoryCreateDto
{
    public required string CategoryId { get; init; }
    public required string Name { get; init; }
}
