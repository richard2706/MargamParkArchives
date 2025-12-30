namespace MargamParkArchives.Core;

public interface IDatabasePasswordValidationService
{
    public Task<PasswordValidationResponse> ValidatePasswordAsync(string password);
}
