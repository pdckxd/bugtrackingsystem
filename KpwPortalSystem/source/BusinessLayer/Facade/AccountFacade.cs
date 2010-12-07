using System.Collections.Generic;
using System.ComponentModel;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.BusinessLayer.Facade
{
    /// <summary>
    /// Facade (also called Service Layer) that controls all access 
    /// to data related activities 
    /// IRolesDB, IUsersDB
    /// </summary>
    public class AccountFacade : IAccountFacade
    {
        private readonly IUsersDB usersDAO = DataAccess.UsersDB;
        private readonly IRolesDB rolesDAO = DataAccess.RolesDB;

        /// <summary>
        /// a list of roles for the user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalRole> RolesByUser(string email)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return usersDAO.GetRolesByUser(email);
        }

        /// <summary>
        /// details about a specific user 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalUser SingleUser(string email)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return usersDAO.GetSingleUser(email);
        }

        /// <summary>
        /// a list of all role names for the specified portal
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalRole> PortalRoles(int portalId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return rolesDAO.GetPortalRoles(portalId);
        }

        /// <summary>
        /// a list of all members in the specified security role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalUser> RoleMembers(int roleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return rolesDAO.GetRoleMembers(roleId);
        }

        /// <summary>
        /// the UserID, Name and Email for all registered users
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalUser> Users()
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return rolesDAO.GetUsers();
        }

        /// <summary>
        /// inserts a new user record into the "Users" database table
        /// </summary>
        /// <returns>userid of the user newlly created user</returns>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int AddUser(PortalUser user)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            int retval;
            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                retval = usersDAO.AddUser(user.Name, user.Email, user.Password);
                transaction.Complete();
            }
            return retval;
        }

        /// <summary>
        /// deleted a  user record from the "Users" database table.
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteUser(int userId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                usersDAO.DeleteUser(userId);
                transaction.Complete();
            }
        }

        /// <summary>
        /// updates a  user record from the "Users" database table.
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateUser(PortalUser user)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                usersDAO.UpdateUser(user.UserID, user.Email, user.Password);
                transaction.Complete();
            }
        }

        /// <summary>
        /// a list of role names for the user
        /// </summary>        
        [DataObjectMethod(DataObjectMethodType.Select)]
        public string[] Roles(string email)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return usersDAO.GetRoles(email);
        }

        /// <summary>
        /// validates a email/password pair against credentials
        /// stored in the users database.  If the email/password pair is valid,
        /// the method returns user's name.
        /// </summary>          
        public string Login(string email, string password)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return usersDAO.Login(email, password);
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int AddRole(PortalRole role)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            int retval;
            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                retval = rolesDAO.AddRole(role.PortalID, role.RoleName);
                transaction.Complete();
            }
            return retval;
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteRole(int roleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                rolesDAO.DeleteRole(roleId);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateRole(PortalRole role)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                rolesDAO.UpdateRole(role.RoleID, role.RoleName);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddUserRole(int roleId, int userId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                rolesDAO.AddUserRole(roleId, userId);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteUserRole(int roleId, int userId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                rolesDAO.DeleteUserRole(roleId, userId);
                transaction.Complete();
            }
        }
    }
}