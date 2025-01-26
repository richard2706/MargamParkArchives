namespace MargamParkArchivesTests.Entities; 

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CategoryBaseTests
{
    private readonly string _descriptionMismatchMsg = GetPropertyMismatchMsg(nameof(CategoryBase.Description), nameof(CategoryBase));

    [TestMethod]
    public void CreateInstance_GivenAllData_ContainsAllData()
    {
        const string description = "Test";
        CategoryBase category = new(description);

        Assert.AreEqual(description, category.Description, _descriptionMismatchMsg);
    }
}
