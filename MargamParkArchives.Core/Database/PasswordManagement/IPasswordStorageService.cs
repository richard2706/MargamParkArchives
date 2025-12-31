namespace MargamParkArchives.Core.Database.PasswordManagement;

public interface IPasswordStorageService
{
    public Task StorePasswordAsync(string password);
}
