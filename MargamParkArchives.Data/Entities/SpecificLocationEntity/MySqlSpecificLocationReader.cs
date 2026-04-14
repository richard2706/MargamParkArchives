using MargamParkArchives.Core.DataAccess.SpecificLocationEntity;
using MargamParkArchives.Core.Entities.SpecificLocationEntity;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.SpecificLocationEntity;

public class MySqlSpecificLocationReader(IMySqlDataAccess dataAccess) : ISpecificLocationReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllSpecificLocationsQuery = "select * from {0};";
    private const string GetOneSpecificLocationQuery = "select * from {0} where specific_location_id = @SpecificLocationId;";
    private const string CheckSpecificLocationExistsQuery = "select exists(select 1 from {0} where specific_location_id = @SpecificLocationId);";

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
            throw new ArgumentOutOfRangeException(nameof(id), InvalidIdErrorMessage);
        }

        string sqlQuery = string.Format(GetOneSpecificLocationQuery, SpecificLocationTableName);
        SpecificLocationDto? specificLocationDto = await _dataAccess.GetOneItemAsync<SpecificLocationDto?, object>(sqlQuery, new { SpecificLocationId = id });
        return specificLocationDto?.ToSpecificLocation();
    }

    public async Task<bool> SpecificLocationExists(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException(InvalidIdErrorMessage, nameof(id));
        }

        string sqlQuery = string.Format(CheckSpecificLocationExistsQuery, DatabaseConstants.SpecificLocationTableName);
        return await _dataAccess.ExistsAsync<object>(sqlQuery, new { SpecificLocationId = id });
    }
}
