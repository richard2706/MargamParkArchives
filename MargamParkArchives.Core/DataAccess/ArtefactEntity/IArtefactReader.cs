using MargamParkArchives.Core.Entities.ArtefactEntity;
using System.Text;

namespace MargamParkArchives.Core.DataAccess.ArtefactEntity;

public interface IArtefactReader
{
    public Task<Artefact?> GetOneArtefactAsync(string identiferGroupId, int identifierNumber);

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
