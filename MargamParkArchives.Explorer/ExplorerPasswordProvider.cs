using MargamParkArchives.Core;
using System;
using System.IO;

namespace MargamParkArchives.Explorer;

public class ExplorerPasswordProvider : IPasswordProvider
{
    private const string PasswordFileName = "explorer_db_password";
    private readonly string _passwordFilePath;

    public ExplorerPasswordProvider()
    {
        string passwordFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        _passwordFilePath = Path.Combine(passwordFileLocation, PasswordFileName);
    }

    public string GetPassword()
    {
        if (!File.Exists(_passwordFilePath))
        {
            throw new PasswordMissingException($"Password file not found at {_passwordFilePath}");
        }

        return "";
    }
}
