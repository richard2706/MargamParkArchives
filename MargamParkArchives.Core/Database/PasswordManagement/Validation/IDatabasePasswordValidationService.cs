namespace MargamParkArchives.Core.Database.PasswordManagement.Validation;

public interface IDatabasePasswordValidationService
{
    public Task<PasswordValidationResponse> ValidatePasswordAsync(string password);
}
