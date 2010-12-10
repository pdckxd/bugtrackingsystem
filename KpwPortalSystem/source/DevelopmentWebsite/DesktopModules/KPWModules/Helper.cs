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
                    return "18:00-19:00";
                case 8:
                    return "19:00-20:00";
                case 9:
                    return "20:00-21:00";
                case 10:
                    return "21:00-22:00";
                case 11:
                    return "22:00-23:00";
                default:
                    return "23:00-24:00";
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
