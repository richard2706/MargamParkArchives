using MargamParkArchives.Core.Entities.ValidationHelpers;

namespace MargamParkArchives.Core.Tests.Entities.ValidationHelpers;

public class StringLengthHelperTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void ValidateNotEmptyOrTooLong_ThrowsException_MaxLength0OrNegative(int maxLength)
    {
        string stringToValidate = "a";
        Assert.Throws<ArgumentException>(() => StringLengthHelper.ValidateNotEmptyOrTooLong(stringToValidate,
            maxLength, nameof(stringToValidate), out _));
    }

    [Theory]
    [InlineData("elephant", 8)]
    [InlineData("elephant", 20)]
    [InlineData("a", 1)]
    public void ValidateNotEmptyOrTooLong_ReturnsTrue_ValidString(string stringToValidate, int maxLength)
    {
        bool actual = StringLengthHelper.ValidateNotEmptyOrTooLong(stringToValidate, maxLength,
            nameof(stringToValidate), out _);
        Assert.True(actual);
    }

    [Theory]
    [InlineData(null, 8)]
    [InlineData("elephants", 8)]
    [InlineData("ab", 1)]
    public void ValidateNotEmptyOrTooLong_ReturnsFalse_InvalidString(string stringToValidate, int maxLength)
    {
        bool actual = StringLengthHelper.ValidateNotEmptyOrTooLong(stringToValidate, maxLength,
            nameof(stringToValidate), out _);
        Assert.False(actual);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void ValidateNotTooLong_ThrowsException_MaxLength0OrNegative(int maxLength)
    {
        string stringToValidate = "a";
        Assert.Throws<ArgumentException>(() => StringLengthHelper.ValidateNotTooLong(stringToValidate, maxLength,
            nameof(stringToValidate), out _));
    }

    [Theory]
    [InlineData(null, 8)]
    [InlineData("", 8)]
    [InlineData("a", 1)]
    [InlineData("elephant", 8)]
    [InlineData("elephant", 20)]
    public void ValidateNotTooLong_ReturnsTrue_ValidString(string stringToValidate, int maxLength)
    {
        bool actual = StringLengthHelper.ValidateNotTooLong(stringToValidate, maxLength, nameof(stringToValidate),
            out _);
        Assert.True(actual);
    }

    [Theory]
    [InlineData("elephants", 8)]
    [InlineData("ab", 1)]
    public void ValidateNotTooLong_ReturnsFalse_InvalidString(string stringToValidate, int maxLength)
    {
        bool actual = StringLengthHelper.ValidateNotTooLong(stringToValidate, maxLength, nameof(stringToValidate),
            out _);
        Assert.False(actual);
    }
}
