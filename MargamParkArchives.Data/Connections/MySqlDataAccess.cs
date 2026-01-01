using Dapper;
using MargamParkArchives.Core.Database;
using MySqlConnector;

namespace MargamParkArchives.Data.Connections;

public class MySqlDataAccess(IConnectionStringProvider connectionStringProvider) : IMySqlDataAccess
{
    private readonly IConnectionStringProvider _connectionStringProvider = connectionStringProvider;

    /// <summary>
    /// Gets a collection of items of the specified type T from the database asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the items to be returned which should correspond to the database
    /// entity.</typeparam>
    /// <typeparam name="P">The type of the parameters object used to supply values for the SQL query.</typeparam>
    /// <param name="sqlQuery">The SQL select query to execute. The entity being selected must correspond to the type
    /// T of items being returned.</param>
    /// <param name="parameters">An object containing the parameter values to be used with the SQL query. The
    /// structure should match the parameters in the query.</param>
    /// <returns>A task that represents the asynchronous operation, which will return a collection of items of type T
    /// returned by the query.</returns>
    public async Task<IEnumerable<T>> GetManyItemsAsync<T, P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        IEnumerable<T> items = await connection.QueryAsync<T>(sqlQuery, parameters);
        return items;
    }

    /// <summary>
    /// Gets a single item of the specified type T from the database asynchronously. Will return the first item if the
    /// query returns multiple items.
    /// </summary>
    /// <typeparam name="T">The type of the item to be returned which should correspond to the database
    /// entity.</typeparam>
    /// <typeparam name="P">The type of the parameters object used to supply values for the SQL query.</typeparam>
    /// <param name="sqlQuery">The SQL select query to execute. The entity being selected must correspond to the type
    /// T of items being returned.</param>
    /// <param name="parameters">An object containing the parameter values to be used with the SQL query. The
    /// structure should match the parameters in the query.</param>
    /// <returns>A task that represents the asynchronous operation, which will return an item of type T
    /// returned by the query, or the first item if the query returns multiple items.</returns>
    public async Task<T?> GetOneItemAsync<T, P>(string sqlQuery, P parameters)
    {
        string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
        using MySqlConnection connection = new(connectionString);
        T item = await connection.QueryFirstAsync<T>(sqlQuery, parameters);
        return item;
    }
}
