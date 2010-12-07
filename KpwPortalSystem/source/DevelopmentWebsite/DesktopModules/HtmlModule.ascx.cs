using System;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class HtmlModule : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control is
        // used to render a block of HTML or text to the page.
        // The text/HTML to render is stored in the HtmlText
        // database table.  This method uses the Nairc.KPWPortal.HtmlTextDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            // Obtain the selected item from the HtmlText table        
            IDesktopModulesFacade facade = new DesktopModulesFacade();
            PortalHtmlText html = facade.HtmlText(ModuleId);

            if (html != null)
            {
                // Dynamically add the file content into the page
                String content = Server.HtmlDecode(html.DesktopHtml);
                HtmlHolder.Controls.Add(new LiteralControl(content));
            }
        }
    }
}