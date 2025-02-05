using MargamParkArchivesData.Entities;
//using MySql.Data.MySqlClient;
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

            string query = string.Format(GetRandomArtefactsQuery, ArtefactTableName, numArtefacts);
            MySqlCommand command = new(query, connection);
            using MySqlDataReader dataReader = command.ExecuteReader();

            Artefact[] artefacts = new Artefact[numArtefacts];
            int i = 0;
            while (dataReader.Read())
            {
                artefacts[i] = new Artefact()
                {
                    IdentifierGroup = dataReader.GetString(ArtefactIdentiferGroupId),
                    IdentifierNumber = dataReader.GetInt32(ArtefactIdentifierNumber),
                    IdentifierKey = dataReader.GetString(ArtefactIdentifierKey),
                    FilePath = dataReader.IsDBNull(ArtefactFilePath) ? null : dataReader.GetString(ArtefactFilePath),
                    DateCreated = dataReader.IsDBNull(ArtefactDateCreated) ? null : dataReader.GetDateTime(ArtefactDateCreated),
                    DateModified = dataReader.IsDBNull(ArtefactDateModified) ? null : dataReader.GetDateTime(ArtefactDateModified),
                    //ParentId = dataReader.GetString(ArtefactParentId),
                    //Notes = dataReader.GetString(ArtefactNotes),
                    //TitleDescription = new()
                    //{
                    //    TitleEn = dataReader.GetString(ArtefactTitleEn),
                    //    TitleCy = dataReader.GetString(ArtefactTitleCy),
                    //    DescriptionEn = dataReader.GetString(ArtefactDescriptionEn),
                    //}
                };
                i++;
            }
            dataReader.Close();
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
