using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;

namespace MargamParkArchives.Core.Tests.Entities;

public class ArtefactDetailsReadModelTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        ArtefactDetailsReadModel actual = new("A", 1, "A-000001", "Apple");
        Assert.NotNull(actual);
    }

    [Fact]
    public void Constructor_MissingIdentifierKey_CreatesObjectWithIdentifierKey()
    {
        ArtefactDetailsReadModel actual = new("A", 1, null, "Apple");
        Assert.NotNull(actual);
        Assert.Equal("A-000001", actual.IdentifierKey);
    }
}
