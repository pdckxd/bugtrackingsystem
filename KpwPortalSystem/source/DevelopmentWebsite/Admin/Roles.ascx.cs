using System;
using System.Web.UI.WebControls;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace Admin.Web
{
    public partial class Roles : PortalModuleControl
    {
        private int tabIndex = 0;
        private int tabId = 0;

        //*******************************************************
        //
        // The Page_Load server event handler on this user control is used
        // to populate the current roles settings from the configuration system
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            if (Request.Params["tabid"] != null)
            {
                tabId = Int32.Parse(Request.Params["tabid"]);
            }
            if (Request.Params["tabindex"] != null)
            {
                tabIndex = Int32.Parse(Request.Params["tabindex"]);
            }

            // If this is the first visit to the page, bind the role data to the datalist
            if (Page.IsPostBack == false)
            {
                BindData();
            }
        }

        //*******************************************************
        //
        // The AddRole_Click server event handler is used to add
        // a new security role for this portal
        //
        //*******************************************************

        protected void AddRole_Click(Object Sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Add a new role to the database        
            IAccountFacade facade = new AccountFacade();
            PortalRole role = new PortalRole();
            role.PortalID = portalSettings.PortalId;
            role.RoleName = "ÐÂ½ÇÉ«";
            facade.AddRole(role);

            // set the edit item index to the last item
            rolesList.EditItemIndex = rolesList.Items.Count;

            // Rebind list
            BindData();
        }

        //*******************************************************
        //
        // The RolesList_ItemCommand server event handler on this page
        // is used to handle the user editing and deleting roles
        // from the RolesList asp:datalist control
        //
        //*******************************************************

        protected void RolesList_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int roleId = (int) rolesList.DataKeys[e.Item.ItemIndex];
            IAccountFacade facade = new AccountFacade();

            if (e.CommandName == "edit")
            {
                // Set editable list item index if "edit" button clicked next to the item
                rolesList.EditItemIndex = e.Item.ItemIndex;

                // Repopulate the datalist control
                BindData();
            }
            else if (e.CommandName == "apply")
            {
                // Apply changes
                String _roleName = ((TextBox) e.Item.FindControl("roleName")).Text;

                // update database
                PortalRole role = new PortalRole();
                role.RoleID = roleId;
                role.RoleName = _roleName;
                facade.UpdateRole(role);

                // Disable editable list item access
                rolesList.EditItemIndex = -1;

                // Repopulate the datalist control
                BindData();
            }
            else if (e.CommandName == "delete")
            {
                // update database
                facade.DeleteRole(roleId);

                // Ensure that item is not editable
                rolesList.EditItemIndex = -1;

                // Repopulate list
                BindData();
            }
            else if (e.CommandName == "members")
            {
                // Save role name changes first
                String _roleName = ((TextBox) e.Item.FindControl("roleName")).Text;
                PortalRole role = new PortalRole();
                role.RoleID = roleId;
                role.RoleName = _roleName;
                facade.UpdateRole(role);

                // redirect to edit page
                Response.Redirect("~/Admin/SecurityRoles.aspx?roleId=" + roleId + "&rolename=" + _roleName +
                                  "&tabindex=" + tabIndex + "&tabid=" + tabId);
            }
        }

        //*******************************************************
        //
        // The BindData helper method is used to bind the list of
        // security roles for this portal to an asp:datalist server control
        //
        //*******************************************************

        private void BindData()
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Get the portal's roles from the database
            IAccountFacade facade = new AccountFacade();
            rolesList.DataSource = facade.PortalRoles(portalSettings.PortalId);
            rolesList.DataBind();
        }
    }
}