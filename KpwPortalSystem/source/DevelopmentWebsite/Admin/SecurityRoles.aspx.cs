using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace Admin.Web
{
    public partial class SecurityRoles : Page
    {
        //*****************************************************************************
        //
        // The SecurityRoles.aspx page is used to create and edit security roles within
        // the Portal application.
        //
        //*****************************************************************************

        private int roleId = -1;
        private String roleName = "";
        private int tabIndex = 0;
        private int tabId = 0;

        //*******************************************************
        //
        // The Page_Load server event handler on this page is used
        // to populate the role information for the page
        //
        //*******************************************************

        protected void Page_Load(Object Sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            // Calculate security roleId
            if (Request.Params["roleid"] != null)
            {
                roleId = Int32.Parse(Request.Params["roleid"]);
            }
            if (Request.Params["rolename"] != null)
            {
                roleName = (String) Request.Params["rolename"];
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
        // The Save_Click server event handler on this page is used
        // to save the current security settings to the configuration system
        //
        //*******************************************************

        protected void Save_Click(Object Sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Navigate back to admin page
            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + tabIndex + "&tabid=" + tabId);
        }

        //*******************************************************
        //
        // The AddUser_Click server event handler is used to add
        // a new user to this security role
        //
        //*******************************************************

        protected void AddUser_Click(Object sender, EventArgs e)
        {
            int userId;
            IAccountFacade facade = new AccountFacade();
            if (((LinkButton) sender).ID == "addNew")
            {
                // add new user to users table            
                PortalUser user = new PortalUser();
                user.Name = windowsUserName.Text;
                user.Email = windowsUserName.Text;
                user.Password = "acme";
                if ((userId = facade.AddUser(user)) == -1)
                {
                    Message.Text = "Add New Failed!  There is already an entry for <" + "u" + ">" + windowsUserName.Text +
                                   "<" + "/u" + "> in the Users database." + "<" + "br" + ">" +
                                   "Please use Add Existing for this user.";
                }
            }
            else
            {
                //get user id from dropdownlist of existing users
                userId = Int32.Parse(allUsers.SelectedItem.Value);
            }

            if (userId != -1)
            {
                // Add a new userRole to the database            
                facade.AddUserRole(roleId, userId);
            }

            // Rebind list
            BindData();
        }

        //*******************************************************
        //
        // The usersInRole_ItemCommand server event handler on this page
        // is used to handle the user editing and deleting roles
        // from the usersInRole asp:datalist control
        //
        //*******************************************************

        protected void usersInRole_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int userId = (int) usersInRole.DataKeys[e.Item.ItemIndex];

            if (e.CommandName == "delete")
            {
                IAccountFacade facade = new AccountFacade();
                // update database
                facade.DeleteUserRole(roleId, userId);

                // Ensure that item is not editable
                usersInRole.EditItemIndex = -1;

                // Repopulate list
                BindData();
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
            // unhide the Windows Authentication UI, if application
            if (User.Identity.AuthenticationType != "Forms")
            {
                windowsUserName.Visible = true;
                addNew.Visible = true;
            }

            // add the role name to the title
            if (roleName != "")
            {
                title.InnerText = "½ÇÉ«¹ØÏµ: " + roleName;
            }

            IAccountFacade facade = new AccountFacade();
            // Get the portal's roles from the database            
            // bind users in role to DataList
            usersInRole.DataSource = facade.RoleMembers(roleId);
            usersInRole.DataBind();

            // bind all portal users to dropdownlist
            allUsers.DataSource = facade.Users();
            allUsers.DataBind();
        }
    }
}