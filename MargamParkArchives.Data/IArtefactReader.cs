using MargamParkArchives.Data.Entities;

namespace MargamParkArchives.Data;

public interface IArtefactReader
{
    public Task<Artefact[]> GetRandomArtefacts(int numArtefacts = 3);
}
