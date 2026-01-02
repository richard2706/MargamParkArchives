using MargamParkArchives.Core.Entities.PeriodEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class PeriodTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        Period actual = new(1, "abc");
        Assert.NotNull(actual);
    }

    [Fact]
    public void Constructor_WithMaxLengthDates_CreatesObject()
    {
        string maxLengthDates = new('a', PeriodRules.DatesMaxLength);
        Period actual = new(1, maxLengthDates);
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Fact]
    public void Constructor_WithEmptyDates_ThrowsException()
    {
        Assert.ThrowsAny<ArgumentException>(() => new Period(1, ""));
    }

    [Fact]
    public void Constructor_WithTooLongDates_ThrowsException()
    {
        string tooLongDates = new('a', PeriodRules.DatesMaxLength + 1);
        Assert.ThrowsAny<ArgumentException>(() => new Period(1, tooLongDates));
    }
}
