using System;
using System.Web.UI;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class ViewDocument : Page
    {
        private int documentId = -1;

        //*******************************************************
        //
        // The Page_Load event handler on this Page is used to
        // obtain obtain the contents of a document from the
        // Documents table, construct an HTTP Response of the
        // correct type for the document, and then stream the
        // document contents to the response.  It uses the
        // Nairc.KPWPortal.DocumentDB() data component to encapsulate
        // the data access functionality.
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (Request.Params["DocumentId"] != null)
            {
                documentId = Int32.Parse(Request.Params["DocumentId"]);
            }

            if (documentId != -1)
            {
                // Obtain Document Data from Documents table                
                IDesktopModulesFacade facade=new DesktopModulesFacade();
                PortalDocument dBContent = facade.DocumentContent(documentId);                

                // Serve up the file by name
                Response.AppendHeader("content-disposition", "filename=" + dBContent.FileFriendlyName);

                // set the content type for the Response to that of the
                // document to display.  For example. "application/msword"
                Response.ContentType = dBContent.ContentType;

                // output the actual document contents to the response output stream
                Response.OutputStream.Write(dBContent.Content, 0, dBContent.ContentSize.Value);

                // end the response
                Response.End();
            }
        }
    }
}