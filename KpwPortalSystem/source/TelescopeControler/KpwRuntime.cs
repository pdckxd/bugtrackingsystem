using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Nairc.KpwFramework.DataModel;

namespace Nairc.KpwFramework.TelescopeControler
{
    public class KpwRuntime
    {
        static private KpwRuntime runtime = new KpwRuntime();

        public static KpwRuntime Instance
        {
            get
            {
                if (runtime == null)
                {
                    runtime = new KpwRuntime();
                }

                return runtime;
            }
        }

        public KpwTcpClient CommandSender { get; set; }

        public KpwCommands Commands { get; set; }

        public TelescopeStatusDataModel TelescopeStatus
        {
            get
            {
                return this.CommandSender.TelescopeStatus;
            }
        }
        

        private KpwRuntime()
        {
            string hostName = ConfigurationManager.AppSettings["telescopeEngineIP"].ToString();
            int port = int.Parse(ConfigurationManager.AppSettings["telescopeEnginPort"].ToString());
            this.CommandSender = new KpwTcpClient(hostName, port);

            this.Commands = KpwCommands.Instance;
        }
    }
}
