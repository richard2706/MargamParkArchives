using System.Text;

namespace MargamParkArchives.Core.DataAccess.ArtefactEntity;

public interface IArtefactWriter
{
    /// <summary>
    /// Creates a new aretefact in the database.
    /// </summary>
    /// <remarks>Related entities are all optional (except identifier group), and should be inserted separately. Future task may include
    /// overloads of this method to create linked entities in one database transaction.</remarks>
    /// <param name="artefact">Object containing values for the new artefact</param>
    /// <returns>Returns the identifier key of the new artefact</returns>
    public Task<string> CreateArtefactAsync(ArtefactCreateDto artefact);

    /// <summary>
    /// Updates an existing artefact in the database.
    /// </summary>
    /// <param name="artefact">Object containing the values for the updated artefact</param>
    /// <returns>True if the artefact was updated successfully or false if the artefact was not found</returns>
    public Task<bool> UpdateArtefactAsync(ArtefactUpdateDto artefact);

    /// <summary>
    /// Gets the identifier key of the most recently inserted artefact in the specificed identifier group or null if it is not found.
    /// </summary>
    /// <remarks>Useful for getting the identifier key of a newly inserted artefact.</remarks>
    /// <param name="identifierGroupId"></param>
    /// <returns>Identifier key of the most recently inserted artefact in the specified identifier group or null if it is not found</returns>
    public Task<string?> GetLastIdentifierKeyForGroupAsync(string identifierGroupId);

    /// <summary>
    /// Returns true if an artefact exists with the specified identifier key.
    /// </summary>
    /// <param name="identifierKey">Identifier key of the artefact</param>
    /// <returns>True if an artefact exists with the specified identifier key.</returns>
    public Task<bool> ArtefactExistsAsync(string identifierKey);

    /// <summary>
    /// Returns true if an artefact exists with the specified identifier group id and identifier number (equivalent to
    /// the identifier key).
    /// </summary>
    /// <param name="identifierKey">Identifier key of the artefact</param>
    /// <returns>True if an artefact exists with the specified identifier group id and identifier number.</returns>
    public Task<bool> ArtefactExistsAsync(string identifierGroupId, int identifierNumber, StringBuilder? errorList = null);
}
