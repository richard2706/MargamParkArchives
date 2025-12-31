namespace MargamParkArchives.Core.Database.PasswordManagement;

public interface IDatabasePasswordValidationService
{
    public Task<PasswordValidationResponse> ValidatePasswordAsync(string password);
}
