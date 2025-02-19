using MargamParkArchivesData.Entities;
using MySqlConnector;
using System;
using System.Data;
using System.Diagnostics;

namespace MargamParkArchivesData
{
    /// <summary>
    /// Class of static methods for database operations used thoughout the application.
    /// </summary>
    internal static class MySqlConnectionFactory
    {
        // Connection constants
        private const string ServerLocation = "localhost";
        private const string ReadOnlyUsername = "readonly_user";
        private const string DatabaseName = "margam_archives";

        internal static MySqlConnection GetReadOnlyConnection()
        {
            return new MySqlConnection(GetReadOnlyConnectionString());
        }

        private static string GetReadOnlyConnectionString() =>
            new MySqlConnectionStringBuilder()
            {
                Server = ServerLocation,
                UserID = ReadOnlyUsername,
                Database = DatabaseName
            }.ConnectionString;
    }
}
