namespace MargamParkArchives.Core.Database
{
    public interface IConnectionStringProvider
    {
        public Task<string> GetConnectionString();
    }
}
