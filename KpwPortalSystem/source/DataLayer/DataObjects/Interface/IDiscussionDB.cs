using System;
using System.Collections.Generic;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface IDiscussionDB
    {
        IList<PortalDiscussion> GetTopLevelMessages(int moduleId);
        IList<PortalDiscussion> GetThreadMessages(String parent);
        PortalDiscussion GetSingleMessage(int itemId);
        int AddMessage(int moduleId, int parentId, String userName, String title, String body);
    }
}