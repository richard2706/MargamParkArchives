namespace MargamParkArchives.Core.Database.PasswordManagement;

public class DatabasePasswordProvider(IPasswordFilePathProvider filePathProvider) : IPasswordProvider
{
    private readonly IPasswordFilePathProvider _filePathProvider = filePathProvider;

    public string GetPassword()
    {
        string _passwordFilePath = _filePathProvider.GetPasswordFilePath();
        if (!File.Exists(_passwordFilePath))
        {
            throw new PasswordMissingException($"Password file not found at {_passwordFilePath}");
        }

        return "";
    }
}
