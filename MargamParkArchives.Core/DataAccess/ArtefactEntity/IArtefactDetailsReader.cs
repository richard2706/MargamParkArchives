using MargamParkArchives.Core.Entities.ArtefactDetails;

namespace MargamParkArchives.Core.DataAccess.ArtefactEntity;

public interface IArtefactDetailsReader
{
    public Task<ArtefactDetails[]> GetRandomArtefactsAsync(int numArtefacts = 3);
    public Task<ArtefactDetails?> GetOneArtefactAsync(string identiferGroupId, int identifierNumber);
}
