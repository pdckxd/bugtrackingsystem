using System;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface IHtmlTextDB
    {
        PortalHtmlText GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails);
    }
}