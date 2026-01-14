using MargamParkArchives.Core.Entities.CategoryEntity;

namespace MargamParkArchives.Core.DataAccess.CategoryEntity;

public interface ICategoryReader
{
    public Task<Category[]> GetAllCategoriesAsync();
    public Task<Category?> GetOneCategoryAsync(string id);
}
