using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.GeneralLocationEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.GeneralLocationEntity;
using static MargamParkArchives.Data.DatabaseConstants;

namespace MargamParkArchives.Data;

public class MySqlGeneralLocationReader(IMySqlDataAccess dataAccess) : IGeneralLocationReader
{
    private readonly IMySqlDataAccess _dataAccess = dataAccess;

    private const string GetAllGeneralLocationsQuery = "select * from {0};";

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
    /// Returns one general locations from the database specified by its id, or null if it doesn't exist.
    /// </summary>
    /// <param name="id">Id that uniquely identifies the general location.</param>
    /// <returns>The general locations identified by the given id, or null if it doesn't exist.</returns>
    /// <exception cref="ArgumentException">If the id is less than 0 it is invalid.</exception>
    public async Task<GeneralLocation?> GetOneGeneralLocationAsync(int id)
    {
        throw new NotImplementedException();
    }
}
