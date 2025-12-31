using MargamParkArchives.Core.Database.PasswordManagement;

namespace MargamParkArchives.Windows;

public class DatabasePasswordFilePathProvider(string fileName, string applicationName) : IPasswordFilePathProvider
{
    public string GetPasswordFilePath()
    {
        string programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        string applicationDirectoryPath = Path.Combine(programDataPath, applicationName);
        Directory.CreateDirectory(applicationDirectoryPath);
        return Path.Combine(applicationDirectoryPath, fileName);
    }
}
