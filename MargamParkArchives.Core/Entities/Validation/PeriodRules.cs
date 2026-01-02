namespace MargamParkArchives.Core.Entities.Validation;

public static class PeriodRules
{
    private const int DatesMaxLength = 50;

    public static bool IsValidDates(string dates, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(dates, DatesMaxLength, nameof(dates), out error);
    }
}
