using MargamParkArchives.Core.Entities.IdentifierGroupEntity;

namespace MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;

public interface IIdentifierGroupReader
{
    /// <summary>
    /// Returns an array of all identifier groups in the database. The array will be empty if the database contains no creators.
    /// </summary>
    /// <returns>An array of all identifier groups in the database. The array will be empty if the database contains no creators.</returns>
    public Task<IdentifierGroup[]> GetAllIdentifierGroupsAsync();

    /// <summary>
    /// Returns one identifier group from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the identifier group.</param>
    /// <returns>The identifier group identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is an empty string then it is invalid.</exception>
    public Task<IdentifierGroup?> GetOneIdentifierGroupAsync(string id);

    /// <summary>
    /// Returns true if an identifier group with the provided id exists in the database.
    /// </summary>
    /// <param name="id">Id of the identifier group to check</param>
    /// <returns>True if the identifer group identified by the provided id exists</returns>
    public Task<bool> IdentifierGroupExists(string id);
}
