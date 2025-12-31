using MargamParkArchives.Data.Entities;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlArtefactReader(IArtefactDataAccess dataAccess) : IArtefactReader
{
    private readonly IArtefactDataAccess _dataAccess = dataAccess;

    private const string _getRandomArtefactsQuery = "select * from {0} order by rand() limit {1};";

    public async Task<Artefact[]> GetRandomArtefacts(int numArtefacts = 3)
    {
        string query = string.Format(_getRandomArtefactsQuery, ArtefactDetailsViewName, numArtefacts);
        return await _dataAccess.GetArtefactList(query);
    }
}
