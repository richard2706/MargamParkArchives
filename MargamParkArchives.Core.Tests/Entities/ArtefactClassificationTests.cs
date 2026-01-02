using MargamParkArchives.Core.Entities.ArtefactEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class ArtefactClassificationTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        ArtefactClassification actual = new("abc", "tag1", "culturetag1", "def");
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Theory]
    [MemberData(nameof(TooLongStringPropertyTestData))]
    public void Constructor_WithTooLongStringProperty_ThrowsException(string? parentId, string? tagsCy,
        string? cultureTagEn, string? locationCoverage)
    {
        Assert.ThrowsAny<ArgumentException>(() => new ArtefactClassification(parentId, tagsCy, cultureTagEn,
            locationCoverage));
    }

    public static TheoryData<string?, string?, string?, string?> TooLongStringPropertyTestData =>
        new()
        {
            { new string('a', ArtefactClassificationRules.ParentIdMaxLength + 1), null, null, null },
            { null, new string('a', ArtefactClassificationRules.ClassificationTextMaxLength + 1), null, null },
            { null, null, new string('a', ArtefactClassificationRules.ClassificationTextMaxLength + 1), null },
            { null, null, null, new string('a', ArtefactClassificationRules.ClassificationTextMaxLength + 1) }
        };
}
