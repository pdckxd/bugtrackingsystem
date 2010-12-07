using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace Admin.Web
{
    //*****************************************************************************
    //
    // The SecurityRoles.aspx page is used to create and edit security roles within
    // the 深圳市天文观测系统 application.
    //
    //*****************************************************************************

    public partial class ManageUsers : Page
    {
        private int userId = -1;
        private String userName = "";
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

            // Calculate userid
            if (Request.Params["userid"] != null)
            {
                userId = Int32.Parse(Request.Params["userid"]);
            }
            if (Request.Params["username"] != null)
            {
                userName = (String) Request.Params["username"];
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
                // new user?
                if (userName == "")
                {
                    // make a unique new user record
                    int uid = -1;
                    int i = 0;

                    while (uid == -1)
                    {
                        String friendlyName = "New User created " + DateTime.Now;
                        userName = "New User" + i;
                        IAccountFacade facade = new AccountFacade();
                        PortalUser user = new PortalUser();
                        user.Name = friendlyName;
                        user.Email = userName;
                        user.Password = "";
                        uid = facade.AddUser(user);
                        i++;
                    }

                    // redirect to this page with the corrected querystring args
                    Response.Redirect("~/Admin/ManageUsers.aspx?userId=" + uid + "&username=" + userName + "&tabindex=" +
                                      tabIndex + "&tabid=" + tabId);
                }

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
        // The AddRole_Click server event handler is used to add
        // the user to this security role
        //
        //*******************************************************

        protected void AddRole_Click(Object sender, EventArgs e)
        {
            int roleId;

            //get user id from dropdownlist of existing users
            roleId = Int32.Parse(allRoles.SelectedItem.Value);

            // Add a new userRole to the database             
            IAccountFacade facade = new AccountFacade();
            facade.AddUserRole(roleId, userId);

            // Rebind list
            BindData();
        }

        //*******************************************************
        //
        // The UpdateUser_Click server event handler is used to add
        // the update the user settings
        //
        //*******************************************************

        protected void UpdateUser_Click(Object sender, EventArgs e)
        {
            // update the user record in the database             
            IAccountFacade facade = new AccountFacade();
            PortalUser user = new PortalUser();
            user.UserID = userId;
            user.Email = Email.Text;
            user.Password = Password.Text;
            facade.UpdateUser(user);

            // redirect to this page with the corrected querystring args
            Response.Redirect("~/Admin/ManageUsers.aspx?userId=" + userId + "&username=" + Email.Text + "&tabindex=" +
                              tabIndex + "&tabid=" + tabId);
        }

        //*******************************************************
        //
        // The UserRoles_ItemCommand server event handler on this page
        // is used to handle deleting the user from roles
        // from the userRoles asp:datalist control
        //
        //*******************************************************

        protected void UserRoles_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            int roleId = (int) userRoles.DataKeys[e.Item.ItemIndex];

            // update database
            IAccountFacade facade = new AccountFacade();
            facade.DeleteUserRole(roleId, userId);

            // Ensure that item is not editable
            userRoles.EditItemIndex = -1;

            // Repopulate list
            BindData();
        }

        //*******************************************************
        //
        // The BindData helper method is used to bind the list of
        // security roles for this portal to an asp:datalist server control
        //
        //*******************************************************

        private void BindData()
        {
            // Bind the Email and Password     
            IAccountFacade facade = new AccountFacade();            
            PortalUser user = facade.SingleUser(userName);            

            Email.Text = user.Email;            

            // add the user name to the title
            if (userName != "")
            {
                title.InnerText = "Manage User: " + userName;
            }

            // bind users in role to DataList
            userRoles.DataSource = facade.RolesByUser(userName);
            userRoles.DataBind();

            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // Get the portal's roles from the database                 
            // bind all portal roles to dropdownlist
            allRoles.DataSource = facade.PortalRoles(portalSettings.PortalId);
            allRoles.DataBind();
        }
    }
}