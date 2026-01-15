using MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities.IdentifierGroupEntity;
using Moq;

namespace MargamParkArchives.Data.Tests.IdentifierGroupEntity;

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

    [Fact]
    public async Task GetOneIdentifierGroupAsync_InvalidId_ThrowsException()
    {
        const string invalidId = ""; // Id cannot be an empty string
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await _identifierGroupReader.GetOneIdentifierGroupAsync(invalidId));
    }

    [Fact]
    public async Task GetOneIdentifierGroupAsync_IdentifierGroupExists_ReturnsIdentifierGroup()
    {
        const string identifierGroupId = "A";
        IdentifierGroup? expected = new(identifierGroupId, "Apple");

        // Set up data access mock
        const string sqlQuery = "select * from identifier_group where identifier_group_id = @IdentifierGroupId;";
        IdentifierGroupDto? identifierGroupDto = new() { IdentifierGroupId = "A", Name = "Apple" };
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<IdentifierGroupDto?, object>(
                sqlQuery,
                // Check anonymous object contains IdentifierGroupId property with correct value
                It.Is<object>(o => o.GetType().GetProperty("IdentifierGroupId")!.GetValue(o)!.Equals(identifierGroupId))))
            .ReturnsAsync(identifierGroupDto);

        // Execute reader method
        IdentifierGroup? actual = await _identifierGroupReader.GetOneIdentifierGroupAsync(identifierGroupId);

        Assert.NotNull(actual);
        Assert.Equal((expected.Id, expected.Name), (actual.Id, actual.Name)); // Check property values
    }

    [Fact]
    public async Task GetOneIdentifierGroupAsync_IdentifierGroupDoesNotExist_ReturnsNull()
    {
        const string identifierGroupId = "A";

        // Set up data access mock
        const string sqlQuery = "select * from identifier_group where identifier_group_id = @IdentifierGroupId;";
        IdentifierGroupDto? identifierGroupDto = null;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<IdentifierGroupDto?, object>(
                sqlQuery,
                // Check anonymous object contains IdentifierGroupId property with correct value
                It.Is<object>(o => o.GetType().GetProperty("IdentifierGroupId")!.GetValue(o)!.Equals(identifierGroupId))))
            .ReturnsAsync(identifierGroupDto);

        // Execute reader method
        IdentifierGroup? actual = await _identifierGroupReader.GetOneIdentifierGroupAsync(identifierGroupId);

        Assert.Null(actual);
    }
}
