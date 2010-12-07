using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.KpwFramework.DataModel
{
    public enum CommandMessage
    {
        //赤经正向快动
        RaForwardFast = 1,
        //赤经正向慢动
        RaForwardSlow,
        //赤纬正向快动
        RaBackwardFast,
        //赤纬正向慢动
        RaBackwardSlow,
        //赤经反向快动
        DecForwardFast,
        //赤经反向慢动
        DecForwardSlow,
        //赤纬反向快动
        DecBackwardFast,
        //赤纬反向慢动
        DecBackwardSlow,
        //DecStop
        DecStop,
        //RaStop
        RaStop,
        //按赤经赤纬找星
        FindStarByPosition,
        //按星名称找星
        FindStarByName,
        //停止找星
        StopFindStar
    }

    public enum DataMessage
    {
        //望远镜轴的当前指向
        POSI = 1
    }

    public class KpwConstance
    {
        public static readonly string KPW = "KPW:";
        public static readonly string OK = "OK";
    }
}
