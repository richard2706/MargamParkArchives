using MargamParkArchives.Core.Entities.CategoryEntity;

namespace MargamParkArchives.Core.Database.DataAccess;

public interface ICategoryReader
{
    public Task<Category[]> GetAllCategoriesAsync();
    public Task<Category?> GetOneCategoryAsync(string id);
}
