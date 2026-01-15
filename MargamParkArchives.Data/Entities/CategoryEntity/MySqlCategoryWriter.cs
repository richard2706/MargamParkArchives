using MargamParkArchives.Core.DataAccess.CategoryEntity;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;

namespace MargamParkArchives.Data.Entities.CategoryEntity;

public class MySqlCategoryWriter(IMySqlDataAccess dataAccess) : ICategoryWriter
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string InsertCategoryQuery = "insert into {0} (category_id, name) values (@CategoryId, @Name);";
    private const string CreateCategoryFailedMessage = "Failed to create the new category in the database.";


    /// <summary>
    /// Creates a new category in the database asynchronously.
    /// </summary>
    /// <param name="category">Object containing the values for the new category.</param>
    /// <returns>The id of the newly created category.</returns>
    public async Task<string> CreateCategoryAsync(CategoryCreateDto category)
    {
        bool idIsValid = CategoryRules.IsValidId(category.CategoryId, nameof(category.CategoryId), out string idError);
        if (!idIsValid)
        {
            throw new ValidationException(idError, nameof(category.CategoryId));
        }

        bool nameIsValid = CategoryRules.IsValidName(category.Name, nameof(category.Name), out string nameError);
        if (!nameIsValid)
        {
            throw new ValidationException(nameError, nameof(category.Name));
        }

        string sqlQuery = string.Format(InsertCategoryQuery, DatabaseConstants.CategoryTableName);
        bool success = await _dataAccess.InsertAsync<CategoryCreateDto>(sqlQuery, category);
        return success ? category.CategoryId : throw new DatabaseException(CreateCategoryFailedMessage);
    }
}
