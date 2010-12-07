using System;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class EditEvents : Page
    {

        private int itemId = 0;
        private int moduleId = 0;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // and ItemId of the event to edit.
        //
        // It then uses the Nairc.KPWPortal.EventsDB() data component
        // to populate the page's edit controls with the event details.
        //
        //****************************************************************

        protected void Page_Load(Object Sender, EventArgs e)
        {
            // Determine ModuleId of Events Portal Module
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            // Determine ItemId of Events to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // event itemId value is specified, and if so populate page
            // contents with the event details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    // Obtain a single row of event information                
                    IDesktopModulesFacade facade = new DesktopModulesFacade();
                    PortalEvent ev = facade.SingleEvent(itemId);
                    
                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = ev.ModuleID;
                    if (dbModuleID != moduleId)
                    {                        
                        Response.Redirect("~/Errors/EditAccessDenied.aspx");
                    }

                    TitleField.Text = ev.Title;
                    DescriptionField.Text = ev.Description;
                    ExpireField.Text = ev.ExpireDate.Value.ToShortDateString();
                    CreatedBy.Text = ev.CreatedByUser;
                    WhereWhenField.Text = ev.WhereWhen;
                    CreatedDate.Text = ev.CreatedDate.Value.ToShortDateString();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update an event.  It uses the Nairc.KPWPortal.EventsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Only Update if the Entered Data is Valid
            if (Page.IsValid == true)
            {
                IDesktopModulesFacade facade = new DesktopModulesFacade();
                PortalEvent portalevent = new PortalEvent();
                portalevent.ModuleID =moduleId;
                portalevent.ItemID =itemId;
                portalevent.CreatedByUser =Context.User.Identity.Name;
                portalevent.Title =TitleField.Text;
                portalevent.ExpireDate =DateTime.Parse(ExpireField.Text);
                portalevent.Description =DescriptionField.Text;
                portalevent.WhereWhen =WhereWhenField.Text;

                if (itemId == 0)
                {
                    // Add the event within the Events table
                    facade.AddEvent(portalevent);
                }
                else
                {
                    // Update the event within the Events table
                    facade.UpdateEvent(portalevent);
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // an event.  It  uses the Nairc.KPWPortal.EventsDB() data component to
        // encapsulate all data functionality.
        //
        //****************************************************************

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (itemId != 0)
            {
                IDesktopModulesFacade facade = new DesktopModulesFacade();
                facade.DeleteEvent(itemId);
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
