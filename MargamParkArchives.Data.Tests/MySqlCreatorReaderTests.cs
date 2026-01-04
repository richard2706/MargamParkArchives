using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.CreatorEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.CreatorEntity;
using Moq;

namespace MargamParkArchives.Data.Tests;

public class MySqlCreatorReaderTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly ICreatorReader _creatorReader; // Instance of the class under test

    public MySqlCreatorReaderTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _creatorReader = new MySqlCreatorReader(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetAllCreatorsAsync_CreatorsExist_ReturnsAllCreators()
    {
        Creator[] expectedCreators =
        [
            new(1, "Jane Smith"),
            new(2, "Bob Smith")
        ];

        // Set up data access mock
        const string sqlQuery = "select * from creator;";
        CreatorDto[] creatorDtos =
        [
            new CreatorDto() { CreatorId = 1, Name = "Jane Smith" },
            new CreatorDto() { CreatorId = 2, Name = "Bob Smith" }
        ];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<CreatorDto>(sqlQuery))
            .ReturnsAsync(creatorDtos);

        // Execute reader method
        Creator[] actual = await _creatorReader.GetAllCreatorsAsync();

        Assert.Equal(expectedCreators.Length, actual.Length);
        Assert.Equal( // Assert actual contains creators with correct property values in any order
            expectedCreators.Select(creator => (creator.Id, creator.Name)).OrderBy(item => item.Id),
            actual.Select(creator => (creator.Id, creator.Name)).OrderBy(item => item.Id));
    }

    [Fact]
    public async Task GetAllCreatorsAsync_NoCreatorsExist_ReturnsEmptyArray()
    {
        Creator[] expectedCreators = [];

        // Set up data access mock
        const string sqlQuery = "select * from creator;";
        CreatorDto[] creatorDtos = [];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<CreatorDto>(sqlQuery))
            .ReturnsAsync(creatorDtos);

        // Execute reader method
        Creator[] actual = await _creatorReader.GetAllCreatorsAsync();

        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetOneCreatorAsync_InvalidId_ThrowsException()
    {
        const int invalidId = -1;
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await _creatorReader.GetOneCreatorAsync(invalidId));
    }

    [Fact]
    public async Task GetOneCreatorAsync_CreatorExists_ReturnsCreator()
    {
        const int creatorId = 1;
        Creator? expected = new(creatorId, "Jane Smith");

        // Set up data access mock
        const string sqlQuery = "select * from creator where creator_id = @CreatorId;";
        CreatorDto? creatorDto = new() { CreatorId = 1, Name = "Jane Smith" };
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<CreatorDto?, object>(
                sqlQuery,
                // Check anonymous object contains CreatorId property with correct value
                It.Is<object>(p => (int)p.GetType().GetProperty("CreatorId")!.GetValue(p)! == creatorId)))
            .ReturnsAsync(creatorDto);

        // Execute reader method
        Creator? actual = await _creatorReader.GetOneCreatorAsync(creatorId);

        Assert.NotNull(actual);
        Assert.Equal((expected.Id, expected.Name), (actual.Id, actual.Name)); // Check property values
    }

    [Fact]
    public async Task GetOneCreatorAsync_CreatorDoesNotExist_ReturnsNull()
    {
        const int creatorId = 1;

        // Set up data access mock
        const string sqlQuery = "select * from creator where creator_id = @CreatorId;";
        CreatorDto? creatorDto = null;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<CreatorDto?, object>(
                sqlQuery,
                // Check anonymous object contains CreatorId property with correct value
                It.Is<object>(p => (int)p.GetType().GetProperty("CreatorId")!.GetValue(p)! == creatorId)))
            .ReturnsAsync(creatorDto);

        // Execute reader method
        Creator? actual = await _creatorReader.GetOneCreatorAsync(creatorId);

        Assert.Null(actual);
    }
}
