using MargamParkArchives.Core.Database.DataAccess;
using MargamParkArchives.Core.Entities.ArtefactDetailsReadModel;
using MargamParkArchives.Data.Entities.Artefact;
using MargamParkArchives.Data.Connections;
using Moq;

namespace MargamParkArchives.Data.Tests
{
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
                    IdentifierGroupId = "A", IdentifierNumber = 1, IdentifierKey = "A-000001",
                    IdentiferGroupName = "Apple"
                },
                new ArtefactDetailsDto()
                {
                    IdentifierGroupId = "B", IdentifierNumber = 1, IdentifierKey = "B-000001",
                    IdentiferGroupName = "Banana"
                }
            ];
            return randomArtefactDetailsDtos[0..numItems];
        }
    }
}