namespace MargamParkArchives.Core.DataAccess.CategoryEntity;

public interface ICategoryWriter
{
    /// <summary>
    /// Creates a new category in the database asynchronously.
    /// </summary>
    /// <param name="category">Object containing the values for the new category.</param>
    /// <returns>The id of the newly created category.</returns>
    public Task<string> CreateCategoryAsync(CategoryCreateDto category);

    /// <summary>
    /// Updates an existing category in the database asynchronously.
    /// </summary>
    /// <param name="category">Object containing the updated values for the category.</param>
    /// <returns>True if the category was updated successfully or false if the category was not found.</returns>
    public Task<bool> UpdateCategoryAsync(CategoryUpdateDto category);

    /// <summary>
    /// Deletes an existing category from the database asynchronously.
    /// </summary>
    /// <param name="categoryId">Id of the category to be deleted.</param>
    /// <returns>True if the category was deleted successfully or false if the category was not found.</returns>
    public Task<bool> DeleteCategoryAsync(string categoryId);
}
