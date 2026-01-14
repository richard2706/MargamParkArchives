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

    /// <summary>
    /// Returns an array of all general locations in the database. The array will be empty if the database contains no general locations.
    /// </summary>
    /// <returns>An array of all general locations in the database. The array will be empty if the database contains no general locations.</returns>
    public async Task<GeneralLocation[]> GetAllGeneralLocationsAsync()
    {
        string sqlQuery = string.Format(GetAllGeneralLocationsQuery, GeneralLocationTableName);
        IEnumerable<GeneralLocationDto> generalLocationDtos = await _dataAccess.GetManyItemsAsync<GeneralLocationDto>(sqlQuery);
        return generalLocationDtos.Select(dto => dto.ToGeneralLocation()).ToArray();
    }

    /// <summary>
    /// Returns one general location from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the general location.</param>
    /// <returns>The general location identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is less than 0 it is invalid.</exception>
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
}
