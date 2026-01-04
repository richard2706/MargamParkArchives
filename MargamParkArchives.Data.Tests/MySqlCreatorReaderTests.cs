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
    public async Task GetAllCreatorsAsync_ReturnsAllCreators()
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
}
