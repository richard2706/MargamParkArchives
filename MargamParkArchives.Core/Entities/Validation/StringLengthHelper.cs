namespace MargamParkArchives.Core.Entities.Validation;

internal static class StringLengthHelper
{
    /// <summary>
    /// Returns true if the specified string is not empty and does not exceed the maximum length specified.
    /// </summary>
    /// <remarks>This method does not check for null values. Callers should ensure that the input string is
    /// not null before invoking this method.</remarks>
    /// <param name="value">The string value to validate. Cannot be null.</param>
    /// <param name="maxLength">The maximum number of characters allowed in the string. Must be greater than zero.</param>
    /// <param name="valueName">The name of the value being validated to be inserted into the error message.</param>
    /// <param name="error">When this method returns, contains an error message if validation fails; otherwise, null.</param>
    /// <returns>true if the string is not empty and its length does not exceed the specified maximum; otherwise, false.</returns>
    internal static bool ValidateNotEmptyOrTooLong(string value, int maxLength, string valueName, out string error)
    {
        if (value.Length == 0)
        {
            error = string.Format(ValidationMessages.ValueEmptyMessage, valueName);
            return false;
        }
        else if (value.Length > maxLength)
        {
            error = string.Format(ValidationMessages.ValueTooLongMessage, valueName, maxLength);
            return false;
        }
        else
        {
            error = "";
            return true;
        }
    }
}
