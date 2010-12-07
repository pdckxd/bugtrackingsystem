using System;

namespace Nairc.KPWPortal
{
    //*********************************************************************
    //
    // ModuleSettings Class
    //
    // Class that encapsulates the detailed settings for a specific Tab 
    // in the Portal.  ModuleSettings implements the IComparable interface 
    // so that an ArrayList of ModuleSettings objects may be sorted by
    // ModuleOrder, using the ArrayList's Sort() method.
    //
    //*********************************************************************

    public class ModuleSettings : IComparable
    {
        public int ModuleId;
        public int ModuleDefId;
        public int TabId;
        public int CacheTime;
        public int ModuleOrder;
        public String PaneName;
        public String ModuleTitle;
        public String AuthorizedEditRoles;
        public bool ShowMobile;
        public String DesktopSrc;
        public String MobileSrc;


        public int CompareTo(object value)
        {
            if (value == null) return 1;

            int compareOrder = ((ModuleSettings) value).ModuleOrder;

            if (ModuleOrder == compareOrder) return 0;
            if (ModuleOrder < compareOrder) return -1;
            if (ModuleOrder > compareOrder) return 1;
            return 0;
        }
    }
}