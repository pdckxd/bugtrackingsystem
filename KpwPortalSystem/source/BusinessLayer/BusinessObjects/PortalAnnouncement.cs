using System;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a PortalAnnouncement
    /// </summary>
    [Serializable]
    public class PortalAnnouncement
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Title { get; set; }

        public string MoreLink { get; set; }

        public string MobileMoreLink { get; set; }

        public DateTime? ExpireDate { get; set; }

        public string Description { get; set; }
    }
}