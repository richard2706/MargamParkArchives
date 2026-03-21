using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Entities.PeriodEntity;

/// <summary>
/// Static methods for validating properties of a period entity.
/// Note the id is an int freely chosen by the user, so no validation is required.
/// </summary>
public static class PeriodValidationRules
{
    public const int DatesMaxLength = 50;

    public static bool IsValidDates(string? dates, string propertyName, out string error)
    {
        return StringLengthHelper.ValidateNotEmptyOrTooLong(dates, DatesMaxLength, propertyName, out error);
    }
}
