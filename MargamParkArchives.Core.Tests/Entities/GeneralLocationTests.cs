using MargamParkArchives.Core.Entities.GeneralLocationEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class GeneralLocationTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        GeneralLocation actual = new(1, "abc");
        Assert.NotNull(actual);
    }

    [Fact]
    public void Constructor_WithMaxLengthName_CreatesObject()
    {
        string maxLengthName = new('a', GeneralLocationRules.NameMaxLength);
        GeneralLocation actual = new(1, maxLengthName);
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Fact]
    public void Constructor_WithEmptyName_ThrowsException()
    {
        Assert.ThrowsAny<ArgumentException>(() => new GeneralLocation(1, ""));
    }

    [Fact]
    public void Constructor_WithTooLongName_ThrowsException()
    {
        string tooLongName = new('a', GeneralLocationRules.NameMaxLength + 1);
        Assert.ThrowsAny<ArgumentException>(() => new GeneralLocation(1, tooLongName));
    }
}
