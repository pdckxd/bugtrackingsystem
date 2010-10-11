using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nairc.BugTrackingSystem.DataAccess
{
    public class NaircConnectionString
    {
        public string ConnectionString { get; private set; }
        public string Name { get; private set; }
        public string ProviderName { get; private set; }

        public NaircConnectionString()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["BugTrackingSystem"];
            this.Name = settings.Name;
            this.ProviderName = settings.ProviderName;
            this.ConnectionString = settings.ConnectionString;
        }
    }
}
