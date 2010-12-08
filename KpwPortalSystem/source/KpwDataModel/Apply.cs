using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.KpwFramework.DataModel
{
    public enum ApplyStatus
    {
        Submitted = 1,
        Approved,
        Deactivated
    }

    public class Apply
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public DateTime ApplyDate { get; set; }
        public int TimeRange { get; set; }
        public ApplyStatus ApplyStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
