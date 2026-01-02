using MargamParkArchives.Core.Entities.ArtefactEntity;

namespace MargamParkArchives.Core.Tests.Entities;

public class ArtefactContentTests
{
    // Valid construction
    [Fact]
    public void Constructor_WithValidData_CreatesObject()
    {
        ArtefactContent actual = new("Castle photo", "Welsh title", "Castle photo description", "Welsh description",
            "notes about castle photo");
        Assert.NotNull(actual);
    }

    // Invalid construction
    [Theory]
    [MemberData(nameof(TooLongStringPropertyTestData))]
    public void Constructor_WithTooLongStringProperty_ThrowsException(string? titleEn, string? titleCy,
        string? descriptionEn, string? descriptionCy, string? notes)
    {
        Assert.ThrowsAny<ArgumentException>(() => new ArtefactContent(titleEn, titleCy, descriptionEn, descriptionCy,
            notes));
    }

    public static TheoryData<string?, string?, string?, string?, string?> TooLongStringPropertyTestData =>
        new()
        {
            { new string('a', ArtefactContentRules.TitleMaxLength + 1), null, null, null, null },
            { null, new string('a', ArtefactContentRules.TitleMaxLength + 1), null, null, null },
            { null, null, new string('a', ArtefactContentRules.DescriptionMaxLength + 1), null, null },
            { null, null, null, new string('a', ArtefactContentRules.DescriptionMaxLength + 1), null },
            { null, null, null, null, new string('a', ArtefactContentRules.NotesMaxLength + 1) }
        };
}
