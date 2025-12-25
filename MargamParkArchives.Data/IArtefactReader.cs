using MargamParkArchives.Data.Entities;

namespace MargamParkArchives.Data;

public interface IArtefactReader
{
    Artefact[] GetRandomArtefacts(int numArtefacts = 3);
}
