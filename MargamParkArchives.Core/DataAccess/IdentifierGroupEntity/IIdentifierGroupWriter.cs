namespace MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;

/// <summary>
/// Defines methods for creating, updating and deleting identifer group items in the database.
/// </summary>
public interface IIdentifierGroupWriter
{
    /// <summary>
    /// Creates a new identifier group item in the database asynchronously.
    /// </summary>
    /// <param name="identifierGroup">Object containing the values for the new identifier group item.</param>
    /// <returns>The id of the newly created identifier group item</returns>
    public Task<string> CreateIdentifierGroupAsync(IdentifierGroupCreateDto identifierGroup);

    /// <summary>
    /// Updates an existing identifier group item in the database asynchronously.
    /// </summary>
    /// <param name="identifierGroup">Object containing the updated values for the identifier group.</param>
    /// <returns>True if the identifier group was updated successfully or false if the identifier group was not found.</returns>
    public Task<bool> UpdateIdentifierGroupAsync(IdentifierGroupUpdateDto identifierGroup);
}
