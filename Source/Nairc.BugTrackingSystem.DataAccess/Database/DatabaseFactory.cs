using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Nairc.BugTrackingSystem.DataAccess
{
    public static class DatabaseFactory
    {
        private static readonly string SqlProviderName = "";
        private static readonly string OracleProviderName = "";
        public static Database GetDatabaseConnection(NaircConnectionString connectionString)
        {
            
                return new SqlDatabase(connectionString);
        }
    }
}
