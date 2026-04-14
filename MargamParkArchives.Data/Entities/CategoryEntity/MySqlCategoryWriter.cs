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

    private const string UpdateCategoryQuery = "update {0} set category_id = @NewCategoryId, name = @Name where category_id = @ExistingCategoryId;";

    public async Task<string> CreateCategoryAsync(CategoryCreateDto category)
    {
        bool idIsValid = CategoryValidationRules.IsValidCategoryId(category.CategoryId, nameof(category.CategoryId), out string idError);
        if (!idIsValid)
        {
            throw new ValidationException(idError, nameof(category.CategoryId));
        }

        bool nameIsValid = CategoryValidationRules.IsValidName(category.Name, nameof(category.Name), out string nameError);
        if (!nameIsValid)
        {
            throw new ValidationException(nameError, nameof(category.Name));
        }

        string sqlQuery = string.Format(InsertCategoryQuery, DatabaseConstants.CategoryTableName);
        bool success = await _dataAccess.InsertAsync<CategoryCreateDto>(sqlQuery, category);
        return success ? category.CategoryId : throw new DatabaseException(CreateCategoryFailedMessage);
    }

    public async Task<bool> UpdateCategoryAsync(CategoryUpdateDto category)
    {
        bool isNewIdValid = CategoryValidationRules.IsValidCategoryId(category.NewCategoryId, nameof(category.NewCategoryId), out string newIdError);
        if (!isNewIdValid)
        {
            throw new ValidationException(newIdError, nameof(category.NewCategoryId));
        }

        bool isNameValid = CategoryValidationRules.IsValidName(category.Name, nameof(category.Name), out string nameError);
        if (!isNameValid)
        {
            throw new ValidationException(nameError, nameof(category.Name));
        }

        string sqlQuery = string.Format(UpdateCategoryQuery, DatabaseConstants.CategoryTableName);
        return await _dataAccess.UpdateAsync<CategoryUpdateDto>(sqlQuery, category);
    }
}
