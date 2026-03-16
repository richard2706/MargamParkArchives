namespace MargamParkArchives.Core.DataAccess.GeneralLocationEntity;

public interface IGeneralLocationWriter
{
    /// <summary>
    /// Creates a new general location item in the database asynchronously.
    /// </summary>
    /// <param name="generalLocation">Object containing the values for the new general location item.</param>
    /// <returns>The id of the newly created general location item</returns>
    public Task<int> CreateGeneralLocationAsync(GeneralLocationCreateDto generalLocation);

    /// <summary>
    /// Updates an existing general location item in the database asynchronously.
    /// </summary>
    /// <param name="generalLocation">Object containing the updated values for the general location.</param>
    /// <returns>True if the general location was updated successfully or false if the general location was not found.</returns>
    public Task<bool> UpdateGeneralLocationAsync(GeneralLocationUpdateDto generalLocation);
}
