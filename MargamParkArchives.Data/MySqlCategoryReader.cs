using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CategoryEntity;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlCategoryReader(IMySqlDataAccess dataAccess) : ICategoryReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllCategoriesQuery = "select * from {0};";

    /// <summary>
    /// Returns an array of all categories in the database which will be empty if the database contains no categories.
    /// </summary>
    /// <returns>An array of all categories in the database which will be empty if the database contains no categories.</returns>
    public async Task<Category[]> GetAllCategoriesAsync()
    {
        string sqlQuery = string.Format(GetAllCategoriesQuery, CategoryTableName);
        IEnumerable<CategoryDto> categoryDtos = await _dataAccess.GetManyItemsAsync<CategoryDto>(sqlQuery);
        return categoryDtos.Select(dto => dto.ToCategory()).ToArray();
    }

    public async Task<Category?> GetOneCategoryAsync(string id)
    {
        throw new NotImplementedException();
    }
}
