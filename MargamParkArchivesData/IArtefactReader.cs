using MargamParkArchivesData.Entities;

namespace MargamParkArchivesData;

public interface IArtefactReader
{
    Artefact[] GetRandomArtefacts(int numArtefacts = 3);
}
