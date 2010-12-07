using System;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class Announcements : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataSet of announcement information from the Announcements
        // table, and then databind the results to a templated DataList
        // server control.  It uses the Nairc.KPWPortal.AnnouncementsDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            // Obtain announcement information from Announcements table
            // and bind to the datalist control            
            // DataBind Announcements to DataList Control
            IDesktopModulesFacade facade = new DesktopModulesFacade();
            myDataList.DataSource = facade.Announcements(ModuleId);
            myDataList.DataBind();
        }
    }
}