using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace YWXKPortal.Web
{
    public partial class Kpw60ConsolePage : System.Web.UI.Page
    {
        //private Kpw60Console control;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                this.controlDiv.Visible = false;
                this.loginDiv.Visible = true;
            }
            else
            {
                this.AddKpwControl();
                this.controlDiv.Visible = true;
                this.loginDiv.Visible = false;
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

            bool authenticated = false;
            string adminPsw = ConfigurationManager.AppSettings["AdminPassword"].ToString();

            if (this.Login1.UserName.ToLower() == "guest")
            {
                Session["User"] = "guest";
                authenticated = true;
            }
            else if (this.Login1.UserName.ToLower() == "admin" && this.Login1.Password == adminPsw)
            {
                Session["User"] = "admin";
                authenticated = true; 
            }

            if (authenticated)
            {
                this.AddKpwControl();
                this.controlDiv.Visible = true;
                this.loginDiv.Visible = false;
            }
        }

        private void AddKpwControl()
        {
            Control control = Page.LoadControl("Kpw60Console.ascx");
            control.ID = "kpwControl";
            this.controlDiv.Controls.Add(control);
        }
    }
}
