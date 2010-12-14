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

        public DateTime TimeFrom
        {
            get
            {
                if (TimeRange < 7)
                    return this.ApplyDate.AddHours(TimeRange - 1);
                else
                    return this.ApplyDate.AddHours(12 + TimeRange -1);
            }
        }

        public DateTime TimeTo
        {
            get
            {
                if (TimeRange < 7)
                    return this.ApplyDate.AddHours(TimeRange);
                else
                    return this.ApplyDate.AddHours(12 + TimeRange);
            }
        }
    }
}
