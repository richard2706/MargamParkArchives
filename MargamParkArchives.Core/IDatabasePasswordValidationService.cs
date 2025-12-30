namespace MargamParkArchives.Core;

public interface IDatabasePasswordValidationService
{
    public Task<bool> ValidatePasswordAsync(string password);
}
