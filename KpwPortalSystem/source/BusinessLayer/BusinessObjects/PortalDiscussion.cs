using System;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a PortalDiscussion
    /// </summary>
    [Serializable]
    public class PortalDiscussion
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public string Title { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Body { get; set; }

        public string DisplayOrder { get; set; }

        public string CreatedByUser { get; set; }

        public int? NextMessageID { get; set; }

        public int? PrevMessageID { get; set; }

        public string Parent { get; set; }

        public int ChildCount { get; set; }

        public string Indent { get; set; }
    }
}