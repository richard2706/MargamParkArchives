using MargamParkArchives.Core.Entities.CategoryEntity;

namespace MargamParkArchives.Core.DataAccess.CategoryEntity;

public interface ICategoryWriter
{
    public Task<string> CreateCategoryAsync(CategoryCreateDto category);
    public Task<bool> UpdateCategoryAsync(CategoryUpdateDto category);
}
