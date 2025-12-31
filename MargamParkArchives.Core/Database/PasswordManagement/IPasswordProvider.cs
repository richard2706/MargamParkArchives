namespace MargamParkArchives.Core.Database.PasswordManagement;

public interface IPasswordProvider
{
    public Task<string> GetPasswordAsync();
}
