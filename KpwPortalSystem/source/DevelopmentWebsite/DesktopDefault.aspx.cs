using System;
using System.Web;
using System.Web.UI;
using Nairc.KPWPortal;

namespace WebApplication
{

    //********************************************************************************************
    //
    // The DesktopDefault.aspx page is used to load and populate each Portal View.  It accomplishes
    // this by reading the layout configuration of the portal from the Portal Configuration
    // system, and then using this information to dynamically instantiate portal modules
    // (each implemented as an ASP.NET User Control), and then inject them into the page.
    //
    //********************************************************************************************

    public partial class DesktopDefault : Page
    {

    //*********************************************************************
     //
     // Page_Init Event Handler
     //
     // The Page_Init event handler executes at the very beginning of each page
     // request (immediately before Page_Load).
     //
     // The Page_Init event handler below determines the tab index of the currently
     // requested portal view, and then calls the PopulatePortalSection utility
     // method to dynamically populate the left, center and right hand sections
     // of the portal tab.
     //
     //*********************************************************************
    
     protected void Page_Init(Object sender, EventArgs e) {
    
         // Obtain PortalSettings from Current Context
         PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
    
         // Ensure that the visiting user has access to the current page
         if (PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles) == false) {
             Response.Redirect("~/Errors/AccessDenied.aspx");
         }
    
         // Dynamically inject a signin login module into the top left-hand corner
         // of the home page if the client is not yet authenticated
         if ((Request.IsAuthenticated == false) && (portalSettings.ActiveTab.TabIndex == 0)) {
             LeftPane.Controls.Add(Page.LoadControl("~/DesktopModules/SignIn.ascx"));
             LeftPane.Visible = true;
         }
    
         // Dynamically Populate the Left, Center and Right pane sections of the portal page
         if (portalSettings.ActiveTab.Modules.Count > 0) {
    
             // Loop through each entry in the configuration system for this tab
             foreach (ModuleSettings _moduleSettings in portalSettings.ActiveTab.Modules) 
             {    
                 Control parent = LeftPane;//default

                 switch (_moduleSettings.PaneName)
                 {
                     case "LeftPane":
                         parent = LeftPane;
                         break;
                     case "ContentPane":
                         parent = ContentPane;
                         break;
                     case "RightPane":
                         parent = RightPane;
                         break;
                 }
                 //Control parent = Page.FindControl(_moduleSettings.PaneName);
    
                 // If no caching is specified, create the user control instance and dynamically
                 // inject it into the page.  Otherwise, create a cached module instance that
                 // may or may not optionally inject the module into the tree
    
                 if ((_moduleSettings.CacheTime) == 0) {
    
                    PortalModuleControl portalModule = (PortalModuleControl) Page.LoadControl(_moduleSettings.DesktopSrc);
    
                    portalModule.PortalId = portalSettings.PortalId;
                    portalModule.ModuleConfiguration = _moduleSettings;
    
                    parent.Controls.Add(portalModule);
                 }
                 else {
    
                    CachedPortalModuleControl portalModule = new CachedPortalModuleControl();
    
                    portalModule.PortalId = portalSettings.PortalId;
                    portalModule.ModuleConfiguration = _moduleSettings;
    
                    parent.Controls.Add(portalModule);
                 }
    
                 // Dynamically inject separator break between portal modules
                 parent.Controls.Add(new LiteralControl("<" + "br" + ">"));
                 parent.Visible = true;

                 if (string.IsNullOrEmpty(ContentPane.Height) || int.Parse(ContentPane.Height) < 700)
                 {
                     ContentPane.Height = "580";
                 }    
             }
         }
     }    

    }
}
