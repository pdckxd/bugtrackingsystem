using System;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a PortalRole    
    /// </summary>
    [Serializable]
    public class PortalRole
    {
        public int RoleID { get; set; }

        public int PortalID { get; set; }

        public string RoleName { get; set; }
    }
}