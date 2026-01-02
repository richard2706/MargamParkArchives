namespace MargamParkArchives.Core.Entities.Validation;

internal static class IdentifierKeyBuilder
{
    internal static string Build(string identiferGroupId, int identiferNumber)
    {
        return $"{identiferGroupId}-{identiferNumber:D6}";
    }
}
