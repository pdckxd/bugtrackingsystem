using System;
using Nairc.KPWPortal;

namespace Admin.Web
{
    public partial class SiteSettings : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load server event handler on this user control is used
        // to populate the current site settings from the config system
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            // If this is the first visit to the page, populate the site data
            if (Page.IsPostBack == false)
            {
                // Obtain PortalSettings from Current Context
                PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

                siteName.Text = portalSettings.PortalName;
                showEdit.Checked = portalSettings.AlwaysShowEditButton;
            }
        }

        //*******************************************************
        //
        // The Apply_Click server event handler is used
        // to update the Site Name within the Portal Config System
        //
        //*******************************************************

        protected void Apply_Click(Object sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // update Tab info in the database
            Nairc.KPWPortal.Configuration config = new Nairc.KPWPortal.Configuration();
            config.UpdatePortalInfo(portalSettings.PortalId, siteName.Text, showEdit.Checked);

            // Redirect to this site to refresh
            Response.Redirect(Request.RawUrl);
        }
    }
}