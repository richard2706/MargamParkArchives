namespace MargamParkArchives.Core.Entities.ValidationHelpers;

public static class IdentifierKeyBuilder
{
    public static string Build(string identiferGroupId, int identiferNumber)
    {
        return $"{identiferGroupId}-{identiferNumber:D6}";
    }
}
