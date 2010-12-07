using System;
using System.Web.Security;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    //*************************************************************************
    //
    // The SignIn User Control enables clients to authenticate themselves using
    // the ASP.NET Forms based authentication system.
    //
    // When a client enters their username/password within the appropriate
    // textboxes and clicks the "Login" button, the LoginBtn_Click event
    // handler executes on the server and attempts to validate their
    // credentials against a SQL database.
    //
    // If the password check succeeds, then the LoginBtn_Click event handler
    // sets the customers username in an encrypted cookieID and redirects
    // back to the portal home page.
    //
    // If the password check fails, then an appropriate error message
    // is displayed.
    //
    //*************************************************************************

    public partial class Signin : PortalModuleControl
    {
        protected void LoginBtn_Click(Object sender, ImageClickEventArgs e)
        {
            // Attempt to Validate User Credentials using UsersDB   
            IAccountFacade facade = new AccountFacade();
            String userId = facade.Login(email.Text, PortalSecurity.Encrypt(password.Text));

            if ((userId != null) && (userId != ""))
            {
                // Use security system to set the UserID within a client-side Cookie
                FormsAuthentication.SetAuthCookie(email.Text, RememberCheckbox.Checked);

                // Redirect browser back to originating page
                Response.Redirect(Global.GetApplicationPath(Request));
            }
            else
            {
                Message.Text = "<" + "br" + ">Login Failed!" + "<" + "br" + ">";
            }
        }
    }
}