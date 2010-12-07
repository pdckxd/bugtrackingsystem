using System;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class Events : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataReader of event information from the Events
        // table, and then databind the results to a templated DataList
        // server control.  It uses the Nairc.KPWPortal.EventDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            // Obtain the list of events from the Events table
            // and bind to the DataList Control     
            IDesktopModulesFacade facade = new DesktopModulesFacade();   
            myDataList.DataSource = facade.Events(ModuleId);
            myDataList.DataBind();
        }
    }
}