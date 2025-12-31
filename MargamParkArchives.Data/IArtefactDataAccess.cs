using MargamParkArchives.Data.Entities;

namespace MargamParkArchives.Data;

public interface IArtefactDataAccess
{
    public Task<Artefact[]> GetArtefactListAsync(string query);
}
