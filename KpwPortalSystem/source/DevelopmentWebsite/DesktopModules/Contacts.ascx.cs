using System;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class Contacts : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataReader of contact information from the Contacts
        // table, and then databind the results to a DataGrid
        // server control.  It uses the Nairc.KPWPortal.ContactsDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            // Obtain contact information from Contacts table
            // and bind to the DataGrid Control  
            IDesktopModulesFacade facade = new DesktopModulesFacade();      
            myDataGrid.DataSource = facade.Contacts(ModuleId);
            myDataGrid.DataBind();
        }
    }
}