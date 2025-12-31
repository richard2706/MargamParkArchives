namespace MargamParkArchives.Core.Database.PasswordManagement;

public interface IPasswordStorageService
{
    public Task SavePasswordAsync(string password);
}
