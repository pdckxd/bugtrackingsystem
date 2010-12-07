using System;
using System.Collections.Generic;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface IUsersDB
    {
        int AddUser(String fullName, String email, String password);
        void DeleteUser(int userId);
        void UpdateUser(int userId, String email, String password);
        IList<PortalRole> GetRolesByUser(String email);
        PortalUser GetSingleUser(String email);
        String[] GetRoles(String email);
        String Login(String email, String password);
    }
}