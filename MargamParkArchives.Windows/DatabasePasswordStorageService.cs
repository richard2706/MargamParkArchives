using MargamParkArchives.Core.Database.PasswordManagement;
using MargamParkArchives.Core.Database.PasswordManagement.Storage;
using System.Security.Cryptography;
using System.Text;

namespace MargamParkArchives.Windows;

public class DatabasePasswordStorageService(IPasswordFilePathProvider filePathProvider) : IPasswordStorageService
{
    private readonly IPasswordFilePathProvider _filePathProvider = filePathProvider;

    public async Task<PasswordStorageResponse> SavePasswordAsync(string password)
    {
        // Encrypt password
        byte[] passwordAsBytes = UnicodeEncoding.ASCII.GetBytes(password);
        byte[] entropy = CreateRandomEntropy();
        byte[] encryptedPassword;
        try
        {
            encryptedPassword = ProtectedData.Protect(
            passwordAsBytes,
            entropy,
            DataProtectionScope.LocalMachine);
        }
        catch (Exception ex)
        {
            return new PasswordStorageResponse(PasswordStorageResult.EncryptionFailed, ex);
        }
        if (encryptedPassword == null)
        {
            return new PasswordStorageResponse(PasswordStorageResult.EncryptionFailed, null);
        }

        // Store encrypted password
        string filePath = _filePathProvider.GetPasswordFilePath();
        using FileStream passwordWriterStream = new(filePath, FileMode.Create);
        try
        {
            await passwordWriterStream.WriteAsync(encryptedPassword.AsMemory());
            return new PasswordStorageResponse(PasswordStorageResult.Success, null);
        }
        catch (Exception ex)
        {
            return new PasswordStorageResponse(PasswordStorageResult.FileWriterError, ex);
        }
    }

    private static byte[] CreateRandomEntropy()
    {
        byte[] entropy = new byte[16];
        RandomNumberGenerator.Create().GetBytes(entropy);
        return entropy;
    }
}
