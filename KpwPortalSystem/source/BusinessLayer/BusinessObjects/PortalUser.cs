using System;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    /// <summary>
    /// Class that holds information about a PortalUser    
    /// </summary>
    [Serializable]
    public class PortalUser
    {
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}