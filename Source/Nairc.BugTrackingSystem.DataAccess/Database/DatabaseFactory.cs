using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Nairc.BugTrackingSystem.DataAccess
{
    public static class DatabaseFactory
    {
        private static readonly string SqlProviderName = "System.Data.SqlClient";
        private static readonly string OracleProviderName = "System.Data.OracleClient";

        public static Database GetDatabaseConnection(NaircConnectionString connectionString)
        {
            if (connectionString.ProviderName.ToUpper() == SqlProviderName.ToUpper())
            {
                return new SqlDatabase(connectionString);
            }
            else if (connectionString.ProviderName.ToUpper() == OracleProviderName.ToUpper())
            {
                return new OracleDatabase(connectionString);
            }
            else
            {
                throw new Exception("No Data Provider exception!");
            }
        }
    }
}
