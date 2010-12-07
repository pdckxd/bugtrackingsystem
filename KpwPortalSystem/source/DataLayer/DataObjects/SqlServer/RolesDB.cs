using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    internal class RolesDB : IRolesDB
    {
        #region ROLES

        /// <summary>
        /// a list of all role names for the specified portal
        /// </summary>
        public IList<PortalRole> GetPortalRoles(int portalId)
        {
            return Db.MapReader<PortalRole>(StoredProcedureNames.GetPortalRoles, CommandType.StoredProcedure,
                                            Db.CreateParameter("PortalID", portalId));
        }

        /// <summary>
        /// creates a new security role for the specified portal,
        /// and returns the new RoleID value
        /// </summary>
        public int AddRole(int portalId, String roleName)
        {
            DbParameter parameterRoleID = Db.CreateParameter("RoleID", DbType.Int32, 4);
            parameterRoleID.Direction = ParameterDirection.Output;

            // Open the database connection and execute the command            
            Db.ExecuteNonQuery(StoredProcedureNames.AddRole, CommandType.StoredProcedure,
                               Db.CreateParameter("PortalID", portalId),
                               Db.CreateParameter("RoleName", roleName),
                               parameterRoleID);

            // return the role id 
            return (int) parameterRoleID.Value;
        }

        /// <summary>
        /// deletes the specified role 
        /// </summary>
        public void DeleteRole(int roleId)
        {
            Db.ExecuteNonQuery(StoredProcedureNames.DeleteRole, CommandType.StoredProcedure,
                               Db.CreateParameter("RoleID", roleId));
        }

        /// <summary>
        /// updates the friendly name of the specified role
        /// </summary>
        public void UpdateRole(int roleId, String roleName)
        {
            // Open the database connection and execute the command            
            Db.ExecuteNonQuery(StoredProcedureNames.UpdateRole, CommandType.StoredProcedure,
                               Db.CreateParameter("RoleID", roleId),
                               Db.CreateParameter("RoleName", roleName));
        }

        #endregion

        #region USER ROLES

        /// <summary>
        /// a list of all members in the specified
        /// security role
        /// </summary>
        public IList<PortalUser> GetRoleMembers(int roleId)
        {
            return Db.MapReader<PortalUser>(StoredProcedureNames.GetRoleMembership, CommandType.StoredProcedure,
                                            Db.CreateParameter("RoleID", roleId));
        }

        /// <summary>
        /// adds the user to the specified security role
        /// </summary>
        public void AddUserRole(int roleId, int userId)
        {
            // Open the database connection and execute the command            
            Db.ExecuteNonQuery(StoredProcedureNames.AddUserRole, CommandType.StoredProcedure,
                               Db.CreateParameter("RoleID", roleId),
                               Db.CreateParameter("UserID", userId));
        }

        /// <summary>
        /// deletes the user from the specified role
        /// </summary>
        public void DeleteUserRole(int roleId, int userId)
        {
            // Open the database connection and execute the command            
            Db.ExecuteNonQuery(StoredProcedureNames.DeleteUserRole, CommandType.StoredProcedure,
                               Db.CreateParameter("RoleID", roleId),
                               Db.CreateParameter("UserID", userId));
        }

        #endregion

        #region USERS        

        /// <summary>
        /// the UserID, Name and Email for 
        /// all registered users
        /// </summary>
        public IList<PortalUser> GetUsers()
        {
            // Return the datareader
            return Db.MapReader<PortalUser>(StoredProcedureNames.GetUsers, CommandType.StoredProcedure);
        }

        #endregion
    }
}