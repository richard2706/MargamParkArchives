using MargamParkArchives.Core.DataAccess.CategoryEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CategoryEntity;
using Moq;

namespace MargamParkArchives.Data.Tests.CategoryEntity;

public class MySqlUpdateCategoryTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly ICategoryWriter _categoryWriter; // Instance of the class under test

    public MySqlUpdateCategoryTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _categoryWriter = new MySqlCategoryWriter(_dataAccessMock.Object);
    }

    [Fact]
    public async Task UpdateCategoryAsync_NotFound_ThrowsException()
    {
        const string categoryId = "A";
        const string categoryName = "Apple";
        CategoryUpdateDto category = new() { ExistingCategoryId = categoryId, NewCategoryId = categoryId, Name = categoryName };

        // Set up data access mock
        const string sqlQuery = "update category set category_id = @NewCategoryId, name = @Name where category_id = @ExistingCategoryId;";
        _dataAccessMock
            .Setup(x => x.UpdateAsync<object>(
                sqlQuery,
                // Check anonymous object contains properties to match query parameters with correct values
                It.Is<object>(o =>
                    o.GetType().GetProperty("NewCategoryId")!.GetValue(o)!.Equals(categoryId) &&
                    o.GetType().GetProperty("Name")!.GetValue(o)!.Equals(categoryName) &&
                    o.GetType().GetProperty("ExistingCategoryId")!.GetValue(o)!.Equals(categoryId)
                )))
            .ReturnsAsync(true); // Assuming it returns number of affected rows

        bool success = await _categoryWriter.UpdateCategoryAsync(category);

        Assert.True(success);
    }

    // Check the test cases below, logic may be incorrect

    // Test existing category id not found, returns false

    // Test new category id already exists and is diffenrent to existing category id, db should reject

    // Test new category id is empty, should throw validation exception

    // Test new category id is too long, should throw validation exception

    // Test name is empty, should throw validation exception

    // Test name is too long, should throw validation exception
}
