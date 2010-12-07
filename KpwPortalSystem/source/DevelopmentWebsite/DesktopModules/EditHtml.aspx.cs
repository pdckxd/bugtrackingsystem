using System;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class EditHtml : Page
    {

        private int moduleId = 0;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // of the xml module to edit.
        //
        // It then uses the Nairc.KPWPortal.HtmlTextDB() data component
        // to populate the page's edit controls with the text details.
        //
        //****************************************************************

        protected void Page_Load(Object Sender, EventArgs e)
        {
            // Determine ModuleId of Announcements Portal Module
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            if (Page.IsPostBack == false)
            {
                // Obtain a single row of text information            
                IDesktopModulesFacade facade=new DesktopModulesFacade();
                PortalHtmlText html = facade.HtmlText(moduleId);

                if (html!=null)
                {
                    DesktopText.Text = Server.HtmlDecode(html.DesktopHtml);
                    MobileSummary.Text = Server.HtmlDecode(html.MobileSummary);
                    MobileDetails.Text = Server.HtmlDecode(html.MobileDetails);
                }
                else
                {
                    DesktopText.Text = "Todo: Add Content...";
                    MobileSummary.Text = "Todo: Add Content...";
                    MobileDetails.Text = "Todo: Add Content...";
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to save
        // the text changes to the database.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Update the text within the HtmlText table
            IDesktopModulesFacade facade = new DesktopModulesFacade();

            PortalHtmlText html = new PortalHtmlText();
            html.ModuleID =moduleId;
            html.DesktopHtml = Server.HtmlEncode(DesktopText.Text);
            html.MobileSummary = Server.HtmlEncode(MobileSummary.Text);
            html.MobileDetails = Server.HtmlEncode(MobileDetails.Text);

            facade.UpdateHtmlText(html);

            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }

        //****************************************************************
        //
        // The CancelBtn_Click event handler on this Page is used to cancel
        // out of the page, and return the user back to the portal home
        // page.
        //
        //****************************************************************

        protected void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }

    }
}
