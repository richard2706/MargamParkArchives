using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.Artefact;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlArtefactDetailsReader(IMySqlDataAccess dataAccess) : IArtefactDetailsReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetRandomArtefactsQuery = "select * from @Table order by rand() limit @Limit;";

    public async Task<ArtefactDetailsReadModel[]> GetRandomArtefactsAsync(int numArtefacts = 3)
    {
        IEnumerable<ArtefactDetailsDto> artefacts = await _dataAccess.GetManyItemsAsync<ArtefactDetailsDto, dynamic>(
            GetRandomArtefactsQuery,
            new { Table = ArtefactDetailsViewName, Limit = numArtefacts });
        return artefacts.Select(dto => dto.ToArtefactDetailsReadModel()).ToArray();
    }
}
