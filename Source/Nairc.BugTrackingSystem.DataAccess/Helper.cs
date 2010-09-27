using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nairc.BugTrackingSystem.DataAccess
{
    public class Helper
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            }
        }
    }
}
