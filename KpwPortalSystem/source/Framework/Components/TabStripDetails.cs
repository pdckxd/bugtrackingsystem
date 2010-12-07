using System;

namespace Nairc.KPWPortal
{
    //*********************************************************************
    //
    // TabStripDetails Class
    //
    // Class that encapsulates the just the tabstrip details -- TabName, TabId and TabOrder 
    // -- for a specific Tab in the Portal
    //
    //*********************************************************************

    public class TabStripDetails
    {
        public int TabId;
        public String TabName;
        public int TabOrder;
        public String AuthorizedRoles;
    }
}