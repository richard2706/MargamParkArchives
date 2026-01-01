namespace MargamParkArchives.Data.Entities.Category;

internal record CategoryCreateDto
{
    internal required string CategoryId { get; init; }
    internal required string Name { get; init; }
}
