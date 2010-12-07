using System;
using System.Collections.Generic;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface IContactsDB
    {
        IList<PortalContact> GetContacts(int moduleId);
        PortalContact GetSingleContact(int itemId);
        void DeleteContact(int itemID);

        int AddContact(int moduleId, int itemId, String userName, String name, String role, String email,
                       String contact1, String contact2);

        void UpdateContact(int moduleId, int itemId, String userName, String name, String role, String email,
                           String contact1, String contact2);
    }
}