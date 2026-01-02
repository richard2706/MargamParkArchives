using MargamParkArchives.Core.Entities.Validation;

namespace MargamParkArchives.Core.Entities.PeriodEntity;

public static class PeriodRules
{
    private const int DatesMaxLength = 50;

    public static bool IsValidDates(string? dates, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(dates, DatesMaxLength, propertyName, out error);
    }
}
