using MargamParkArchives.Core.Entities.CategoryEntity;

namespace MargamParkArchives.Core.DataAccess.CategoryEntity;

public interface ICategoryReader
{
    /// <summary>
    /// Returns an array of all categories in the database which will be empty if the database contains no categories.
    /// </summary>
    /// <returns>An array of all categories in the database which will be empty if the database contains no categories.</returns>
    public Task<Category[]> GetAllCategoriesAsync();

    /// <summary>
    /// Returns one category from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the category.</param>
    /// <returns>The category identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is an empty string then it is invalid.</exception>
    public Task<Category?> GetOneCategoryAsync(string id);

    /// <summary>
    /// Returns true if a category provided id exists in the database.
    /// </summary>
    /// <param name="id">Id of the category to check</param>
    /// <returns>True if the category identified by the provided id exists</returns>
    public Task<bool> CategoryExists(string id);
}
