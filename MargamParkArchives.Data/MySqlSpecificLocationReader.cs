using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.SpecificLocationEntity;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlSpecificLocationReader(IMySqlDataAccess dataAccess) : ISpecificLocationReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllSpecificLocationsQuery = "select * from {0};";

    public async Task<SpecificLocation[]> GetAllSpecificLocationsAsync()
    {
        string sqlQuery = string.Format(GetAllSpecificLocationsQuery, SpecificLocationTableName);
        IEnumerable<SpecificLocationDto> specificLocationDtos = await _dataAccess.GetManyItemsAsync<SpecificLocationDto>(sqlQuery);
        return specificLocationDtos.Select(dto => dto.ToSpecificLocation()).ToArray();
    }

    public Task<SpecificLocation?> GetOneSpecificLocationAsync(int id)
    {
        throw new NotImplementedException();
    }
}
