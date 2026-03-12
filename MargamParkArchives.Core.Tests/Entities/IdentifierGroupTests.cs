using MargamParkArchives.Core.Entities.IdentifierGroupEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class IdentifierGroupTests
{
    // Valid construction
    [Theory]
    [InlineData("A", "Apple")] // Min length Id
    [InlineData("AAA", "Apple")] // Max length Id
    public void Constructor_WithValidData_CreatesObject(string id, string name)
    {
        IdentifierGroup actual = new(id, name);
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Theory]
    [InlineData("", "Apple")]
    [InlineData("A", "")]
    public void Constructor_WithEmptyStringProperty_ThrowsException(string id, string name)
    {
        Assert.ThrowsAny<ArgumentException>(() => new IdentifierGroup(id, name));
    }

    [Fact]
    public void Constructor_WithTooLongId_ThrowsException()
    {
        string tooLongId = new('a', IdentifierGroupValidationRules.IdentifierGroupIdMaxLength + 1);
        Assert.ThrowsAny<ArgumentException>(() => new IdentifierGroup(tooLongId, "Apple"));
    }

    [Fact]
    public void Constructor_WithTooLongName_ThrowsException()
    {
        string tooLongName = new('a', IdentifierGroupValidationRules.NameMaxLength + 1);
        Assert.ThrowsAny<ArgumentException>(() => new IdentifierGroup("A", tooLongName));
    }
}
