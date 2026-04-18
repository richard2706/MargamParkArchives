using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;
using MargamParkArchives.Data.Entities.ArtefactEntity;
using MargamParkArchives.Data.Connections;
using Moq;
using MargamParkArchives.Core.DataAccess.ArtefactEntity;

namespace MargamParkArchives.Data.Tests.ArtefactEntity;

public class MySqlArtefactDetailsReaderTests
{
    private readonly Mock<IMySqlDataAccess> _dataAccessMock; // Dependency of the class under test
    private readonly IArtefactDetailsReader _artefactDetailsReader; // Instance of the class under test

    public MySqlArtefactDetailsReaderTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _artefactDetailsReader = new MySqlArtefactDetailsReader(_dataAccessMock.Object);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetRandomArtefactsAsync_RequestMany_ReturnsArtefactsArray(int numArtefactsRequested)
    {
        ArtefactDetailsReadModel[] expectedArtefacts = GetArtefactDetailsReadModels(numArtefactsRequested);

        // Set up data access mock
        const string sqlQuery = "select * from artefact_details order by rand() limit @Limit;";
        ArtefactDetailsDto[] randomArtefactDetailsDtos = GetArtefactDetailsDtos(numArtefactsRequested);
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<ArtefactDetailsDto, object>(
                sqlQuery,
                // Checks the anonymous object contains a property called Limit that is set to numArtefactsRequested
                It.Is<object>(p => (int)p.GetType().GetProperty("Limit")!.GetValue(p)! == numArtefactsRequested)
                ))
            .ReturnsAsync(randomArtefactDetailsDtos);

        // Execute reader method
        ArtefactDetailsReadModel[] actual = await _artefactDetailsReader.GetRandomArtefactsAsync(numArtefactsRequested);

        _dataAccessMock.Verify( // Verify data access method was called exactly once
            x => x.GetManyItemsAsync<ArtefactDetailsDto, object>(sqlQuery, It.Is<object>(
                p => (int)p.GetType().GetProperty("Limit")!.GetValue(p)! == numArtefactsRequested)),
            Times.Once);
        Assert.Equal(expectedArtefacts.Length, actual.Length); // Array should contain correct number of artefacts
        Assert.Equal(expectedArtefacts, actual);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetRandomArtefactsAsync_InvalidNumArtefacts_ThrowsException(int numArtefactsRequested)
    {
        await Assert.ThrowsAnyAsync<ArgumentException>(
            async () => await _artefactDetailsReader.GetRandomArtefactsAsync(numArtefactsRequested));
    }

    [Fact]
    public async Task GetRandomArtefactsAsync_NoArtefactsExists_ReturnsEmptyArray()
    {
        const int numArtefactsRequested = 2;
        ArtefactDetailsReadModel[] expectedArtefacts = [];

        // Set up data access mock
        const string sqlQuery = "select * from artefact_details order by rand() limit @Limit;";
        ArtefactDetailsDto[] randomArtefactDetailsDtos = [];
        _dataAccessMock
            .Setup(x => x.GetManyItemsAsync<ArtefactDetailsDto, object>(
                sqlQuery,
                // Checks the anonymous object contains a property called Limit that is set to numArtefactsRequested
                It.Is<object>(o => (int)o.GetType().GetProperty("Limit")!.GetValue(o)! != numArtefactsRequested)
                ))
            .ReturnsAsync(randomArtefactDetailsDtos);

        // Execute reader method
        ArtefactDetailsReadModel[] actual = await _artefactDetailsReader.GetRandomArtefactsAsync(numArtefactsRequested);

        _dataAccessMock.Verify( // Verify data access method was called exactly once
            x => x.GetManyItemsAsync<ArtefactDetailsDto, object>(
                sqlQuery,
                It.Is<object>(o => (int)o.GetType().GetProperty("Limit")!.GetValue(o)! == numArtefactsRequested)),
            Times.Once);

        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Theory]
    [InlineData("", 1)] // Invalid identifier group (cannot be empty)
    [InlineData("A", -1)] // Invalid identifier number (must be >= 0)
    public async Task GetOneArtefactAsync_InvalidArtefactIdentifiers_ThrowsException(string identiferGroupId,
        int identifierNumber)
    {
        await Assert.ThrowsAnyAsync<ArgumentException>(
            async () => await _artefactDetailsReader.GetOneArtefactAsync(identiferGroupId, identifierNumber));
    }

    [Fact]
    public async Task GetOneArtefactAsync_ArtefactExists_ReturnsArtefact()
    {
        ArtefactDetailsReadModel expectedArtefact = GetArtefactDetailsReadModels(1)[0];

        // Set up data access mock
        const string sqlQuery = "select * from artefact_details where identifier_group_id = @IdentifierGroupId and identifier_number = @IdentifierNumber;";
        ArtefactDetailsDto? artefactDetailsDto = GetArtefactDetailsDtos(1)[0];
        string identifierGroupId = artefactDetailsDto.identifier_group_id;
        int identifierNumber = artefactDetailsDto.identifier_number;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<ArtefactDetailsDto?, object>(
                sqlQuery,
                It.Is<object>(p => // Check anonymous object contains correct properties with correct values
                    p.GetType().GetProperty("IdentifierGroupId")!.GetValue(p)!.Equals(identifierGroupId)
                    && (int)p.GetType().GetProperty("IdentifierNumber")!.GetValue(p)! == identifierNumber)))
            .ReturnsAsync(artefactDetailsDto);

        // Execute reader method
        ArtefactDetailsReadModel? actual = await _artefactDetailsReader.GetOneArtefactAsync(identifierGroupId, identifierNumber);

        _dataAccessMock.Verify( // Verify data access method was called exactly once
            x => x.GetOneItemAsync<ArtefactDetailsDto?, object>(
                sqlQuery,
                It.Is<object>(
                    p => p.GetType().GetProperty("IdentifierGroupId")!.GetValue(p)!.Equals(identifierGroupId)
                    && (int)p.GetType().GetProperty("IdentifierNumber")!.GetValue(p)! == identifierNumber)),
            Times.Once);
        Assert.NotNull(actual);
        Assert.Equal(expectedArtefact, actual);
    }

    [Fact]
    public async Task GetOneArtefactAsync_ArtefactDoesNotExist_ReturnsNull()
    {
        const string identifierGroupId = "A";
        const int identifierNumber = 1;

        // Set up data access mock
        const string sqlQuery = "select * from artefact_details where identifier_group_id = @IdentifierGroupId and identifier_number = @IdentifierNumber;";
        ArtefactDetailsDto? artefactDetailsDto = null;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<ArtefactDetailsDto?, object>(
                sqlQuery,
                It.Is<object>(p => // Check anonymous object contains correct properties with correct values
                    p.GetType().GetProperty("IdentifierGroupId")!.GetValue(p)!.Equals(identifierGroupId)
                    && (int)p.GetType().GetProperty("IdentifierNumber")!.GetValue(p)! == identifierNumber)))
            .ReturnsAsync(artefactDetailsDto);

        // Execute reader method
        ArtefactDetailsReadModel? actual = await _artefactDetailsReader.GetOneArtefactAsync(identifierGroupId, identifierNumber);

        Assert.Null(actual);
    }

    private static ArtefactDetailsReadModel[] GetArtefactDetailsReadModels(int numItems)
    {
        ArtefactDetailsReadModel[] expectedArtefacts = // Method under test should return read models
        [
            new("A", 1, "A-000001", "Apple"),
            new("B", 1, "B-000001", "Banana")
        ];
        return expectedArtefacts[0..numItems];
    }

    private static ArtefactDetailsDto[] GetArtefactDetailsDtos(int numItems)
    {
        ArtefactDetailsDto[] randomArtefactDetailsDtos = // Data access mocked method returns random Dto types
        [
            new ArtefactDetailsDto()
            {
                identifier_group_id = "A", identifier_number = 1, identifier_key = "A-000001",
                identifer_group_name = "Apple"
            },
            new ArtefactDetailsDto()
            {
                identifier_group_id = "B", identifier_number = 1, identifier_key = "B-000001",
                identifer_group_name = "Banana"
            }
        ];
        return randomArtefactDetailsDtos[0..numItems];
    }
}