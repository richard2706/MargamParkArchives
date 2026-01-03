using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;

namespace MargamParkArchives.Core.Database.DataAccess;

public interface IArtefactDetailsReader
{
    public Task<ArtefactDetailsReadModel[]> GetRandomArtefactsAsync(int numArtefacts = 3);
}
