namespace MargamParkArchives.Data.Connections;

public interface IMySqlDataAccess
{
    #region Read methods

    /// <summary>
    /// Gets a collection of items of the specified type T from the database asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the items to be returned which should correspond to the database
    /// entity.</typeparam>
    /// <param name="sqlQuery">The SQL select query to execute. The entity being selected must correspond to the type
    /// T of items being returned.</param>
    /// <returns>A task that represents the asynchronous operation, which will return a collection of items of type T
    /// returned by the query.</returns>
    public Task<IEnumerable<T>> GetManyItemsAsync<T>(string sqlQuery);

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
    public Task<IEnumerable<T>> GetManyItemsAsync<T, P>(string sqlQuery, P parameters);

    /// <summary>
    /// Gets a single item of the specified type T from the database asynchronously. Will return the first item if the
    /// query returns multiple items or null if the item doesn't exist.
    /// </summary>
    /// <typeparam name="T">The type of the item to be returned which should correspond to the database
    /// entity.</typeparam>
    /// <param name="sqlQuery">The SQL select query to execute. The entity being selected must correspond to the type
    /// T of items being returned.</param>
    /// <returns>A task that represents the asynchronous operation, which will return an item of type T?
    /// returned by the query, or the first item if the query returns multiple items, or null if the item doesn't
    /// exist.</returns>
    public Task<T?> GetOneItemAsync<T>(string sqlQuery);

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
    public Task<T?> GetOneItemAsync<T, P>(string sqlQuery, P parameters);

    #endregion Read methods

    #region Write methods

    /// <summary>
    /// Inserts an item into the database asynchronously. Does not return the inserted item's ID. Useful for items
    /// where the id is not auto-generated.
    /// </summary>
    /// <typeparam name="P">The type of the parameters object used to supply values for the SQL query.</typeparam>
    /// <param name="sqlQuery">The SQL insert query to execute.</param>
    /// <param name="parameters">An object containing the parameter values to be used with the SQL query. The
    /// property names in the object should match the parameter names in the query.</param>
    /// <returns>True if the item was inserted sucessfully, false otherwise.</returns>
    public Task<bool> InsertAsync<P>(string sqlQuery, P parameters); // For records with user-specified IDs

    /// <summary>
    /// Inserts an item into the database asynchronously and return the inserted item's ID. Useful for items
    /// where the id is auto-generated.
    /// </summary>
    /// <typeparam name="P">The type of the parameters object used to supply values for the SQL query.</typeparam>
    /// <param name="sqlQuery">The SQL insert query to execute.</param>
    /// <param name="parameters">An object containing the parameter values to be used with the SQL query. The
    /// property names in the object should match the parameter names in the query.</param>
    /// <returns>The id of the newly inserted item.</returns>
    public Task<int> InsertAndReturnIdAsync<P>(string sqlQuery, P parameters);

    /// <summary>
    /// Updates an item in the database as specified by the sqlQuery and parameters.
    /// </summary>
    /// <typeparam name="P">The type of the parameters object used to supply values for the SQL query.</typeparam>
    /// <param name="sqlQuery">The SQL update query to execute.</param>
    /// <param name="parameters">An object containing the parameter values to be used with the SQL query. The
    /// property names in the object should match the parameter names in the query.</param>
    /// <returns>True if the item was updated sucessfully, false otherwise.</returns>
    public Task<bool> UpdateAsync<P>(string sqlQuery, P parameters);

    //public Task<bool> DeleteAsync<P>(string sqlQuery, P parameters);

    #endregion Write methods
}
