namespace MargamParkArchives.Data.Connections;

public interface IMySqlDataAccess
{
    // Read methods
    public Task<IEnumerable<T>> GetManyItemsAsync<T>(string sqlQuery);
    public Task<IEnumerable<T>> GetManyItemsAsync<T, P>(string sqlQuery, P parameters);
    public Task<T?> GetOneItemAsync<T>(string sqlQuery);
    public Task<T?> GetOneItemAsync<T, P>(string sqlQuery, P parameters);

    // Write methods
    public Task<bool> InsertAsync<P>(string sqlQuery, P parameters); // For records with user-specified IDs
}
