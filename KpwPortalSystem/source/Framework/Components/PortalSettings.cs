using System;
using System.Collections;
using Nairc.KPWPortal.Components;

namespace Nairc.KPWPortal
{
    //*********************************************************************
    //
    // PortalSettings Class
    //
    // This class encapsulates all of the settings for the Portal, as well
    // as the configuration settings required to execute the current tab
    // view within the portal.
    //
    //*********************************************************************

    public class PortalSettings
    {
        public int PortalId;
        public String PortalName;
        public bool AlwaysShowEditButton;
        public ArrayList DesktopTabs = new ArrayList();
        public ArrayList MobileTabs = new ArrayList();
        public TabSettings ActiveTab = new TabSettings();

        //*********************************************************************
        //
        // PortalSettings Constructor
        //
        // The PortalSettings Constructor encapsulates all of the logic
        // necessary to obtain configuration settings necessary to render
        // a Portal Tab view for a given request.
        //
        // These Portal Settings are stored within PortalCFG.xml, and are
        // fetched below by calling config.GetSiteSettings().
        // The method config.GetSiteSettings() fills the SiteConfiguration
        // class, derived from a DataSet, which PortalSettings accesses.
        //       
        //*********************************************************************

        public PortalSettings(int tabIndex, int tabId)
        {
            // Get the configuration data
            SiteConfiguration siteSettings = Configuration.GetSiteSettings();

            // Read the Desktop Tab Information, and sort by Tab Order
            foreach (SiteConfiguration.TabRow tRow in siteSettings.Tab.Select("", "TabOrder"))
            {
                TabStripDetails tabDetails = new TabStripDetails();

                tabDetails.TabId = tRow.TabId;
                tabDetails.TabName = tRow.TabName;
                tabDetails.TabOrder = tRow.TabOrder;
                tabDetails.AuthorizedRoles = tRow.AccessRoles;

                DesktopTabs.Add(tabDetails);
            }

            // If the PortalSettings.ActiveTab property is set to 0, change it to  
            // the TabID of the first tab in the DesktopTabs collection
            if (ActiveTab.TabId == 0)
                ActiveTab.TabId = ((TabStripDetails) DesktopTabs[0]).TabId;


            // Read the Mobile Tab Information, and sort by Tab Order
            foreach (SiteConfiguration.TabRow mRow in siteSettings.Tab.Select("ShowMobile='true'", "TabOrder"))
            {
                TabStripDetails tabDetails = new TabStripDetails();

                tabDetails.TabId = mRow.TabId;
                tabDetails.TabName = mRow.MobileTabName;
                tabDetails.AuthorizedRoles = mRow.AccessRoles;

                MobileTabs.Add(tabDetails);
            }

            // Read the Module Information for the current (Active) tab
            SiteConfiguration.TabRow activeTab = siteSettings.Tab.FindByTabId(tabId);

            // Get Modules for this Tab based on the Data Relation
            foreach (SiteConfiguration.ModuleRow moduleRow in activeTab.GetModuleRows())
            {
                ModuleSettings moduleSettings = new ModuleSettings();

                moduleSettings.ModuleTitle = moduleRow.ModuleTitle;
                moduleSettings.ModuleId = moduleRow.ModuleId;
                moduleSettings.ModuleDefId = moduleRow.ModuleDefId;
                moduleSettings.ModuleOrder = moduleRow.ModuleOrder;
                moduleSettings.TabId = tabId;
                moduleSettings.PaneName = moduleRow.PaneName;
                moduleSettings.AuthorizedEditRoles = moduleRow.EditRoles;
                moduleSettings.CacheTime = moduleRow.CacheTimeout;
                moduleSettings.ShowMobile = moduleRow.ShowMobile;

                // ModuleDefinition data
                SiteConfiguration.ModuleDefinitionRow modDefRow =
                    siteSettings.ModuleDefinition.FindByModuleDefId(moduleSettings.ModuleDefId);

                moduleSettings.DesktopSrc = modDefRow.DesktopSourceFile;
                moduleSettings.MobileSrc = modDefRow.MobileSourceFile;

                ActiveTab.Modules.Add(moduleSettings);
            }

            // Sort the modules in order of ModuleOrder
            ActiveTab.Modules.Sort();

            // Get the first row in the Global table
            SiteConfiguration.GlobalRow globalSettings = (SiteConfiguration.GlobalRow) siteSettings.Global.Rows[0];

            // Read Portal global settings 
            PortalId = globalSettings.PortalId;
            PortalName = globalSettings.PortalName;
            AlwaysShowEditButton = globalSettings.AlwaysShowEditButton;
            ActiveTab.TabIndex = tabIndex;
            ActiveTab.TabId = tabId;
            ActiveTab.TabOrder = activeTab.TabOrder;
            ActiveTab.MobileTabName = activeTab.MobileTabName;
            ActiveTab.AuthorizedRoles = activeTab.AccessRoles;
            ActiveTab.TabName = activeTab.TabName;
            ActiveTab.ShowMobile = activeTab.ShowMobile;
        }
    }
}