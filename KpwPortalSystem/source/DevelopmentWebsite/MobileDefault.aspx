<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.UI.MobileControls.MobilePage" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<%@ Register TagPrefix="ASPNETPortal" Namespace="Nairc.KPWPortal.MobileControls" Assembly="ASPNETPortal" %>
<%@ import Namespace="System.Data" %>
<%@ import Namespace="System.Data.SqlClient" %>
<%@ import Namespace="Nairc.KPWPortal" %>
<script runat="server">

    ArrayList authorizedTabs = new ArrayList();
    
    //*********************************************************************
    //
    // Page_Init Event Handler
    //
    // The Page_Init event handler executes at the very beginning of each page
    // request (immediately before Page_Load).
    //
    // The Page_Init event handler calls the PopulateTabs utility method
    // to insert empty tabs into the tab view. It then determines the tab
    // index of the currently requested portal, and then calls the
    // PopulateTabView utility method to dynamically populate the
    // active portal view.
    //
    //*********************************************************************
    
    void Page_Init(Object sender, EventArgs e) {
    
        int tabIndex = 0;
        int tabID = 1;
    
        // Obtain current tab index and tab id settings
        String tabSetting = (String)HiddenVariables["ti"];
    
        if (tabSetting != null) {
    
            int comma = tabSetting.IndexOf(',');
            tabIndex = Int32.Parse(tabSetting.Substring(0, comma));
            tabID = Int32.Parse(tabSetting.Substring(comma + 1));
        }
    
        // Obtain PortalSettings from Current Context
        LoadPortalSettings(tabIndex, tabID);
    
        // Populate tab list with empty tabs
        PopulateTabStrip();
    
        // Populate the current tab view
        PopulateTabView(tabIndex);
    }
    
    //*********************************************************************
    //
    // PopulateTabStrip method
    //
    // The PopulateTabStrip method is used to dynamically create and add
    // tabs for each tab view defined in the portal configuration.
    //
    //*********************************************************************
    
    void PopulateTabStrip() {
    
        // Obtain PortalSettings from Current Context
        PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
    
        for (int i=0;i < portalSettings.MobileTabs.Count; i++) {
    
            // Create a MobilePortalTab control for the tab,
            // and add it to the tab view.
    
            TabStripDetails tab = (TabStripDetails)portalSettings.MobileTabs[i];
    
            if (PortalSecurity.IsInRoles(tab.AuthorizedRoles)) {
    
                MobilePortalTab tabPanel = new MobilePortalTab();
                tabPanel.Title = tab.TabName;
    
                TabView.Panes.Add(tabPanel);
            }
        }
    }
    
    //*********************************************************************
    //
    // PopulateTabView method
    //
    // The PopulateTabView method dynamically populates a portal tab
    // with each module defined in the portal configuration.
    //
    //*********************************************************************
    
    void PopulateTabView(int tabIndex) {
    
        // Obtain PortalSettings from Current Context
        PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
    
        // Ensure that the visiting user has access to the current page
        if (PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles) == false) {
            Response.Redirect("~/Errors/MobileAccessDenied.aspx");
        }
    
        // Obtain reference to container mobile tab
        MobilePortalTab view = (MobilePortalTab) TabView.Panes[tabIndex];
    
        // Dynamically populate the view
        if (portalSettings.ActiveTab.Modules.Count > 0) {
    
            // Loop through each entry in the configuration system for this tab
            foreach (ModuleSettings moduleSettings in portalSettings.ActiveTab.Modules) {
    
                // Only add the module if it support Mobile devices
                if (moduleSettings.ShowMobile) {
    
                    MobilePortalModuleControl moduleControl = (MobilePortalModuleControl) Page.LoadControl(moduleSettings.MobileSrc);
                    moduleControl.ModuleConfiguration = moduleSettings;
    
                    view.Panes.Add(moduleControl);
                }
            }
        }
    }
    
    //*********************************************************************
    //
    // TabView_OnActivate Event Handler
    //
    // The TabView_OnActivate event handler executes when the user switches
    // tabs in the tab view. It calls the PopulateTabView utility
    // method to dynamically populate the newly activated view.
    //
    //*********************************************************************
    
    void TabView_OnTabActivate(Object sender, EventArgs e) {
    
        // Obtain PortalSettings from Current Context
        PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
    
        int tabIndex = TabView.ActivePaneIndex;
        int tabID = ((TabStripDetails) portalSettings.MobileTabs[tabIndex]).TabId;
    
        // Store tabindex in a hidden variable to preserve accross round trips
        if (tabIndex != 0) {
            HiddenVariables["ti"] = String.Concat(tabIndex.ToString(), ",", tabID.ToString());
        }
        else {
            HiddenVariables.Remove("ti");
        }
    
        // Check to see if portal settings need reloading
        LoadPortalSettings(tabIndex, tabID);
    
        // Populate the newly active tab.
        PopulateTabView(tabIndex);
    
        // Set the view to summary mode, where a summary of all the modules are shown.
        ((MobilePortalTab)TabView.ActivePane).SummaryView = true;
    }
    
    //*********************************************************************
    //
    // LoadPortalSettings method
    //
    // LoadPortalSettings is a helper methods that loads portal settings for
    // the selected tab.  It first verifies that the settings haven't already
    // been set within the Global.asax file -- if they are different (in the
    // case that a tab change is made) then the method reloads the appropriate
    // tab data.
    //
    //*********************************************************************
    
    void LoadPortalSettings(int tabIndex, int tabId) {
    
        // Obtain PortalSettings from Current Context
        PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
    
        if ((portalSettings.ActiveTab.TabId != tabId) || (portalSettings.ActiveTab.TabIndex != tabIndex)) {
    
            HttpContext.Current.Items["PortalSettings"] = new PortalSettings(tabIndex, tabId);
        }
    }

    //***************************************************************************************************
    //
    // The MobileDefault.aspx page is used to load and populate each Mobile Portal View.  It accomplishes
    // this by reading the layout configuration of the portal from the Portal Configuration
    // system. At the top level is a tab view, implemented using a TabbedPanel custom control.
    // Each portal view is inserted into this control, and portal modules (each implemented
    // as an ASP.NET user control) are instantiated and inserted into tabs.
    //
    //***************************************************************************************************



</script>

<mobile:Form runat="server" Wrapping="NoWrap" Paginate="true" PagerStyle-Font-Name="Verdana" PagerStyle-ForeColor="#ffffff" PagerStyle-Font-Size="Small">
    <DEVICESPECIFIC>
        <CHOICE filter="isJScript" backcolor="#000000">
            <HEADERTEMPLATE>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <img height="45" src="data/mobilelogo.gif" width="180" /> 
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table height="270" cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffff" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <img height="220" src="images/spacer.gif" width="2" /> 
                            </td>
                            <td valign="top">
            </HEADERTEMPLATE>
            <FOOTERTEMPLATE>
                </td>
                <td>
                    <img height="220" src="images/spacer.gif" width="2" /> 
                </td>
                </tr>
                </tbody>
                </table>
            </FOOTERTEMPLATE>
        </CHOICE>
        <CHOICE>
            <HEADERTEMPLATE>
                <mobile:Label id="Label1" runat="server" StyleReference="title">
                    深圳市天文观测系统</mobile:Label>
            </HEADERTEMPLATE>
        </CHOICE>
    </DEVICESPECIFIC>
    <ASPNETPORTAL:TABBEDPANEL id="TabView" runat="server" OnTabActivate="TabView_OnTabActivate" TabColor="#bbbb9a" TabTextColor="#000000" ActiveTabColor="#000000" ActiveTabTextColor="#ffffff" />
</mobile:Form>
