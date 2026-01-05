using MargamParkArchives.Core.Entities.ArtefactEntity;

namespace MargamParkArchives.Core.Database.DataAccess;

public interface IArtefactReader
{
    public Task<Artefact?> GetOneArtefactAsync(string identiferGroupId, int identifierNumber);
}
