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
    private const string GetOneCategoryQuery = "select * from {0} where category_id = @CategoryId;";

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
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Id cannot be an empty string.", nameof(id));
        }

        string sqlQuery = string.Format(GetOneCategoryQuery, CategoryTableName);
        CategoryDto? categoryDto = await _dataAccess.GetOneItemAsync<CategoryDto?, object>(sqlQuery, new { CategoryId = id });
        return categoryDto?.ToCategory();
    }
}
