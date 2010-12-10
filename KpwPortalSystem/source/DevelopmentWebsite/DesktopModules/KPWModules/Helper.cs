using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nairc.KpwFramework.DataModel;

namespace DesktopModules.Web
{
    public class Helper
    {
        public static string GetTimeRange(int i)
        {
            switch (i)
            {
                case 1:
                    return "00:00-1:00";
                case 2:
                    return "1:00-2:00";
                case 3:
                    return "2:00-3:00";
                case 4:
                    return "3:00-4:00";
                case 5:
                    return "4:00-5:00";
                case 6:
                    return "5:00-6:00";
                case 7:
                    return "6:00-7:00";
                case 8:
                    return "7:00-8:00";
                case 9:
                    return "8:00-9:00";
                case 10:
                    return "9:00-10:00";
                case 11:
                    return "10:00-11:00";
                default:
                    return "11:00-12:00";
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
