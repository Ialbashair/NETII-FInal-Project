using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal static class SqlConnectionProvider
    {
        // Use a factory method to ensure that there is only 
        // one method in the enitre priejct that has the database
        // connection string

        // connections strings always include the server, database, credentials
        // be sure to change [LocalHost] to correct value
        static string connectionString = @"Data Source=[LocalHost];Initial Catalog=phonedbam;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(connectionString);
            return connection;
        }

    }
}
