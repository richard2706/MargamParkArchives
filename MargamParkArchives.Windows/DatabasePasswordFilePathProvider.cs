using MargamParkArchives.Core.Database.PasswordManagement;

namespace MargamParkArchives.Windows;

public class DatabasePasswordFilePathProvider(string fileName) : IPasswordFilePathProvider
{
    private readonly string _fileName = fileName;

    public string GetPasswordFilePath()
    {
        string passwordFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        return Path.Combine(passwordFileLocation, _fileName);
    }
}
