using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.IdentifierGroupEntity;
using Moq;

namespace MargamParkArchives.Data.Tests;

public class MySqlIdentifierGroupReaderTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly IIdentifierGroupReader _identifierGroupReader; // Instance of the class under test

    public MySqlIdentifierGroupReaderTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _identifierGroupReader = new MySqlIdentifierGroupReader(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetAllIdentifierGroupsAsync_IdentifierGroupsExist_ReturnsAllIdentifierGroups()
    {
        IdentifierGroup[] expected =
        [
            new("A", "Apple"),
            new("B", "Banana")
        ];

        // Set up data access mock
        const string sqlQuery = "select * from identifier_group;";
        IdentifierGroupDto[] identifierGroupDtos =
        [
            new IdentifierGroupDto() { IdentifierGroupId = "A", Name = "Apple" },
            new IdentifierGroupDto() { IdentifierGroupId = "B", Name = "Banana" }
        ];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<IdentifierGroupDto>(sqlQuery))
            .ReturnsAsync(identifierGroupDtos);

        // Execute reader method
        IdentifierGroup[] actual = await _identifierGroupReader.GetAllIdentifierGroupsAsync();

        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal( // Assert actual contains identifiergroups with correct property values in any order
            expected.Select(identifierGroup => (identifierGroup.Id, identifierGroup.Name)).OrderBy(item => item.Id),
            actual.Select(identifiergroup => (identifiergroup.Id, identifiergroup.Name)).OrderBy(item => item.Id));
    }

    [Fact]
    public async Task GetAllIdentifierGroupsAsync_NoIdentifierGroupsExist_ReturnsEmptyArray()
    {
        IdentifierGroup[] expectedIdentifierGroups = [];

        // Set up data access mock
        const string sqlQuery = "select * from identifier_group;";
        IdentifierGroupDto[] identifierGroupDtos = [];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<IdentifierGroupDto>(sqlQuery))
            .ReturnsAsync(identifierGroupDtos);

        // Execute reader method
        IdentifierGroup[] actual = await _identifierGroupReader.GetAllIdentifierGroupsAsync();

        Assert.NotNull(actual);
        Assert.Empty(actual);
    }
}
