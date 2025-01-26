namespace MargamParkArchivesTests.Entities; 

using MargamParkArchives.Entities;
using static TestMessageConstants;

[TestClass]
public class CategoryBaseTests
{
    private readonly string _nameMismatchMsg = GetPropertyMismatchMsg(nameof(CategoryBase.Name), nameof(CategoryBase));

    [TestMethod]
    public void CreateInstance_GivenAllData_ContainsAllData()
    {
        const string name = "Test";
        CategoryBase category = new(name);

        Assert.AreEqual(name, category.Name, _nameMismatchMsg);
    }
}
