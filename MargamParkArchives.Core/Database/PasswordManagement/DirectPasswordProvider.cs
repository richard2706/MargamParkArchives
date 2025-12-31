using MargamParkArchives.Core.Database.PasswordManagement;

namespace MargamParkArchives.Data.Connections;

public class DirectPasswordProvider(string password) : IPasswordProvider
{
    private readonly string _password = password;

    public string GetPassword()
    {
        return _password;
    }
}
