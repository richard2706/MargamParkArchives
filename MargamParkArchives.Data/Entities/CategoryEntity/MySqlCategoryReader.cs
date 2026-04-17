using MargamParkArchives.Core.DataAccess.CategoryEntity;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.CategoryEntity;

public class MySqlCategoryReader(IMySqlDataAccess dataAccess) : ICategoryReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllCategoriesQuery = "select * from {0};";
    private const string GetOneCategoryQuery = "select * from {0} where category_id = @CategoryId;";
    private const string CheckCategoryExistsQuery = "select exists(select 1 from {0} where category_id = @CategoryId);";

    public async Task<Category[]> GetAllCategoriesAsync()
    {
        string sqlQuery = string.Format(GetAllCategoriesQuery, CategoryTableName);
        IEnumerable<CategoryDto> categoryDtos = await _dataAccess.GetManyItemsAsync<CategoryDto>(sqlQuery);
        return categoryDtos.Select(dto => dto.ToCategory()).ToArray();
    }

    public async Task<Category?> GetOneCategoryAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException(ValidationMessages.EmptyStringIdErrorMessage, nameof(id));
        }

        string sqlQuery = string.Format(GetOneCategoryQuery, CategoryTableName);
        CategoryDto? categoryDto = await _dataAccess.GetOneItemAsync<CategoryDto?, object>(sqlQuery, new { CategoryId = id });
        return categoryDto?.ToCategory();
    }

    public async Task<bool> CategoryExists(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException(ValidationMessages.EmptyStringIdErrorMessage, nameof(id));
        }

        string sqlQuery = string.Format(CheckCategoryExistsQuery, CategoryTableName);
        return await _dataAccess.ExistsAsync<object>(sqlQuery, new { CategoryId = id });
    }
}
