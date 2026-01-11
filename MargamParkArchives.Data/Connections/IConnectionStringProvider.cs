namespace MargamParkArchives.Data.Connections;

public interface IConnectionStringProvider
{
    public Task<string> GetConnectionStringAsync();
}
