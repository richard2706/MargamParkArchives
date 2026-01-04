using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.GeneralLocationEntity;
using MargamParkArchives.Data.Entities.SpecificLocationEntity;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlSpecificLocationReader(IMySqlDataAccess dataAccess) : ISpecificLocationReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllSpecificLocationsQuery = "select * from {0};";
    private const string GetOneSpecificLocationQuery = "select * from {0} where specific_location_id = @SpecificLocationId;";

    public async Task<SpecificLocation[]> GetAllSpecificLocationsAsync()
    {
        string sqlQuery = string.Format(GetAllSpecificLocationsQuery, SpecificLocationTableName);
        IEnumerable<SpecificLocationDto> specificLocationDtos = await _dataAccess.GetManyItemsAsync<SpecificLocationDto>(sqlQuery);
        return specificLocationDtos.Select(dto => dto.ToSpecificLocation()).ToArray();
    }

    public async Task<SpecificLocation?> GetOneSpecificLocationAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be less than 0.");
        }

        string sqlQuery = string.Format(GetOneSpecificLocationQuery, SpecificLocationTableName);
        SpecificLocationDto? specificLocationDto = await _dataAccess.GetOneItemAsync<SpecificLocationDto?, object>(sqlQuery, new { SpecificLocationId = id });
        return specificLocationDto?.ToSpecificLocation();
    }
}
