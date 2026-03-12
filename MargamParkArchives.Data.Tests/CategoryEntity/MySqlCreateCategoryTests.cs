using MargamParkArchives.Core.DataAccess.CategoryEntity;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Entities.CategoryEntity;
using MargamParkArchives.Core.Entities.ValidationHelpers;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CategoryEntity;
using Moq;

namespace MargamParkArchives.Data.Tests.CategoryEntity;

public class MySqlCreateCategoryTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly ICategoryWriter _categoryWriter; // Instance of the class under test

    public MySqlCreateCategoryTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _categoryWriter = new MySqlCategoryWriter(_dataAccessMock.Object);
    }

    [Theory]
    [InlineData("", "Book")]
    [InlineData("B", "")]
    public async Task CreateCategoryAsync_EmptyCategoryIdOrName_ThrowsException(string id, string name)
    {
        CategoryCreateDto newCategory = new() { CategoryId = id, Name = name };
        await Assert.ThrowsAnyAsync<ValidationException>(async () => await _categoryWriter.CreateCategoryAsync(newCategory));
    }

    [Fact]
    public async Task CreateCategoryAsync_WithTooLongId_ThrowsException()
    {
        string tooLongId = new('A', CategoryRules.IdMaxLength + 1);
        CategoryCreateDto newCategory = new() { CategoryId = tooLongId, Name = "Book" };
        await Assert.ThrowsAnyAsync<ValidationException>(async () => await _categoryWriter.CreateCategoryAsync(newCategory));
    }

    [Fact]
    public async Task CreateCategoryAsync_WithTooLongName_ThrowsException()
    {
        string tooLongName = new('A', CategoryRules.NameMaxLength + 1);
        CategoryCreateDto newCategory = new() { CategoryId = "B", Name = tooLongName };
        await Assert.ThrowsAnyAsync<ValidationException>(async () => await _categoryWriter.CreateCategoryAsync(newCategory));
    }

    [Fact]
    public async Task CreateCategoryAsync_CategoryNotInserted_ThrowsException()
    {
        CategoryCreateDto newCategory = new() { CategoryId = "A", Name = "Apple" };

        // Set up data access mock to simulate failed insertion
        const string sqlQuery = "insert into category (category_id, name) values (@CategoryId, @Name);";
        _dataAccessMock
            .Setup(x => x.InsertAsync<object>(
                sqlQuery,
                // Check anonymous object contains CategoryId and Name properties with correct values
                It.Is<object>(o =>
                    o.GetType().GetProperty("CategoryId")!.GetValue(o)!.Equals("A") &&
                    o.GetType().GetProperty("Name")!.GetValue(o)!.Equals("Apple")
                )))
            .ReturnsAsync(false); // Simulate failure

        await Assert.ThrowsAnyAsync<DatabaseException>(async () => await _categoryWriter.CreateCategoryAsync(newCategory));
    }

    [Fact]
    public async Task CreateCategoryAsync_ValidCategory_ReturnsNewCategoryId()
    {
        const string categoryId = "A";
        CategoryCreateDto newCategory = new() { CategoryId = categoryId, Name = "Apple" };

        // Set up data access mock
        const string sqlQuery = "insert into category (category_id, name) values (@CategoryId, @Name);";
        _dataAccessMock
            .Setup(x => x.InsertAsync<object>(
                sqlQuery,
                // Check anonymous object contains CategoryId and Name properties with correct values
                It.Is<object>(o =>
                    o.GetType().GetProperty("CategoryId")!.GetValue(o)!.Equals(categoryId) &&
                    o.GetType().GetProperty("Name")!.GetValue(o)!.Equals("Apple")
                )))
            .ReturnsAsync(true); // Assuming it returns number of affected rows

        var actualCategoryId = await _categoryWriter.CreateCategoryAsync(newCategory);

        Assert.Equal(categoryId, actualCategoryId);
    }
}
