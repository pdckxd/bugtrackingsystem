using System;
using System.IO;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class EditDocs : Page
    {        
        private int itemId = 0;
        private int moduleId = 0;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // and ItemId of the document to edit.
        //
        // It then uses the Nairc.KPWPortal.DocumentDB() data component
        // to populate the page's edit controls with the document details.
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

            // Determine ItemId of Document to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // document itemId value is specified, and if so populate page
            // contents with the document details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    IDesktopModulesFacade facade = new DesktopModulesFacade();
                    // Obtain a single row of document information                
                    PortalDocument doc = facade.SingleDocument(itemId);

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = doc.ModuleID;
                    if (dbModuleID != moduleId)
                    {                        
                        Response.Redirect("~/Errors/EditAccessDenied.aspx");
                    }

                    NameField.Text = doc.FileFriendlyName;
                    PathField.Text = doc.FileNameUrl;
                    CategoryField.Text = doc.Category;
                    CreatedBy.Text = doc.CreatedByUser;
                    CreatedDate.Text = doc.CreatedDate.Value.ToShortDateString();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update an document.  It  uses the Nairc.KPWPortal.DocumentDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Only Update if Input Data is Valid
            if (Page.IsValid == true)
            {
                IDesktopModulesFacade facade = new DesktopModulesFacade();

                PortalDocument doc = new PortalDocument();
                doc.ModuleID=moduleId;
                doc.ItemID =itemId;
                doc.CreatedByUser =Context.User.Identity.Name;
                doc.FileFriendlyName =NameField.Text;
                doc.FileNameUrl =PathField.Text;
                doc.Category=CategoryField.Text;                

                // Determine whether a file was uploaded    
                if ((storeInDatabase.Checked == true) && (FileUpload.PostedFile != null))
                {
                    // for web farm support
                    int length = (int) FileUpload.PostedFile.InputStream.Length;
                    String contentType = FileUpload.PostedFile.ContentType;
                    byte[] content = new byte[length];

                    FileUpload.PostedFile.InputStream.Read(content, 0, length);

                    // Update the document within the Documents table                    
                    doc.Content =content;
                    doc.ContentSize =length;
                    doc.ContentType =contentType;
                    facade.UpdateDocument(doc);
                }
                else
                {
                    if ((Upload.Checked == true) && (FileUpload.PostedFile != null))
                    {
                        // Calculate virtualPath of the newly uploaded file
                        String virtualPath = "~/uploads/" + Path.GetFileName(FileUpload.PostedFile.FileName);

                        // Calculate physical path of the newly uploaded file
                        String phyiscalPath = Server.MapPath(virtualPath);

                        // Save file to uploads directory
                        FileUpload.PostedFile.SaveAs(phyiscalPath);

                        // Update PathFile with uploaded virtual file location
                        PathField.Text = virtualPath;
                    }
                    doc.Content = new byte[0];
                    doc.ContentSize = 0;
                    doc.ContentType = "";
                    facade.UpdateDocument(doc);
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // a document.  It  uses the Nairc.KPWPortal.DocumentsDB()
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
                facade.DeleteDocument(itemId);
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
