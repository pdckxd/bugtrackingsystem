using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RainCoder.EA.Common;

namespace Nairc.KpwFramework.TelescopeControler
{
    public class KpwHelper
    {
        public static T DeserializeFile<T>(string fileName) where T : class
        {
            return System.IO.File.ReadAllText(fileName).DeserializeFromXml<T>();
        }
    }
}
