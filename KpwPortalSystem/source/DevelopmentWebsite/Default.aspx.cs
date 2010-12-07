using System;
using System.Web.UI;

namespace WebApplication
{
    //*************************************************************************
    //
    // The Default.aspx page simply tests the browser type and redirects either to
    // the DesktopDefault or MobileDefault pages, depending on the device type.
    //
    //*************************************************************************

    public partial class Default : Page
    {
        protected void Page_Load(Object sender, EventArgs e)
        {

            if (Request.Browser["IsMobileDevice"] == "true")
            {

                Response.Redirect("MobileDefault.aspx");
            }
            else
            {
                Response.Redirect("DesktopDefault.aspx");
            }    
        }
    }
}
