using System;
using System.Collections.Generic;
using System.ComponentModel;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.BusinessLayer.Facade
{
    public interface IAccountFacade
    {
        /// <summary>
        /// a list of roles for the user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalRole> RolesByUser(string email);

        /// <summary>
        /// details about a specific user 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalUser SingleUser(string email);

        /// <summary>
        /// a list of all role names for the specified portal
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalRole> PortalRoles(int portalId);

        /// <summary>
        /// a list of all members in the specified security role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalUser> RoleMembers(int roleId);

        /// <summary>
        /// the UserID, Name and Email for all registered users
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalUser> Users();

        /// <summary>
        /// inserts a new user record into the "Users" database table
        /// </summary>
        /// <returns>userid of the user newlly created user</returns>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddUser(PortalUser user);

        /// <summary>
        /// deleted a  user record from the "Users" database table.
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        void DeleteUser(int userId);

        /// <summary>
        /// updates a  user record from the "Users" database table.
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateUser(PortalUser user);

        /// <summary>
        /// a list of role names for the user
        /// </summary>        
        [DataObjectMethod(DataObjectMethodType.Select)]
        String[] Roles(String email);

        /// <summary>
        /// validates a email/password pair against credentials
        /// stored in the users database.  If the email/password pair is valid,
        /// the method returns user's name.
        /// </summary>  
        String Login(String email, String password);

        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddRole(PortalRole role);

        [DataObjectMethod(DataObjectMethodType.Delete)]
        void DeleteRole(int roleId);

        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateRole(PortalRole role);

        [DataObjectMethod(DataObjectMethodType.Insert)]
        void AddUserRole(int roleId, int userId);

        [DataObjectMethod(DataObjectMethodType.Delete)]
        void DeleteUserRole(int roleId, int userId);
    }
}