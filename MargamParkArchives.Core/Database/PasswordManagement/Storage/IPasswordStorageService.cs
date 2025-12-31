namespace MargamParkArchives.Core.Database.PasswordManagement.Storage;

public interface IPasswordStorageService
{
    public Task<PasswordStorageResponse> SavePasswordAsync(string password);
}
