using MargamParkArchives.Core.Entities.ArtefactEntity;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class ArtefactTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        IdentifierGroup identifierGroup = new("A", "Apple");
        Artefact actual = new(identifierGroup, 1, "A-000001");
        Assert.NotNull(actual);
    }

    [Fact]
    public void Constructor_MissingIdentifierKey_CreatesObjectWithIdentifierKey()
    {
        IdentifierGroup identifierGroup = new("A", "Apple");
        Artefact actual = new(identifierGroup, 1);
        Assert.NotNull(actual);
        Assert.Equal("A-000001", actual.IdentifierKey);
    }

    // Invalid construction is not possible
}
