namespace MargamParkArchives.Core.Database.PasswordManagement;

public class DirectPasswordProvider(string password) : IPasswordProvider
{
    private readonly string _password = password;

    public async Task<string> GetPassword()
    {
        return _password;
    }
}
