using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Nairc.BugTrackingSystem.DataAccess
{
    public class SqlDatabase:Database
    {
        public SqlDatabase(NaircConnectionString connectionString)
            : base(connectionString, SqlClientFactory.Instance)
        {
        }
    }
}
