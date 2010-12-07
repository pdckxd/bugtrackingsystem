using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Web;
using Nairc.KpwFramework.DataModel;

namespace Nairc.KpwFramework.TelescopeControler
{
    public class KpwCommands
    {

        [XmlAttribute]
        public string Header { get; set; }

        [XmlAttribute]
        public int ParameterWordCount { get; set; }

        [XmlAttribute]
        public string End { get; set; }

        [XmlAttribute]
        public string Enter { get; set; }

        private List<KpwCommand> _SubKpwCommandList = new List<KpwCommand>();
        public List<KpwCommand> SubKpwCommandList
        {
            get { return _SubKpwCommandList; }
            set { _SubKpwCommandList = value; }
        }

        private static KpwCommands cmds;

        public static KpwCommands Instance
        {
            get
            {
                string file = ConfigurationManager.AppSettings["commandFile"].ToString();

                file = HttpContext.Current.Server.MapPath(file);
                if(cmds == null)
                    cmds = KpwHelper.DeserializeFile<KpwCommands>(file);
                return cmds;
            }
        }

        public static string GetCommandString(string commandName, params string[] list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Instance.Header);

            KpwCommand cmd = Instance.SubKpwCommandList.Find(p=>p.Name == commandName);

            if (cmd != null)
            {
                sb.Append(cmd.Word);

                if (cmd.Name == CommandMessage.FindStarByPosition.ToString())
                {
                    string command = cmd.Command.Replace("{RA}", list[0]).Replace("{DEC}", list[1]);
                    sb.Append(command);
                }
                else if (cmd.Name == CommandMessage.FindStarByName.ToString())
                {
                    sb.Append(list[0]);
                }
                else
                {
                    sb.Append(cmd.Command);
                }

                sb.Append(Instance.End + Instance.Enter);
            }

            return sb.ToString();
        }
    }

    public class KpwCommand
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Word { get; set; }

        [XmlAttribute]
        public int RealParameterWordCount { get; set; }

        [XmlAttribute]
        public string Command { get; set; }
    }
}
