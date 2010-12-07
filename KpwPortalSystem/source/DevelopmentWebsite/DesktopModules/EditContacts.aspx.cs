using System;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class EditContacts : Page
    {
        private int itemId = 0;
        private int moduleId = 0;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // and ItemId of the contact to edit.
        //
        // It then uses the Nairc.KPWPortal.ContactsDB() data component
        // to populate the page's edit controls with the contact details.
        //
        //****************************************************************

        protected void Page_Load(Object Sender, EventArgs e)
        {
            // Determine ModuleId of Contacts Portal Module
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            // Determine ItemId of Contacts to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // contact itemId value is specified, and if so populate page
            // contents with the contact details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    // Obtain a single row of contact information
                    IDesktopModulesFacade facade = new DesktopModulesFacade();
                    PortalContact contact = facade.SingleContact(itemId);

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = contact.ModuleID;
                    if (dbModuleID != moduleId)
                    {                        
                        Response.Redirect("~/Errors/EditAccessDenied.aspx");
                    }

                    NameField.Text = contact.Name;
                    RoleField.Text = contact.Role;
                    EmailField.Text = contact.Email;
                    Contact1Field.Text = contact.Contact1;
                    Contact2Field.Text = contact.Contact2;
                    CreatedBy.Text = contact.CreatedByUser;
                    CreatedDate.Text = contact.CreatedDate.Value.ToShortDateString();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update a contact.  It  uses the Nairc.KPWPortal.ContactsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Only Update if Entered data is Valid
            if (Page.IsValid == true)
            {
                IDesktopModulesFacade facade = new DesktopModulesFacade();
                PortalContact contact = new PortalContact();
                contact.ModuleID =moduleId;
                contact.ItemID =itemId;
                contact.CreatedByUser =Context.User.Identity.Name;
                contact.Name =NameField.Text;
                contact.Role =RoleField.Text;
                contact.Email =EmailField.Text;
                contact.Contact1 = Contact1Field.Text;
                contact.Contact2 = Contact2Field.Text;

                if (itemId == 0)
                {
                    // Add the contact within the contacts table
                    facade.AddContact(contact);
                }
                else
                {
                    // Update the contact within the contacts table
                    facade.UpdateContact(contact);
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // a contact.  It  uses the Nairc.KPWPortal.ContactsDB()
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
                facade.DeleteContact(itemId);
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