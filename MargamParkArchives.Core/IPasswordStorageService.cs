namespace MargamParkArchives.Core;

public interface IPasswordStorageService
{
    public Task StorePasswordAsync(string password);
}
