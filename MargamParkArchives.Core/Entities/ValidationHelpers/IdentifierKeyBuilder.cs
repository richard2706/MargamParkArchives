namespace MargamParkArchives.Core.Entities.ValidationHelpers;

internal static class IdentifierKeyBuilder
{
    internal static string Build(string identiferGroupId, int identiferNumber)
    {
        return $"{identiferGroupId}-{identiferNumber:D6}";
    }
}
