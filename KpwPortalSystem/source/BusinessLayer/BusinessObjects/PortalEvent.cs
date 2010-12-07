using System;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a PortalEvent
    [Serializable]
    public class PortalEvent
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Title { get; set; }

        public string WhereWhen { get; set; }

        public string Description { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}