namespace MargamParkArchives.Core.DataAccess.CreatorEntity;

public interface ICreatorWriter
{
    /// <summary>
    /// Creates a new category in the database asynchronously.
    /// </summary>
    /// <param name="creator">Object containing the values for the new creator.</param>
    /// <returns>The id of the newly created creator.</returns>
    public Task<int> CreateCreatorAsync(CreatorCreateDto creator);

    /// <summary>
    /// Updates an existing creator in the database asynchronously.
    /// </summary>
    /// <param name="category">Object containing the updated values for the creator.</param>
    /// <returns>True if the creator was updated successfully or false if the creator was not found.</returns>
    public Task<bool> UpdateCreatorAsync(CreatorUpdateDto creator);

    /// <summary>
    /// Deletes an existing creator from the database asynchronously.
    /// </summary>
    /// <param name="creatorId">Id of the creator to delete.</param>
    /// <returns>True if the creator was deleted successfully or false if the creator was not found.</returns>
    public Task<bool> DeleteCreatorAsync(int creatorId);
}
