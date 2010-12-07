using System;
using System.Collections;

namespace Nairc.KPWPortal
{
    //*********************************************************************
    //
    // TabSettings Class
    //
    // Class that encapsulates the detailed settings for a specific Tab 
    // in the Portal
    //
    //*********************************************************************

    public class TabSettings
    {
        public int TabIndex;
        public int TabId;
        public String TabName;
        public int TabOrder;
        public String MobileTabName;
        public String AuthorizedRoles;
        public bool ShowMobile;
        public ArrayList Modules = new ArrayList();
    }
}