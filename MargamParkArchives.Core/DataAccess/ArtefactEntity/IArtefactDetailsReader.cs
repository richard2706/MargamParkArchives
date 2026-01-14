using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;

namespace MargamParkArchives.Core.DataAccess.ArtefactEntity;

public interface IArtefactDetailsReader
{
    public Task<ArtefactDetailsReadModel[]> GetRandomArtefactsAsync(int numArtefacts = 3);
    public Task<ArtefactDetailsReadModel?> GetOneArtefactAsync(string identiferGroupId, int identifierNumber);
}
