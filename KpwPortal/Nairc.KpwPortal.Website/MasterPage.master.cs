using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nairc.KpwPortal.Website
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string RenderMenu()
        {
            var result = new StringBuilder();
            RenderMenuItem("主页", "Default.aspx", result);
            RenderMenuItem("望远镜控制台", "KpwControlPage.aspx", result);
            RenderMenuItem("关于", "About.aspx", result);
            return result.ToString();
        }

        void RenderMenuItem(string title, string address, StringBuilder output)
        {
            output.AppendFormat("<li><a href=\"{0}\" ", address);

            var requestUrl = HttpContext.Current.Request.Url;
            if (requestUrl.Segments[requestUrl.Segments.Length - 1].Equals(address, StringComparison.OrdinalIgnoreCase)) // If the requested address is this menu item.
                output.Append("class=\"ActiveMenuButton\"");
            else
                output.Append("class=\"MenuButton\"");

            output.AppendFormat("><span>{0}</span></a></li>|", title);
        }
        
        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

        }
}
}