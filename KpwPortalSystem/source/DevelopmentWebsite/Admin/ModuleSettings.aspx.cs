using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace Admin.Web
{
    public partial class ModuleSettings : Page
    {
        //**********************************************************************************
        //
        // The ModuleSettings.aspx page is used to enable administrators to view/edit/update
        // a portal module's settings (title, output cache properties, edit access)
        //
        //**********************************************************************************


        private int moduleId = 0;
        private int tabId = 0;

        //*******************************************************
        //
        // The Page_Load server event handler on this page is used
        // to populate the module settings on the page
        //
        //*******************************************************

        protected void Page_Load(Object Sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            // Determine Module to Edit
            if (Request.Params["mid"] != null)
            {
                moduleId = Int32.Parse(Request.Params["mid"]);
            }
            // Determine Tab to Edit
            if (Request.Params["tabid"] != null)
            {
                tabId = Int32.Parse(Request.Params["tabid"]);
            }

            if (Page.IsPostBack == false)
            {
                BindData();
            }
        }

        //*******************************************************
        //
        // The ApplyChanges_Click server event handler on this page is used
        // to save the module settings into the portal configuration system
        //
        //*******************************************************

        protected void ApplyChanges_Click(Object Sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            object value = GetModule();
            if (value != null)
            {
                Nairc.KPWPortal.ModuleSettings m = (Nairc.KPWPortal.ModuleSettings) value;

                // Construct Authorized User Roles String
                String editRoles = "";

                foreach (ListItem item in authEditRoles.Items)
                {
                    if (item.Selected == true)
                    {
                        editRoles = editRoles + item.Text + ";";
                    }
                }

                // update module
                Nairc.KPWPortal.Configuration config = new Nairc.KPWPortal.Configuration();
                config.UpdateModule(moduleId, m.ModuleOrder, m.PaneName, moduleTitle.Text, Int32.Parse(cacheTime.Text),
                                    editRoles, showMobile.Checked);

                // Update Textbox Settings
                moduleTitle.Text = m.ModuleTitle;
                cacheTime.Text = m.CacheTime.ToString();

                // Populate checkbox list with all security roles for this portal
                // and "check" the ones already configured for this module
                IAccountFacade facade = new AccountFacade();
                IList<PortalRole> roles = facade.PortalRoles(portalSettings.PortalId);

                // Clear existing items in checkboxlist
                authEditRoles.Items.Clear();

                ListItem allItem = new ListItem();
                allItem.Text = "所有用户";

                if (m.AuthorizedEditRoles.LastIndexOf("All Users") > -1)
                {
                    allItem.Selected = true;
                }

                authEditRoles.Items.Add(allItem);

                foreach(PortalRole role in roles)
                {
                    ListItem item = new ListItem();
                    item.Text = role.RoleName;
                    item.Value = role.RoleID.ToString();

                    if ((m.AuthorizedEditRoles.LastIndexOf(item.Text)) > -1)
                    {
                        item.Selected = true;
                    }

                    authEditRoles.Items.Add(item);
                }
            }

            // Navigate back to admin page
            Response.Redirect("TabLayout.aspx?tabid=" + tabId);
        }

        //*******************************************************
        //
        // The BindData helper method is used to populate a asp:datalist
        // server control with the current "edit access" permissions
        // set within the portal configuration system
        //
        //*******************************************************

        protected void BindData()
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            object value = GetModule();
            if (value != null)
            {
                Nairc.KPWPortal.ModuleSettings m = (Nairc.KPWPortal.ModuleSettings) value;

                // Update Textbox Settings
                moduleTitle.Text = m.ModuleTitle;
                cacheTime.Text = m.CacheTime.ToString();
                showMobile.Checked = m.ShowMobile;

                // Populate checkbox list with all security roles for this portal
                // and "check" the ones already configured for this module            
                IAccountFacade facade = new AccountFacade();
                IList<PortalRole> roles = facade.PortalRoles(portalSettings.PortalId);

                // Clear existing items in checkboxlist
                authEditRoles.Items.Clear();

                ListItem allItem = new ListItem();
                allItem.Text = "所有用户";

                if (m.AuthorizedEditRoles.LastIndexOf("All Users") > -1)
                {
                    allItem.Selected = true;
                }

                authEditRoles.Items.Add(allItem);

                foreach(PortalRole role in roles)
                {
                    ListItem item = new ListItem();
                    item.Text = role.RoleName;
                    item.Value = role.RoleID.ToString();

                    if ((m.AuthorizedEditRoles.LastIndexOf(item.Text)) > -1)
                    {
                        item.Selected = true;
                    }

                    authEditRoles.Items.Add(item);
                }
            }
        }

        protected Nairc.KPWPortal.ModuleSettings GetModule()
        {
            // Obtain PortalSettings for this tab
            PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            // Obtain selected module data
            foreach (Nairc.KPWPortal.ModuleSettings _module in portalSettings.ActiveTab.Modules)
            {
                if (_module.ModuleId == moduleId)
                    return _module;
            }
            return null;
        }
    }
}