using System;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a PortalHtmlText
    /// </summary>
    [Serializable]
    public class PortalHtmlText
    {
        public int ModuleID { get; set; }

        public string DesktopHtml { get; set; }

        public string MobileSummary { get; set; }

        public string MobileDetails { get; set; }
    }
}