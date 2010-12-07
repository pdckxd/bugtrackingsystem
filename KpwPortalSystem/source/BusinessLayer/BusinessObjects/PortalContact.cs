using System;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a PortalContact
    /// </summary>
    [Serializable]
    public class PortalContact
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Contact1 { get; set; }

        public string Contact2 { get; set; }
    }
}