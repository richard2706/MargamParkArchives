using Dapper;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Database.PasswordManagement;
using MySqlConnector;

namespace MargamParkArchives.Data.Connections;

public class MySqlDataAccess(IConnectionStringProvider connectionStringProvider) : IMySqlDataAccess
{
    private const string InvalidAuthSqlState = "28000";

    private readonly IConnectionStringProvider _connectionStringProvider = connectionStringProvider;

    #region Read methods

    public async Task<IEnumerable<T>> GetManyItemsAsync<T>(string sqlQuery)
    {
        return await GetManyItemsAsync<T, object>(sqlQuery, new { });
    }

    public async Task<IEnumerable<T>> GetManyItemsAsync<T, P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        IEnumerable<T> items;
        try
        {
            items = await connection.QueryAsync<T>(sqlQuery, parameters);
        }
        catch (MySqlException ex)
        {
            throw ex.SqlState == InvalidAuthSqlState ? new DatabasePasswordInvalidException(ex.Message) : ex;
        }
        return items;
    }

    public async Task<T?> GetOneItemAsync<T>(string sqlQuery)
    {
        return await GetOneItemAsync<T, object>(sqlQuery, new { });
    }

    public async Task<T?> GetOneItemAsync<T, P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        T? item;
        try
        {
            item = await connection.QueryFirstOrDefaultAsync<T>(sqlQuery, parameters);
        }
        catch (MySqlException ex)
        {
            throw ex.SqlState == InvalidAuthSqlState ? new DatabasePasswordInvalidException(ex.Message) : ex;
        }
        return item;
    }

    public async Task<bool> ExistsAsync(string sqlQuery)
    {
        return await ExistsAsync(sqlQuery, new { });
    }

    public async Task<bool> ExistsAsync<P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        bool recordExists;
        try
        {
            recordExists = await connection.ExecuteScalarAsync<bool>(sqlQuery, parameters);
        }
        catch (MySqlException ex)
        {
            throw ex.SqlState == InvalidAuthSqlState ? new DatabasePasswordInvalidException(ex.Message) : ex;
        }
        return recordExists;
    }

    public async Task<T?> GetSingleValueAsync<T>(string sqlQuery)
    {
        return await GetSingleValueAsync<T, object>(sqlQuery, new { });
    }

    public async Task<T?> GetSingleValueAsync<T, P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        T? value;
        try
        {
            value = await connection.ExecuteScalarAsync<T>(sqlQuery, parameters);
        }
        catch (MySqlException ex)
        {
            throw ex.SqlState == InvalidAuthSqlState ? new DatabasePasswordInvalidException(ex.Message) : ex;
        }
        return value;
    }

    #endregion Read methods

    #region Write methods

    public async Task<bool> InsertAsync<P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        int rowsAffected = 0;
        try
        {
            rowsAffected = await connection.ExecuteAsync(sqlQuery, parameters);
        }
        catch (MySqlException ex)
        {
            throw ex.SqlState == InvalidAuthSqlState ? new DatabasePasswordInvalidException(ex.Message) : ex;
        }
        return rowsAffected > 0;
    }

    public async Task<int> InsertAndReturnIdAsync<P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        int rowsAffected = 0;
        try
        {
            rowsAffected = await connection.ExecuteAsync(sqlQuery, parameters);

        }
        catch (MySqlException ex)
        {
            throw ex.SqlState == InvalidAuthSqlState ? new DatabasePasswordInvalidException(ex.Message) : ex;
        }

        if (rowsAffected == 0)
        {
            throw new DatabaseException("Insert operation failed.");
        }

        return connection.ExecuteScalar<int>("SELECT LAST_INSERT_ID();");
    }

    public async Task<bool> UpdateAsync<P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        int rowsAffected = 0;
        try
        {
            rowsAffected = await connection.ExecuteAsync(sqlQuery, parameters);
        }
        catch (MySqlException ex)
        {
            throw ex.SqlState == InvalidAuthSqlState ? new DatabasePasswordInvalidException(ex.Message) : ex;
        }
        return rowsAffected > 0;
    }

    #endregion Write methods
}
