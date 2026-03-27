using MargamParkArchives.Core.Entities.CreatorEntity;

namespace MargamParkArchives.Core.DataAccess.CreatorEntity;

public interface ICreatorReader
{
    /// <summary>
    /// Returns an array of all creators in the database which will be empty if the database contains no creators.
    /// </summary>
    /// <returns>An array of all creators in the database which will be empty if the database contains no creators.</returns>
    public Task<Creator[]> GetAllCreatorsAsync();

    /// <summary>
    /// Returns one creator from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the creator.</param>
    /// <returns>The creator identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is less than 0 it is invalid.</exception>
    public Task<Creator?> GetOneCreatorAsync(int id);

    /// <summary>
    /// Returns true if a creator with the provided id exists in the database.
    /// </summary>
    /// <remarks>Creator ids are auto generated so will always be 0 or greater</remarks>
    /// <param name="id">Id of the creator to check</param>
    /// <returns>True if the creator identified by the provided id exists</returns>
    public Task<bool> CreatorExists(int id);
}
