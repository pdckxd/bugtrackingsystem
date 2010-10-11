using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.BugTrackingSystem.DataAccess
{
    public static class DatabaseFactory
    {
        public static Database GetDatabaseConnection(NaircConnectionString connectionString)
        {
            return new Database();
        }
    }
}
