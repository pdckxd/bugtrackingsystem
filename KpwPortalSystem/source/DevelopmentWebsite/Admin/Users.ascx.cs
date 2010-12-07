using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace Admin.Web
{
    public partial class Users : PortalModuleControl
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
        // The DeleteUser_Click server event handler is used to add
        // a new security role for this portal
        //
        //*******************************************************

        protected void DeleteUser_Click(Object Sender, ImageClickEventArgs e)
        {
            IAccountFacade facade = new AccountFacade();
            // get user id from dropdownlist of users        
            facade.DeleteUser(Int32.Parse(allUsers.SelectedItem.Value));

            // Rebind list
            BindData();
        }

        //*******************************************************
        //
        // The EditUser_Click server event handler is used to add
        // a new security role for this portal
        //
        //*******************************************************

        protected void EditUser_Click(Object Sender, CommandEventArgs e)
        {
            // get user id from dropdownlist of users
            int userId = -1;
            String _userName = "";

            if (e.CommandName == "edit")
            {
                userId = Int32.Parse(allUsers.SelectedItem.Value);
                _userName = allUsers.SelectedItem.Text;
            }

            // redirect to edit page
            Response.Redirect("~/Admin/ManageUsers.aspx?userId=" + userId + "&username=" + _userName + "&tabindex=" +
                              tabIndex + "&tabid=" + tabId);
        }

        //*******************************************************
        //
        // The BindData helper method is used to bind the list of
        // users for this portal to an asp:DropDownList server control
        //
        //*******************************************************

        private void BindData()
        {
            // change the message between Windows and Forms authentication
            if (Context.User.Identity.AuthenticationType != "Forms")
                Message.Text =
                    "用户必须注册才能查看安全内容。可以通过注册功能添加用户，管理员可以使用以上的安全角色功能指定用户角色。";
            else
                Message.Text =
                    "用户必须注册才能查看安全内容。可以通过注册功能添加用户，管理员可以使用以上的安全角色功能指定用户角色。";

            // Get the list of registered users from the database            
            // bind all portal users to dropdownlist
            IAccountFacade facade = new AccountFacade();
            allUsers.DataSource = facade.Users();
            allUsers.DataBind();
        }
    }
}