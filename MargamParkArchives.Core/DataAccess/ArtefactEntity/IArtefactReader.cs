using MargamParkArchives.Core.Entities.ArtefactEntity;

namespace MargamParkArchives.Core.DataAccess.ArtefactEntity;

public interface IArtefactReader
{
    public Task<Artefact?> GetOneArtefactAsync(string identiferGroupId, int identifierNumber);
}
