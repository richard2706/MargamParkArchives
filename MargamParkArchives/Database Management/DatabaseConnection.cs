using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace MargamParkArchives
{
    internal class DatabaseConnection
    {
        private const string ServerLocation = "localhost";
        private const string DatabaseName = "margam_archives";

        private readonly bool _connectAsAdmin;
        private readonly string _connectionString;

        public DatabaseConnection(bool connectAsAdmin = false)
        {
            _connectAsAdmin = connectAsAdmin;
            _connectionString = GetConnectionString();
        }

        public ExecuteReaderQuery(string query)
        {

        }

        private string GetConnectionString()
        {
            // Get user details from encrypted database config file
            string user = _connectAsAdmin ? "admin" : "readonly";
            MySqlConnectionStringBuilder connectionStringBuilder = new()
            {
                Server = ServerLocation,
                UserID = user,
                Database = DatabaseName
            };
            return connectionStringBuilder.ConnectionString;
        }
    }
}
