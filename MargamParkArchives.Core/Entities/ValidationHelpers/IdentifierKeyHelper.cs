namespace MargamParkArchives.Core.Entities.ValidationHelpers;

public static class IdentifierKeyHelper
{
    public static string BuildIdentifierKey(string identiferGroupId, int identiferNumber)
    {
        return $"{identiferGroupId}-{identiferNumber:D6}";
    }
}
