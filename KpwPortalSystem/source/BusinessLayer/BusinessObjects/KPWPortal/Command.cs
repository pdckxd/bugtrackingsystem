using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
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
        //Stop
        Stop
    }
}
