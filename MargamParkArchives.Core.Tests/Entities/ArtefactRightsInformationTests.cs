using MargamParkArchives.Core.Entities.ArtefactEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class ArtefactRightsInformationTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        ArtefactRightsInformation actual = new("abc", "def", "ghi");
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Theory]
    [MemberData(nameof(TooLongStringPropertyTestData))]
    public void Constructor_WithTooLongStringProperty_ThrowsException(string? rightType1, string? rightHolder1En,
        string? rightHolder1Cy)
    {
        Assert.ThrowsAny<ArgumentException>(() => new ArtefactRightsInformation(rightType1, rightHolder1En,
            rightHolder1Cy));
    }

    public static TheoryData<string?, string?, string?> TooLongStringPropertyTestData =>
        new()
        {
            { new string('a', ArtefactRightsInformationRules.RightsInformationMaxLength + 1), null, null },
            { null, new string('a', ArtefactRightsInformationRules.RightsInformationMaxLength + 1), null },
            { null, null, new string('a', ArtefactRightsInformationRules.RightsInformationMaxLength + 1) }
        };
}
