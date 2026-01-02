using MargamParkArchives.Core.Entities.CreatorEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class CreatorTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        Creator actual = new(1, "Steve");
        Assert.NotNull(actual);
    }

    [Fact]
    public void Constructor_WithMaxLengthName_CreatesObject()
    {
        string maxLengthName = new('a', CreatorRules.NameMaxLength);
        Creator actual = new(1, maxLengthName);
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Fact]
    public void Constructor_WithEmptyName_ThrowsException()
    {
        Assert.ThrowsAny<ArgumentException>(() => new Creator(1, ""));
    }

    [Fact]
    public void Constructor_WithTooLongName_ThrowsException()
    {
        string tooLongName = new('a', CreatorRules.NameMaxLength + 1);
        Assert.ThrowsAny<ArgumentException>(() => new Creator(1, tooLongName));
    }
}
