namespace MargamParkArchivesTests.Entities; 

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CategoryBaseTests
{
    private const string ClassUnderTestName = "CategoryBase";

    [TestMethod]
    public void CreateCategory_GivenAllData_ContainsAllData()
    {
        const string description = "Test";
        CategoryBase category = new(description);

        Assert.AreEqual(description, category.Description, GetPropertyMismatchMsg("description", ClassUnderTestName));
    }
}
