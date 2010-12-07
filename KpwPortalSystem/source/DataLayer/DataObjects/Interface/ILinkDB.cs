using System;
using System.Collections.Generic;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface ILinkDB
    {
        IList<PortalLink> GetLinks(int moduleId);
        PortalLink GetSingleLink(int itemId);
        void DeleteLink(int itemID);

        int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                    int viewOrder, String description);

        void UpdateLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                        int viewOrder, String description);
    }
}