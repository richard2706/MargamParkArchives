using MargamParkArchives.Data.Entities;

namespace MargamParkArchives.Data;

public interface IArtefactReader
{
    public Task<Artefact[]> GetRandomArtefactsAsync(int numArtefacts = 3);
}
