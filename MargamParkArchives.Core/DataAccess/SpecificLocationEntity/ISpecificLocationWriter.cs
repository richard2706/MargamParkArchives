namespace MargamParkArchives.Core.DataAccess.SpecificLocationEntity;

public interface ISpecificLocationWriter
{
    /// <summary>
    /// Creates a new specific location item in the database asynchronously.
    /// </summary>
    /// <param name="specificLocation">Object containing the values for the new specific location item.</param>
    /// <returns>The id of the newly created specific location item</returns>
    public Task<int> CreateSpecificLocationAsync(SpecificLocationCreateDto specificLocation);

    /// <summary>
    /// Updates an existing specific location item in the database asynchronously.
    /// </summary>
    /// <param name="specificLocation">Object containing the updated values for the specific location.</param>
    /// <returns>True if the specific location was updated successfully or false if the specific location was not found.</returns>
    public Task<bool> UpdateSpecificLocationAsync(SpecificLocationUpdateDto specificLocation);

    /// <summary>
    /// Deletes a specific location item from the database asynchronously.
    /// </summary>
    /// <param name="specificLocationId">Id of the specific location item to delete.</param>
    /// <returns>True if the specific location item was deleted successfully or false if the specific location item was not found.</returns>
    public Task<bool> DeleteSpecificLocationAsync(int specificLocationId);
}
