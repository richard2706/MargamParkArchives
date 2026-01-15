using Dapper;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Database.PasswordManagement;
using MySqlConnector;

namespace MargamParkArchives.Data.Connections;

public class MySqlDataAccess(IConnectionStringProvider connectionStringProvider) : IMySqlDataAccess
{
    private const string InvalidAuthSqlState = "28000";

    private readonly IConnectionStringProvider _connectionStringProvider = connectionStringProvider;

    public async Task<IEnumerable<T>> GetManyItemsAsync<T>(string sqlQuery)
    {
        return await GetManyItemsAsync<T, object>(sqlQuery, new { });
    }

    /// <summary>
    /// Gets a collection of items of the specified type T from the database asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the items to be returned which should correspond to the database
    /// entity.</typeparam>
    /// <typeparam name="P">The type of the parameters object used to supply values for the SQL query.</typeparam>
    /// <param name="sqlQuery">The SQL select query to execute. The entity being selected must correspond to the type
    /// T of items being returned.</param>
    /// <param name="parameters">An object containing the parameter values to be used with the SQL query. The
    /// property names in the object should match the parameter names in the query.</param>
    /// <returns>A task that represents the asynchronous operation, which will return a collection of items of type T
    /// returned by the query.</returns>
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

    /// <summary>
    /// Gets a single item of the specified type T from the database asynchronously. Will return the first item if the
    /// query returns multiple items or null if the item doesn't exist.
    /// </summary>
    /// <typeparam name="T">The type of the item to be returned which should correspond to the database
    /// entity.</typeparam>
    /// <typeparam name="P">The type of the parameters object used to supply values for the SQL query.</typeparam>
    /// <param name="sqlQuery">The SQL select query to execute. The entity being selected must correspond to the type
    /// T of items being returned.</param>
    /// <param name="parameters">An object containing the parameter values to be used with the SQL query. The
    /// property names in the object should match the parameter names in the query.</param>
    /// <returns>A task that represents the asynchronous operation, which will return an item of type T?
    /// returned by the query, or the first item if the query returns multiple items, or null if the item doesn't
    /// exist.</returns>
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

    /// <summary>
    /// Inserts an item into the database asynchronously. Does not return the inserted item's ID. Useful for items
    /// where the id is not auto-generated.
    /// </summary>
    /// <typeparam name="P">The type of the parameters object used to supply values for the SQL query.</typeparam>
    /// <param name="sqlQuery">The SQL insert query to execute.</param>
    /// <param name="parameters">An object containing the parameter values to be used with the SQL query. The
    /// property names in the object should match the parameter names in the query.</param>
    /// <returns>True if the item was inserted sucessfully, false otherwise.</returns>
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
}
