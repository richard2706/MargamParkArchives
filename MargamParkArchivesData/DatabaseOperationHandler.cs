using MargamParkArchivesData.Entities;
using MySqlConnector;
using System;
using System.Data;
using System.Diagnostics;
using static MargamParkArchivesData.DatabaseConstants;

namespace MargamParkArchivesData
{
    /// <summary>
    /// Class of static methods for database operations used thoughout the application.
    /// </summary>
    public class DatabaseOperationHandler
    {
        // Connection constants
        private const string ServerLocation = "localhost";
        private const string ReadOnlyUsername = "readonly_user";
        private const string DatabaseName = "margam_archives";
        private const string DbOpenConnectionFailMsg = "There was an error connecting to the database.";

        // Queries
        private const string GetRandomArtefactsQuery = "select * from {0} order by rand() limit {1};";

        private static readonly string _connectionString = GenerateReadOnlyConnectionString();

        public static Artefact[] GetRandomArtefacts(int numArtefacts = 3)
        {
            using MySqlConnection connection = new(_connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(DbOpenConnectionFailMsg);
                Debug.WriteLine(ex.Message);
                throw;
            }

            string query = string.Format(GetRandomArtefactsQuery, ArtefactDetailsViewName, numArtefacts);
            MySqlCommand command = new(query, connection);
            using MySqlDataReader dbReader = command.ExecuteReader();

            Artefact[] artefacts = new Artefact[numArtefacts];
            int i = 0;
            while (dbReader.Read())
            {
                Category? category = (dbReader.IsDBNull(CategoryId) || dbReader.IsDBNull(ArtefactDetailsCategoryName))
                    ? null
                    : new Category(dbReader.GetString(CategoryId), dbReader.GetString(ArtefactDetailsCategoryName));

                Creator? creator = (dbReader.IsDBNull(CreatorId) || dbReader.IsDBNull(ArtefactDetailsCreatorName))
                    ? null
                    : new Creator(dbReader.GetInt32(CreatorId), dbReader.GetString(ArtefactDetailsCreatorName));

                GeneralLocation? generalLocation = (dbReader.IsDBNull(GeneralLocationId) || dbReader.IsDBNull(ArtefactDetailsGeneralLocationName))
                    ? null
                    : new GeneralLocation(dbReader.GetInt32(GeneralLocationId), dbReader.GetString(ArtefactDetailsGeneralLocationName));

                SpecificLocation? specificLocation = (dbReader.IsDBNull(SpecificLocationId) || dbReader.IsDBNull(ArtefactDetailsSpecificLocationSummary))
                    ? null
                    : new SpecificLocation(dbReader.GetInt32(SpecificLocationId), dbReader.GetString(ArtefactDetailsSpecificLocationSummary));

                Period? period = (dbReader.IsDBNull(PeriodId) || dbReader.IsDBNull(ArtefactDetailsPeriodDates))
                    ? null
                    : new Period(dbReader.GetInt32(PeriodId), dbReader.GetString(ArtefactDetailsPeriodDates));

                artefacts[i] = new Artefact()
                {
                    IdentifierGroup = new
                    (
                        dbReader.GetString(ArtefactIdentiferGroupId),
                        dbReader.GetString(ArtefactDetailsIdentifierGroupName)
                    ),
                    IdentifierNumber = dbReader.GetInt32(ArtefactIdentifierNumber),
                    IdentifierKey = dbReader.GetString(ArtefactIdentifierKey),
                    DateCreated = dbReader.IsDBNull(ArtefactDateCreated) ? null : dbReader.GetDateTime(ArtefactDateCreated),
                    DateModified = dbReader.IsDBNull(ArtefactDateModified) ? null : dbReader.GetDateTime(ArtefactDateModified),
                    FilePath = dbReader.IsDBNull(ArtefactFilePath) ? null : dbReader.GetString(ArtefactFilePath),
                    ParentId = dbReader.IsDBNull(ArtefactParentId) ? null : dbReader.GetString(ArtefactParentId),
                    Notes = dbReader.IsDBNull(ArtefactNotes) ? null : dbReader.GetString(ArtefactNotes),
                    IsVisualArtefact = dbReader.IsDBNull(ArtefactVisualArtefact) ? null : dbReader.GetBoolean(ArtefactVisualArtefact),
                    LocationCoverage = dbReader.IsDBNull(ArtefactLocationCoverage) ? null : dbReader.GetString(ArtefactLocationCoverage),
                    TitleDescription = new()
                    {
                        TitleEn = dbReader.IsDBNull(ArtefactTitleEn) ? null : dbReader.GetString(ArtefactTitleEn),
                        TitleCy = dbReader.IsDBNull(ArtefactTitleCy) ? null : dbReader.GetString(ArtefactTitleCy),
                        DescriptionEn = dbReader.IsDBNull(ArtefactDescriptionEn) ? null : dbReader.GetString(ArtefactDescriptionEn),
                        DescriptionCy = dbReader.IsDBNull(ArtefactDescriptionCy) ? null : dbReader.GetString(ArtefactDescriptionCy)
                    },
                    Tags = new()
                    {
                        CultureTagEn = dbReader.IsDBNull(ArtefactCultureTagEn) ? null : dbReader.GetString(ArtefactCultureTagEn),
                        TagsCy = dbReader.IsDBNull(ArtefactTagsCy) ? null : dbReader.GetString(ArtefactTagsCy)
                    },
                    RightsDetails = new()
                    {
                        RightHolder1En = dbReader.IsDBNull(ArtefactRightHolder1En) ? null : dbReader.GetString(ArtefactRightHolder1En),
                        RightHolder1Cy = dbReader.IsDBNull(ArtefactRightHolder1Cy) ? null : dbReader.GetString(ArtefactRightHolder1Cy),
                        RightType1 = dbReader.IsDBNull(ArtefactRightType1) ? null : dbReader.GetString(ArtefactRightType1)
                    },
                    Category = category,
                    Creator = creator,
                    GeneralLocation = generalLocation,
                    SpecificLocation = specificLocation,
                    Period = period
                };
                i++;
            }
            dbReader.Close();
            return artefacts;
        }

        private static string GenerateReadOnlyConnectionString() =>
            new MySqlConnectionStringBuilder()
            {
                Server = ServerLocation,
                UserID = ReadOnlyUsername,
                Database = DatabaseName
            }.ConnectionString;
    }
}
