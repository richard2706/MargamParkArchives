using MargamParkArchives.Core.Entities.CategoryEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class CategoryTests
{
    // Valid construction
    [Theory]
    [InlineData("B", "Book")] // Min length Id
    [InlineData("BB", "Book")] // Max length Id
    public void Constructor_WithValidData_CreatesObject(string id, string name)
    {
        Category actual = new(id, name);
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Theory]
    [InlineData("", "Book")]
    [InlineData("B", "")]
    public void Constructor_WithEmptyStringProperty_ThrowsException(string id, string name)
    {
        Assert.ThrowsAny<ArgumentException>(() => new Category(id, name));
    }

    [Fact]
    public void Constructor_WithTooLongId_ThrowsException()
    {
        string tooLongId = new('a', CategoryValidationRules.CategoryIdMaxLength + 1);
        Assert.ThrowsAny<ArgumentException>(() => new Category(tooLongId, "Book"));
    }

    [Fact]
    public void Constructor_WithTooLongName_ThrowsException()
    {
        string tooLongName = new('a', CategoryValidationRules.NameMaxLength + 1);
        Assert.ThrowsAny<ArgumentException>(() => new Category("B", tooLongName));
    }
}
