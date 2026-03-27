using MargamParkArchives.Core.DataAccess.GeneralLocationEntity;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Data.Connections;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data.Entities.GeneralLocationEntity;

public class MySqlGeneralLocationReader(IMySqlDataAccess dataAccess) : IGeneralLocationReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllGeneralLocationsQuery = "select * from {0};";
    private const string GetOneGeneralLocationQuery = "select * from {0} where general_location_id = @GeneralLocationId;";
    private const string CheckGeneralLocationExistsQuery = "select exists(select 1 from {0} where general_location_id = @GeneralLocationId);";
    private const string InvalidIdErrorMessage = "Id cannot be less than 0.";

    public async Task<GeneralLocation[]> GetAllGeneralLocationsAsync()
    {
        string sqlQuery = string.Format(GetAllGeneralLocationsQuery, GeneralLocationTableName);
        IEnumerable<GeneralLocationDto> generalLocationDtos = await _dataAccess.GetManyItemsAsync<GeneralLocationDto>(sqlQuery);
        return generalLocationDtos.Select(dto => dto.ToGeneralLocation()).ToArray();
    }

    public async Task<GeneralLocation?> GetOneGeneralLocationAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be less than 0.");
        }

        string sqlQuery = string.Format(GetOneGeneralLocationQuery, GeneralLocationTableName);
        GeneralLocationDto? generalLocationDto = await _dataAccess.GetOneItemAsync<GeneralLocationDto?, object>(sqlQuery, new { GeneralLocationId = id });
        return generalLocationDto?.ToGeneralLocation();
    }

    public async Task<bool> GeneralLocationExists(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException(InvalidIdErrorMessage, nameof(id));
        }

        string sqlQuery = string.Format(CheckGeneralLocationExistsQuery, DatabaseConstants.GeneralLocationTableName);
        return await _dataAccess.ExistsAsync<object>(sqlQuery, new { GeneralLocationId = id });
    }
}
