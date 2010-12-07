using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nairc.KpwFramework.DataModel
{
    public class KpwConfiguration
    {
        public static string ImageRelativePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageRelativePath"].ToString();
            }
        }

        public static string ThumbImageRelativePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ThumbImageRelativePath"].ToString();
            }
        }

        public static string BigImageRelativePath
        {
            get
            {
                return ConfigurationManager.AppSettings["BigImageRelativePath"].ToString();
            }
        }

        public static string FitsImageRelativePath
        {
            get
            {
                return ConfigurationManager.AppSettings["FitsImageRelativePath"].ToString();
            }
        }

        public static string KpwCommandFile
        {
            get
            {
                return ConfigurationManager.AppSettings["commandFile"].ToString();
                
            }
        }

        public static string TelescopeEngineIP
        {
            get
            {
                return ConfigurationManager.AppSettings["telescopeEngineIP"].ToString();
            }
        }

        public static string TelescopeEnginPort
        {
            get
            {
                return ConfigurationManager.AppSettings["telescopeEnginPort"].ToString();
            }
        }
    }
}
