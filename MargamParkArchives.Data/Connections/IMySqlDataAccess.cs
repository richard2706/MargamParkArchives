namespace MargamParkArchives.Data.Connections;

public interface IMySqlDataAccess
{
    public Task<IEnumerable<T>> GetManyItemsAsync<T, P>(string sqlQuery, P parameters);

    public Task<T?> GetOneItemAsync<T, P>(string sqlQuery, P parameters);

    // TODO add methods for insert one, insert many, update one, delete
}
