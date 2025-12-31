using MargamParkArchives.Core.Database.PasswordManagement;
using System.Security.Cryptography;
using System.Text;

namespace MargamParkArchives.Windows;

public class DatabasePasswordProvider(IPasswordFilePathProvider filePathProvider) : IPasswordProvider
{
    private readonly IPasswordFilePathProvider _filePathProvider = filePathProvider;

    public async Task<string> GetPasswordAsync()
    {
        string _passwordFilePath = _filePathProvider.GetPasswordFilePath();
        if (!File.Exists(_passwordFilePath))
        {
            throw new PasswordFileMissingException($"Password file not found at {_passwordFilePath}");
        }

        // Read encrypted password from file
        using FileStream passwordFileStream = new(_passwordFilePath, FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, useAsync: true);
        byte[] fileBuffer = new byte[passwordFileStream.Length];
        int numBytesRead = await passwordFileStream.ReadAsync(fileBuffer);
        if (numBytesRead != passwordFileStream.Length)
        {
            throw new IOException("There was a problem accessing the database password.");
        }

        // Decrypt password
        byte[] decryptedbytes = ProtectedData.Unprotect(fileBuffer, null, DataProtectionScope.LocalMachine);
        string decryptedPassword = UnicodeEncoding.UTF8.GetString(decryptedbytes);
        return decryptedPassword;
    }
}
