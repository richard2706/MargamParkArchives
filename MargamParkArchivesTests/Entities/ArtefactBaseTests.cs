using MargamParkArchivesData.Entities;
using MargamParkArchivesData.Entities.ArtefactSubEntities;

namespace MargamParkArchivesDataTests.Entities;

[TestClass]
public class ArtefactBaseTests
{
    // Values for test artefact
    private const string TestFilePath = "path/to/file";
    private const string TestParentId = "1";
    private const string TestNotes = "Some notes.";
    private const bool TestIsVisualArtefact = true;
    private const string TestLocationCoverage = "location info";
    private const string TestTitleEn = "Test Artefact 1";
    private const string TestTitleCy = "Test Artefact 1 welsh";
    private const string TestDescriptionEn = "Test Artefact 1 details";
    private const string TestDescriptionCy = "Test Artefact 1 details welsh";
    private const string TestCultureTagEn = "Sample culture tag";
    private const string TestTagsCy = "Sample tagas welsh";
    private const string TestRightHolder1En = "Mr right holder";
    private const string TestRightHolder1Cy = "Mr right holder welsh";
    private const string TestRightType1 = "sample right type";

    [TestMethod]
    public void CreateInstance_GivenAllValues_ContainsAllValues()
    {
        ArtefactBase actualArtefact = CreateTestArtefactBaseInstance();
        Assert.AreEqual(CreateTestIdentifierGroup(), actualArtefact.IdentifierGroup);
        Assert.AreEqual(TestFilePath, actualArtefact.FilePath);
        Assert.AreEqual(TestParentId, actualArtefact.ParentId);
        Assert.AreEqual(TestNotes, actualArtefact.Notes);
        Assert.AreEqual(TestIsVisualArtefact, actualArtefact.IsVisualArtefact);
        Assert.AreEqual(TestLocationCoverage, actualArtefact.LocationCoverage);
        Assert.AreEqual(TestTitleEn, actualArtefact.TitleDescription?.TitleEn);
        Assert.AreEqual(TestTitleCy, actualArtefact.TitleDescription?.TitleCy);
        Assert.AreEqual(TestDescriptionEn, actualArtefact.TitleDescription?.DescriptionEn);
        Assert.AreEqual(TestDescriptionCy, actualArtefact.TitleDescription?.DescriptionCy);
        Assert.AreEqual(TestCultureTagEn, actualArtefact.Tags?.CultureTagEn);
        Assert.AreEqual(TestTagsCy, actualArtefact.Tags?.TagsCy);
        Assert.AreEqual(TestRightHolder1En, actualArtefact.RightsDetails?.RightHolder1En);
        Assert.AreEqual(TestRightHolder1Cy, actualArtefact.RightsDetails?.RightHolder1Cy);
        Assert.AreEqual(TestRightType1, actualArtefact.RightsDetails?.RightType1);
        Assert.AreEqual(CreateTestCategory(), actualArtefact.Category);
        Assert.AreEqual(CreateTestPeriod(), actualArtefact.Period);
        Assert.AreEqual(CreateTestCreator(), actualArtefact.Creator);
        Assert.AreEqual(CreateTestGeneralLocation(), actualArtefact.GeneralLocation);
        Assert.AreEqual(CreateTestSpecificLocation(), actualArtefact.SpecificLocation);
    }

    [TestMethod]
    public void CreateInstance_RequiredValuesOnly_ContainsCorrectValues()
    {
        ArtefactBase actualArtefact = new()
        {
            IdentifierGroup = CreateTestIdentifierGroup()
        };

        Assert.AreEqual(CreateTestIdentifierGroup(), actualArtefact.IdentifierGroup);
        Assert.AreEqual(null, actualArtefact.FilePath);
        Assert.AreEqual(null, actualArtefact.ParentId);
        Assert.AreEqual(null, actualArtefact.Notes);
        Assert.AreEqual(null, actualArtefact.IsVisualArtefact);
        Assert.AreEqual(null, actualArtefact.LocationCoverage);
        Assert.AreEqual(null, actualArtefact.TitleDescription?.TitleEn);
        Assert.AreEqual(null, actualArtefact.TitleDescription?.TitleCy);
        Assert.AreEqual(null, actualArtefact.TitleDescription?.DescriptionEn);
        Assert.AreEqual(null, actualArtefact.TitleDescription?.DescriptionCy);
        Assert.AreEqual(null, actualArtefact.Tags?.CultureTagEn);
        Assert.AreEqual(null, actualArtefact.Tags?.TagsCy);
        Assert.AreEqual(null, actualArtefact.RightsDetails?.RightHolder1En);
        Assert.AreEqual(null, actualArtefact.RightsDetails?.RightHolder1Cy);
        Assert.AreEqual(null, actualArtefact.RightsDetails?.RightType1);
        Assert.AreEqual(null, actualArtefact.Category);
        Assert.AreEqual(null, actualArtefact.Period);
        Assert.AreEqual(null, actualArtefact.Creator);
        Assert.AreEqual(null, actualArtefact.GeneralLocation);
        Assert.AreEqual(null, actualArtefact.SpecificLocation);
    }

    private static ArtefactBase CreateTestArtefactBaseInstance() => new()
    {
        IdentifierGroup = CreateTestIdentifierGroup(),
        FilePath = TestFilePath,
        ParentId = TestParentId,
        Notes = TestNotes,
        IsVisualArtefact = TestIsVisualArtefact,
        LocationCoverage = TestLocationCoverage,
        TitleDescription = new TitleDescription()
        {
            TitleEn = TestTitleEn,
            TitleCy = TestTitleCy,
            DescriptionEn = TestDescriptionEn,
            DescriptionCy = TestDescriptionCy
        },
        Tags = new Tags()
        {
            CultureTagEn = TestCultureTagEn,
            TagsCy = TestTagsCy
        },
        RightsDetails = new RightsInformation()
        {
            RightHolder1En = TestRightHolder1En,
            RightHolder1Cy = TestRightHolder1Cy,
            RightType1 = TestRightType1
        },
        Category = CreateTestCategory(),
        Period = CreateTestPeriod(),
        Creator = CreateTestCreator(),
        GeneralLocation = CreateTestGeneralLocation(),
        SpecificLocation = CreateTestSpecificLocation()
    };

    private static IdentifierGroup CreateTestIdentifierGroup() => new("ABC", "Sample group");

    private static Category CreateTestCategory() => new("A", "Sample category");

    private static Period CreateTestPeriod() => new (1, "pre 1900");

    private static Creator CreateTestCreator() => new(1, "sample person");

    private static GeneralLocation CreateTestGeneralLocation() => new(1, "test room");

    private static SpecificLocation CreateTestSpecificLocation() => new(1, "test cabinet");
}
