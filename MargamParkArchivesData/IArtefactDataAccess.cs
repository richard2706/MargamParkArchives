using MargamParkArchivesData.Entities;

namespace MargamParkArchivesData;

public interface IArtefactDataAccess
{
    Artefact[] GetArtefactList(string query);
}
