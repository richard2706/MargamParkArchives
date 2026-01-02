namespace MargamParkArchives.Core.Entities.Validation;

public static class SpecificLocationRules
{
    private const int SummaryMaxLength = 255;

    public static bool IsValidSummary(string summary, out string error)
    {
        return StringLengthHelper.ValidateStringNotEmptyOrTooLong(summary, SummaryMaxLength, out error);
    }
}
