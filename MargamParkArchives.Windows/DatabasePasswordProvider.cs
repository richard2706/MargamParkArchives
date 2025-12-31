using MargamParkArchives.Core.Database.PasswordManagement;

namespace MargamParkArchives.Windows;

public class DatabasePasswordProvider(IPasswordFilePathProvider filePathProvider) : IPasswordProvider
{
    private readonly IPasswordFilePathProvider _filePathProvider = filePathProvider;

    public string GetPassword()
    {
        string _passwordFilePath = _filePathProvider.GetPasswordFilePath();
        if (!File.Exists(_passwordFilePath))
        {
            throw new PasswordFileMissingException($"Password file not found at {_passwordFilePath}");
        }

        return "";
    }
}
