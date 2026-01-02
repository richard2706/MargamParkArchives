using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.SpecificLocationEntity;

public static class SpecificLocationRules
{
    public const int SummaryMaxLength = 255;

    public static bool IsValidSummary(string? summary, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(summary, SummaryMaxLength, propertyName, out error);
    }
}
