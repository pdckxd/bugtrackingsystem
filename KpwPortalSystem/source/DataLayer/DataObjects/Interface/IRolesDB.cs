using System;
using System.Collections.Generic;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface IRolesDB
    {
        IList<PortalRole> GetPortalRoles(int portalId);
        int AddRole(int portalId, String roleName);
        void DeleteRole(int roleId);
        void UpdateRole(int roleId, String roleName);
        IList<PortalUser> GetRoleMembers(int roleId);
        void AddUserRole(int roleId, int userId);
        void DeleteUserRole(int roleId, int userId);
        IList<PortalUser> GetUsers();
    }
}