namespace MargamParkArchives.Core.Entities.ValidationHelpers;

internal static class StringLengthHelper
{
    private const string MaxLengthInvalidMessage = "Max length cannot be less than 1";

    /// <summary>
    /// Returns true if the specified string is not empty, not null and does not exceed the maximum length specified.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="maxLength">The maximum number of characters allowed in the string. Must be greater than zero.</param>
    /// <param name="propertyName">The name of the value being validated to be inserted into the error message.</param>
    /// <param name="error">When this method returns, contains an error message if validation fails; otherwise, null.</param>
    /// <returns>true if the string is not empty, not null and its length does not exceed the specified maximum; otherwise, false.</returns>
    internal static bool ValidateNotEmptyOrTooLong(string? value, int maxLength, string propertyName, out string error)
    {
        if (maxLength <= 0)
        {
            throw new ArgumentException(MaxLengthInvalidMessage);
        }
        else if (value == null || value.Length == 0)
        {
            error = string.Format(ValidationMessages.ValueEmptyMessage, propertyName);
            return false;
        }
        else if (value.Length > maxLength)
        {
            error = string.Format(ValidationMessages.ValueTooLongMessage, propertyName, maxLength);
            return false;
        }
        else
        {
            error = "";
            return true;
        }
    }

    /// <summary>
    /// Returns true if the specified string does not exceed the maximum length specified. The string is valid if it is empty or null.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="maxLength">The maximum number of characters allowed in the string. Must be greater than zero.</param>
    /// <param name="propertyName">The name of the value being validated to be inserted into the error message.</param>
    /// <param name="error">When this method returns, contains an error message if validation fails; otherwise, null.</param>
    /// <returns>true if the string length does not exceed the specified maximum (including if the string is empty or null); otherwise, false.</returns>
    internal static bool ValidateNotTooLong(string? value, int maxLength, string propertyName, out string error)
    {
        if (maxLength <= 0)
        {
            throw new ArgumentException(MaxLengthInvalidMessage);
        }
        else if (value != null && value.Length > maxLength)
        {
            error = string.Format(ValidationMessages.ValueTooLongMessage, propertyName, maxLength);
            return false;
        }
        else
        {
            error = "";
            return true;
        }
    }
}
