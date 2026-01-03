using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.Artefact;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlArtefactDetailsReader(IMySqlDataAccess dataAccess) : IArtefactDetailsReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetRandomArtefactsQuery = "select * from {0} order by rand() limit @Limit;";

    public async Task<ArtefactDetailsReadModel[]> GetRandomArtefactsAsync(int numArtefacts = 3)
    {
        if (numArtefacts <= 0)
        {
            throw new ArgumentException("Number of artefacts must be greater than 0.");
        }

        string sqlQuery = string.Format(GetRandomArtefactsQuery, ArtefactDetailsViewName);
        IEnumerable<ArtefactDetailsDto> artefacts = await _dataAccess.GetManyItemsAsync<ArtefactDetailsDto, dynamic>(
            sqlQuery, new { Limit = numArtefacts });
        return artefacts.Select(dto => dto.ToArtefactDetailsReadModel()).ToArray();
    }
}
