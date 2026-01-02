namespace MargamParkArchives.Core.Entities.Validation;

public static class SpecificLocationRules
{
    private const int SummaryMaxLength = 255;

    public static bool IsValidSummary(string summary, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(summary, SummaryMaxLength, nameof(summary), out error);
    }
}
