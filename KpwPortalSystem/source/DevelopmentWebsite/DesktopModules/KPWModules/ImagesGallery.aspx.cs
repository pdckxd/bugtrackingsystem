using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace DesktopModules.Web.KPWModules
{
    public partial class ImagesGallery : System.Web.UI.Page
    {
        protected string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageRelativePath"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
