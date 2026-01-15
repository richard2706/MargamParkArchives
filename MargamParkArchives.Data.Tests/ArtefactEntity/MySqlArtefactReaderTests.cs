using MargamParkArchives.Core.DataAccess.CategoryEntity;
using MargamParkArchives.Core.DataAccess.CreatorEntity;
using MargamParkArchives.Core.DataAccess.GeneralLocationEntity;
using MargamParkArchives.Core.DataAccess.IdentifierGroupEntity;
using MargamParkArchives.Core.DataAccess.PeriodEntity;
using MargamParkArchives.Core.DataAccess.SpecificLocationEntity;
using MargamParkArchives.Core.Database;
using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;
using MargamParkArchives.Core.Entities.ArtefactEntity;
using MargamParkArchives.Core.Entities.IdentifierGroupEntity;
using MargamParkArchives.Data.Connections;
using MargamParkArchives.Data.Entities;
using MargamParkArchives.Data.Entities.ArtefactEntity;
using Moq;

namespace MargamParkArchives.Data.Tests.ArtefactEntity;

public class MySqlArtefactReaderTests
{
    // Dependencies of the class under test
    private readonly Mock<IMySqlDataAccess> _dataAccessMock;
    private readonly Mock<IIdentifierGroupReader> _identifierGroupReaderMock;

    private readonly MySqlArtefactReader _artefactReader; // Instance of the class under test

    public MySqlArtefactReaderTests()
    {
        _dataAccessMock = new Mock<IMySqlDataAccess>();
        _identifierGroupReaderMock = new Mock<IIdentifierGroupReader>();

        _artefactReader = new MySqlArtefactReader(_dataAccessMock.Object, _identifierGroupReaderMock.Object,
            Mock.Of<ICategoryReader>(), Mock.Of<IPeriodReader>(), Mock.Of<ICreatorReader>(),
            Mock.Of<IGeneralLocationReader>(), Mock.Of<ISpecificLocationReader>());
    }

    [Theory]
    [InlineData("", 1)] // Invalid identifier group (cannot be empty)
    [InlineData("A", -1)] // Invalid identifier number (must be >= 0)
    public async Task GetOneArtefactAsync_InvalidArtefactIdentifiers_ThrowsException(string identiferGroupId,
        int identifierNumber)
    {
        await Assert.ThrowsAnyAsync<ArgumentException>(
            async () => await _artefactReader.GetOneArtefactAsync(identiferGroupId, identifierNumber));
    }

    [Fact]
    public async Task GetOneArtefactAsync_IdentifierGroupNotFound_ThrowsException()
    {
        // The identifier group id stored in the artefact does not exist in identifier group table.
        const string invalidIdentifierGroupId = "A";
        const int identifierNumber = 1;

        // Set up data access mock
        const string sqlQuery = "select * from artefact where identifier_group_id = @IdentifierGroupId and identifier_number = @IdentifierNumber;";
        ArtefactDto? artefactDto = new() { IdentifierGroupId = invalidIdentifierGroupId, IdentifierNumber = 1 };
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<ArtefactDto?, object>(
                sqlQuery,
                It.Is<object>( // Check anonymous object contains correct properties with correct values
                    o => o.GetType().GetProperty("IdentifierGroupId")!.GetValue(o)!.Equals(invalidIdentifierGroupId)
                    && (int)o.GetType().GetProperty("IdentifierNumber")!.GetValue(o)! == identifierNumber)))
            .ReturnsAsync(artefactDto);
        IdentifierGroup? identifierGroup = null;
        _identifierGroupReaderMock
            .Setup(x => x.GetOneIdentifierGroupAsync(invalidIdentifierGroupId))
            .ReturnsAsync(identifierGroup);

        await Assert.ThrowsAnyAsync<DataIntegrityException>(
            async () => await _artefactReader.GetOneArtefactAsync(invalidIdentifierGroupId, identifierNumber));
    }

    [Fact]
    public async Task GetOneArtefactAsync_ArtefactExists_ReturnsArtefact()
    {
        const string identifierGroupId = "A";
        const int identifierNumber = 1;
        IdentifierGroup identifierGroup = new(identifierGroupId, "Apple");
        Artefact expected = new(identifierGroup, 1);

        // Set up data access mock
        const string sqlQuery = "select * from artefact where identifier_group_id = @IdentifierGroupId and identifier_number = @IdentifierNumber;";
        ArtefactDto? artefactDto = new() { IdentifierGroupId = "A", IdentifierNumber = 1 };
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<ArtefactDto?, object>(
                sqlQuery,
                It.Is<object>( // Check anonymous object contains correct properties with correct values
                    o => o.GetType().GetProperty("IdentifierGroupId")!.GetValue(o)!.Equals(identifierGroupId)
                    && (int)o.GetType().GetProperty("IdentifierNumber")!.GetValue(o)! == identifierNumber)))
            .ReturnsAsync(artefactDto);
        _identifierGroupReaderMock
            .Setup(x => x.GetOneIdentifierGroupAsync(identifierGroupId))
            .ReturnsAsync(identifierGroup);

        // Execute reader method
        Artefact? actual = await _artefactReader.GetOneArtefactAsync(identifierGroupId, identifierNumber);

        _dataAccessMock.Verify( // Verify data access method was called exactly once
            x => x.GetOneItemAsync<ArtefactDto?, object>(
                sqlQuery,
                It.Is<object>(
                    o => o.GetType().GetProperty("IdentifierGroupId")!.GetValue(o)!.Equals(identifierGroupId)
                    && (int)o.GetType().GetProperty("IdentifierNumber")!.GetValue(o)! == identifierNumber)),
            Times.Once);
        Assert.NotNull(actual);
        Assert.Equal((expected.IdentifierGroup.Id, expected.IdentifierGroup.Name, expected.IdentifierNumber),
            (actual.IdentifierGroup.Id, actual.IdentifierGroup.Name, actual.IdentifierNumber));
    }

    [Fact]
    public async Task GetOneArtefactAsync_ArtefactDoesNotExist_ReturnsNull()
    {
        const string identifierGroupId = "A";
        const int identifierNumber = 1;

        // Set up data access mock
        const string sqlQuery = "select * from artefact where identifier_group_id = @IdentifierGroupId and identifier_number = @IdentifierNumber;";
        ArtefactDto? artefactDto = null;
        _dataAccessMock
            .Setup(x => x.GetOneItemAsync<ArtefactDto?, object>(
                sqlQuery,
                It.Is<object>( // Check anonymous object contains correct properties with correct values
                    o => o.GetType().GetProperty("IdentifierGroupId")!.GetValue(o)!.Equals(identifierGroupId)
                    && (int)o.GetType().GetProperty("IdentifierNumber")!.GetValue(o)! == identifierNumber)))
            .ReturnsAsync(artefactDto);

        // Execute reader method
        Artefact? actual = await _artefactReader.GetOneArtefactAsync(identifierGroupId, identifierNumber);

        Assert.Null(actual);
    }
}
