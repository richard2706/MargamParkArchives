using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.PeriodEntity;

public static class PeriodValidationRules
{
    public const int DatesMaxLength = 50;

    public static bool IsValidDates(string? dates, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(dates, DatesMaxLength, propertyName, out error);
    }
}
