using MargamParkArchives.Core.Entities.GeneralLocationEntity;

namespace MargamParkArchives.Core.DataAccess.GeneralLocationEntity;

/// <summary>
/// Provides methods for reading general location entities from the database. Get and exists methods.
/// </summary>
public interface IGeneralLocationReader
{
    /// <summary>
    /// Returns an array of all general locations in the database. The array will be empty if the database contains no general locations.
    /// </summary>
    /// <returns>An array of all general locations in the database. The array will be empty if the database contains no general locations.</returns>
    public Task<GeneralLocation[]> GetAllGeneralLocationsAsync();

    /// <summary>
    /// Returns one general location from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the general location.</param>
    /// <returns>The general location identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is less than 0 it is invalid.</exception>
    public Task<GeneralLocation?> GetOneGeneralLocationAsync(int id);

    /// <summary>
    /// Returns true if a general location with the provided id exists in the database.
    /// </summary>
    /// <remarks>General location ids are auto generated so will always be 0 or greater</remarks>
    /// <param name="id">Id of the general location to check</param>
    /// <returns>True if the general location identified by the provided id exists</returns>
    public Task<bool> GeneralLocationExists(int id);
}
