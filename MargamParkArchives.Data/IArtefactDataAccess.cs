using MargamParkArchives.Data.Entities;

namespace MargamParkArchives.Data;

public interface IArtefactDataAccess
{
    Artefact[] GetArtefactList(string query);
}
