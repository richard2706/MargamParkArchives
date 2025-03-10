using MargamParkArchivesData.Entities;
using MySqlConnector;
using System.Data;
using System.Diagnostics;
using static MargamParkArchivesData.DatabaseConstants;

namespace MargamParkArchivesData;

public class MySqlArtefactDataAccess : IArtefactDataAccess
{
    // Messages
    private const string _openConnectionFailMsg = "There was an error connecting to the database.";
    private const string _connectionOpenedMsg = "Database connection {0} has been opened.";
    private const string _connectionClosedMsg = "Database connection {0} has been closed.";

    /// <summary>
    /// Gets a list of artefact objects resulting from the given database query. Assumes the query is querying the ArtefactDetails view.
    /// </summary>
    /// <param name="query">SQL query to execute on the database. Must be on the ArtefactDetails view or</param>
    /// <returns></returns>
    public Artefact[] GetArtefactList(string query)
    {
        using MySqlConnection connection = MySqlConnectionFactory.GetReadOnlyConnection();
        try
        {
            connection.Open();
            Debug.WriteLine(_connectionOpenedMsg, connection.ServerThread);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(_openConnectionFailMsg);
            Debug.WriteLine(ex.Message);
            throw;
        }

        MySqlCommand command = new(query, connection);
        using MySqlDataReader dbReader = command.ExecuteReader();

        List<Artefact> artefacts = [];
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

            artefacts.Add(new Artefact()
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
            });
            i++;
        }
        dbReader.Close();
        Debug.WriteLine(_connectionClosedMsg, connection.ServerThread);
        return artefacts.ToArray();
    }
}
