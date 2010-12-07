using System;
using System.Collections.Generic;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface IEventsDB
    {
        IList<PortalEvent> GetEvents(int moduleId);
        PortalEvent GetSingleEvent(int itemId);
        void DeleteEvent(int itemID);

        int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                     String description, String wherewhen);

        void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                         String description, String wherewhen);
    }
}