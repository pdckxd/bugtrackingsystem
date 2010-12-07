using System;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class EditLinks : Page
    {

        private int itemId = 0;
        private int moduleId = 0;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the
        // ItemId of the link to edit.
        //
        // It then uses the Nairc.KPWPortal.LinkDB() data component
        // to populate the page's edit controls with the links details.
        //
        //****************************************************************

        protected void Page_Load(Object Sender, EventArgs e)
        {
            // Determine ModuleId of Links Portal Module
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            // Determine ItemId of Link to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // link itemId value is specified, and if so populate page
            // contents with the link details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    // Obtain a single row of link information       
                    IDesktopModulesFacade facade=new DesktopModulesFacade();
                    PortalLink link = facade.SingleLink(itemId);

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = link.ModuleID;
                    if (dbModuleID != moduleId)
                    {                        
                        Response.Redirect("~/Errors/EditAccessDenied.aspx");
                    }

                    TitleField.Text = link.Title;
                    DescriptionField.Text = link.Description;
                    UrlField.Text = link.Url;
                    MobileUrlField.Text = link.MobileUrl;
                    ViewOrderField.Text = link.ViewOrder.ToString();
                    CreatedBy.Text = link.CreatedByUser;
                    CreatedDate.Text = link.CreatedDate.Value.ToShortDateString();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update a link.  It  uses the Nairc.KPWPortal.LinkDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            if (Page.IsValid == true)
            {
                IDesktopModulesFacade facade = new DesktopModulesFacade();

                PortalLink link = new PortalLink();
                link.ModuleID =moduleId;
                link.ItemID =itemId;
                link.CreatedByUser =Context.User.Identity.Name;
                link.Title =TitleField.Text;
                link.Url =UrlField.Text;
                link.MobileUrl =MobileUrlField.Text;
                link.ViewOrder =Int32.Parse(ViewOrderField.Text);
                link.Description =DescriptionField.Text;

                if (itemId == 0)
                {
                    // Add the link within the Links table
                    facade.AddLink(link);
                }
                else
                {
                    // Update the link within the Links table
                    facade.UpdateLink(link);
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete
        // a link.  It  uses the Nairc.KPWPortal.LinksDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (itemId != 0)
            {
                IDesktopModulesFacade facade = new DesktopModulesFacade();
                facade.DeleteLink(itemId);
            }

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
