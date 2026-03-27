using MargamParkArchives.Core.Entities.SpecificLocationEntity;

namespace MargamParkArchives.Core.DataAccess.SpecificLocationEntity;

public interface ISpecificLocationReader
{
    /// <summary>
    /// Returns an array of all specific locations in the database. The array will be empty if the database contains no specific locations.
    /// </summary>
    /// <returns>An array of all specific locations in the database. The array will be empty if the database contains no specific locations.</returns>
    public Task<SpecificLocation[]> GetAllSpecificLocationsAsync();

    /// <summary>
    /// Returns one specific location from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the specific location.</param>
    /// <returns>The specific location identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is less than 0 it is invalid.</exception>
    public Task<SpecificLocation?> GetOneSpecificLocationAsync(int id);

    /// <summary>
    /// Returns true if a specific location with the provided id exists in the database.
    /// </summary>
    /// <remarks>Specific location ids are auto generated so will always be 0 or greater</remarks>
    /// <param name="id">Id of the specific location to check</param>
    /// <returns>True if the specific location identified by the provided id exists</returns>
    public Task<bool> SpecificLocationExists(int id);
}
