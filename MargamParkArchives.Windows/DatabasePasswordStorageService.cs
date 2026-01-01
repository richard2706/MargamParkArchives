using MargamParkArchives.Core.Database.PasswordManagement;
using System.Security.Cryptography;
using System.Text;

namespace MargamParkArchives.Windows;

public class DatabasePasswordStorageService(IPasswordFilePathProvider filePathProvider) : IPasswordStorageService
{
    private readonly IPasswordFilePathProvider _filePathProvider = filePathProvider;

    public async Task SavePasswordAsync(string password)
    {
        // Encrypt password
        byte[] passwordAsBytes = UnicodeEncoding.ASCII.GetBytes(password);
        byte[] encryptedPassword = ProtectedData.Protect(passwordAsBytes, null, DataProtectionScope.LocalMachine)
            ?? throw new CryptographicException("The password was not encrypted. The encrypted value is null");

        // Store encrypted password
        string filePath = _filePathProvider.GetPasswordFilePath();
        using FileStream passwordWriterStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
        await passwordWriterStream.WriteAsync(encryptedPassword.AsMemory());
    }
}
