using System;
using System.Web.Security;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace Admin.Web
{
    public partial class Register : Page
    {
        //*************************************************************************************
        //
        // The Register.aspx page is used to enable clients to register a new unique username
        // and password with the portal system.  The page contains a single server event
        // handler -- RegisterBtn_Click -- that executes in response to the page's Register
        // Button being clicked.
        //
        // The Register.aspx page uses the UsersDB class to manage the actual account creation.
        // Note that the Usernames and passwords are stored within a table in a SQL database.
        //
        //*************************************************************************************

        protected void RegisterBtn_Click(Object sender, EventArgs E)
        {
            // Only attempt a login if all form fields on the page are valid
            if (Page.IsValid == true)
            {
                IAccountFacade facade = new AccountFacade();
                // Add New User to Portal User Database    
                PortalUser user=new PortalUser();
                user.Name = Name.Text;
                user.Email = Email.Text;
                user.Password = PortalSecurity.Encrypt(Password.Text);
                if (facade.AddUser(user) > -1)
                {
                    // Set the user's authentication name to the userId
                    FormsAuthentication.SetAuthCookie(Email.Text, false);

                    // Redirect browser back to home page
                    Response.Redirect("~/DesktopDefault.aspx");
                }
                else
                {
                    Message.Text = "Registration Failed!  <" + "u" + ">" + Email.Text + "<" + "/u" +
                                   "> is already registered." + "<" + "br" + ">" +
                                   "Please register using a different email address.";
                }
            }
        }
    }
}