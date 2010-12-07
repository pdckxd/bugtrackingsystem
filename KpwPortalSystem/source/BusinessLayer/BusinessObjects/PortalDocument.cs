using System;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a PortalDocument
    /// </summary>
    [Serializable]
    public class PortalDocument
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string FileNameUrl { get; set; }

        public string FileFriendlyName { get; set; }

        public string Category { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public int? ContentSize { get; set; }
    }
}