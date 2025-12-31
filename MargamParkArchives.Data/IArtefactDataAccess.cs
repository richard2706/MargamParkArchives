using MargamParkArchives.Data.Entities;

namespace MargamParkArchives.Data;

public interface IArtefactDataAccess
{
    public Task<Artefact[]> GetArtefactList(string query);
}
