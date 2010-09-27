using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.BugTrackingSystem.BusinessObject
{
    public class User
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
