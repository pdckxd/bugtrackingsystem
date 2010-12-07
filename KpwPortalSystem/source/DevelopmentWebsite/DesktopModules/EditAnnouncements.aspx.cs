using System;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class EditAnnouncements : Page
    {
        private int itemId = 0;
        private int moduleId = 0;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // and ItemId of the announcement to edit.
        //
        // It then uses the Nairc.KPWPortal.AnnouncementsDB() data component
        // to populate the page's edit controls with the annoucement details.
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

            // Determine ItemId of Announcement to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // announcement itemId value is specified, and if so populate page
            // contents with the announcement details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    IDesktopModulesFacade facade = new DesktopModulesFacade();
                    // Obtain a single row of announcement information                
                    PortalAnnouncement announcement = 
                        facade.SingleAnnouncement(itemId);                    

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = announcement.ModuleID;
                    if (dbModuleID != moduleId)
                    {                        
                        Response.Redirect("~/Errors/EditAccessDenied.aspx");
                    }

                    TitleField.Text = announcement.Title;
                    MoreLinkField.Text = announcement.MoreLink;
                    MobileMoreField.Text = announcement.MobileMoreLink;
                    DescriptionField.Text = announcement.Description;
                    ExpireField.Text = announcement.ExpireDate.Value.ToShortDateString();
                    CreatedBy.Text = announcement.CreatedByUser;
                    CreatedDate.Text = announcement.CreatedDate.Value.ToShortDateString();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update an announcement.  It  uses the Nairc.KPWPortal.AnnouncementsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {            
            // Only Update if the Entered Data is Valid
            if (Page.IsValid == true)
            {
                IDesktopModulesFacade facade = new DesktopModulesFacade();
                PortalAnnouncement announcement = new PortalAnnouncement();
                announcement.ModuleID =moduleId;
                announcement.ItemID =itemId;
                announcement.CreatedByUser =Context.User.Identity.Name;
                announcement.Title =TitleField.Text;
                announcement.ExpireDate =DateTime.Parse(ExpireField.Text);
                announcement.Description =DescriptionField.Text;
                announcement.MoreLink =MoreLinkField.Text;
                announcement.MobileMoreLink =MobileMoreField.Text;

                if (itemId == 0)
                {
                    // Add the announcement within the Announcements table
                    facade.AddAnnouncement(announcement);
                }
                else
                {
                    // Update the announcement within the Announcements table
                    facade.UpdateAnnouncement(announcement);
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // an announcement.  It  uses the Nairc.KPWPortal.AnnouncementsDB()
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
                facade.DeleteAnnouncement(itemId);
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