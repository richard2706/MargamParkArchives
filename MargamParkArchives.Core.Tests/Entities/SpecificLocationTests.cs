using MargamParkArchives.Core.Entities.SpecificLocationEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class SpecificLocationTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        SpecificLocation actual = new(1, "abc");
        Assert.NotNull(actual);
    }

    [Fact]
    public void Constructor_WithMaxLengthSummary_CreatesObject()
    {
        string maxLengthSummary = new('a', SpecificLocationValidationRules.SummaryMaxLength);
        SpecificLocation actual = new(1, maxLengthSummary);
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Fact]
    public void Constructor_WithEmptySummary_ThrowsException()
    {
        Assert.ThrowsAny<ArgumentException>(() => new SpecificLocation(1, ""));
    }

    [Fact]
    public void Constructor_WithTooLongSummary_ThrowsException()
    {
        string longSummary = new('a', SpecificLocationValidationRules.SummaryMaxLength + 1);
        Assert.ThrowsAny<ArgumentException>(() => new SpecificLocation(1, longSummary));
    }
}
