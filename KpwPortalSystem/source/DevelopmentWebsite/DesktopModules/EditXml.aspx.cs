using System;
using System.Collections;
using System.Web.UI;
using Nairc.KPWPortal;

namespace DesktopModules.Web
{
    public partial class EditXml : Page
    {

        private int moduleId = 0;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // xml module to edit.
        //
        // It then uses the ASP.NET configuration system to populate the page's
        // edit controls with the xml details.
        //
        //****************************************************************

        protected void Page_Load(Object Sender, EventArgs e)
        {
            // Determine ModuleId of Announcements Portal Module
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            if (Page.IsPostBack == false)
            {
                if (moduleId > 0)
                {
                    // Get settings from the database
                    Hashtable settings = Nairc.KPWPortal.Configuration.GetModuleSettings(moduleId);

                    XmlDataSrc.Text = (String) settings["xmlsrc"];
                    XslTransformSrc.Text = (String) settings["xslsrc"];
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to save
        // the settings to the configuration file.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Update settings in the database
            Nairc.KPWPortal.Configuration config = new Nairc.KPWPortal.Configuration();

            config.UpdateModuleSetting(moduleId, "xmlsrc", XmlDataSrc.Text);
            config.UpdateModuleSetting(moduleId, "xslsrc", XslTransformSrc.Text);

            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }

        //****************************************************************
        //
        // The CancelBtn_Click event handler on this Page is used to cancel
        // out of the page, and return the user back to the portal home
        // page.
        //
        //****************************************************************

        protected void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }

    }
}
