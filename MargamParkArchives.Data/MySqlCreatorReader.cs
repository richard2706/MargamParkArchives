using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CreatorEntity;

namespace MargamParkArchives.Data;

public class MySqlCreatorReader(IMySqlDataAccess dataAccess) : ICreatorReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllCreatorsQuery = "select * from creator;";

    public async Task<Creator[]> GetAllCreatorsAsync()
    {
        IEnumerable<CreatorDto> creatorDtos = await _dataAccess.GetManyItemsAsync<CreatorDto>(GetAllCreatorsQuery);
        return creatorDtos.Select(dto => dto.ToCreator()).ToArray();
    }
}
