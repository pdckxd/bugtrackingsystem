using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nairc.KpwFramework.DataModel;

namespace DesktopModules.Web
{
    public class Helper
    {
        public static string[] TimeRangeArray
        {
            get
            {
                return new string[]{
                    "00:00-1:00",
                    "00:00-1:00",
                    "1:00-2:00",
                    "2:00-3:00",
                    "3:00-4:00",
                    "4:00-5:00",
                    "5:00-6:00",
                    "18:00-19:00",
                    "19:00-20:00",
                    "20:00-21:00",
                    "21:00-22:00",
                    "22:00-23:00",
                    "23:00-24:00"
                };
            }
        }

        public static string GetStatus(ApplyStatus status)
        {
            switch (status)
            {
                case ApplyStatus.Submitted:
                    return "已申请";
                case ApplyStatus.Approved:
                    return "批准";
                default:
                    return "无效";
            }
        }
    }
}
