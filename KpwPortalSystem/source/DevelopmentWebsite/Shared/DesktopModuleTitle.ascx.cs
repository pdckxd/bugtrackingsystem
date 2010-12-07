using System;
using System.Web;
using System.Web.UI;
using Nairc.KPWPortal;

namespace WebApplication
{
    //***********************************************************************************
    //
    // The PortalModuleTitle User Control is responsible for displaying the title of each
    // portal module within the portal -- as well as optionally the module's "Edit Page"
    // (if such a page has been configured).
    //
    //***********************************************************************************

    public partial class DesktopModuleTitle : UserControl
    {
        public String EditText = null;
        public String EditUrl = null;
        public String EditTarget = null;

        protected void Page_Load(Object sender, EventArgs e)
        {

            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings)HttpContext.Current.Items["PortalSettings"];

            // Obtain reference to parent portal module
            PortalModuleControl portalModule = (PortalModuleControl)this.Parent;

            // Display Modular Title Text and Edit Buttons
            ModuleTitle.Text = portalModule.ModuleConfiguration.ModuleTitle;

            // Display the Edit button if the parent portalmodule has configured the PortalModuleTitle User Control
            // to display it -- and the current client has edit access permissions
            if ((portalSettings.AlwaysShowEditButton) || (PortalSecurity.IsInRoles(portalModule.ModuleConfiguration.AuthorizedEditRoles)) && (EditText != null))
            {

                EditButton.Text = EditText;
                EditButton.NavigateUrl = EditUrl + "?mid=" + portalModule.ModuleId;
                EditButton.Target = EditTarget;
            }
        }


    }
}
