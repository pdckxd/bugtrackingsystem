using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    /// <summary>    
    /// The UsersDB class encapsulates all data logic necessary to add/login/query
    /// users within the Portal Users database.
    /// </summary>    
    /// <remarks>
    /// Important Note: The UsersDB class is only used when forms-based cookie
    /// authentication is enabled within the portal.  When windows based
    /// authentication is used instead, then either the Windows SAM or Active Directory
    /// is used to store and validate all username/password credentials.
    /// </remarks>
    internal class UsersDB : IUsersDB
    {
        /// <summary>
        /// inserts a new user record into the "Users" database table
        /// </summary>
        /// <returns>userid of the user newlly created user</returns>
        public int AddUser(String fullName, String email, String password)
        {
            DbParameter parameterUserId = Db.CreateParameter("UserID", SqlDbType.Int);
            parameterUserId.Direction = ParameterDirection.Output;

            // Execute the command in a try/catch to catch duplicate username errors
            try
            {
                // execute the Command                
                Db.ExecuteNonQuery(StoredProcedureNames.AddUser, CommandType.StoredProcedure,
                                   Db.CreateParameter("Name", fullName),
                                   Db.CreateParameter("Email", email),
                                   Db.CreateParameter("Password", password),
                                   parameterUserId);
            }
            catch
            {
                // failed to create a new user
                return -1;
            }

            return (int) parameterUserId.Value;
        }

        /// <summary>
        /// deleted a  user record from the "Users" database table.
        /// </summary>
        public void DeleteUser(int userId)
        {
            Db.ExecuteNonQuery(StoredProcedureNames.DeleteUser, CommandType.StoredProcedure,
                               Db.CreateParameter("UserID", userId));
        }

        /// <summary>
        /// updates a  user record from the "Users" database table.
        /// </summary>
        public void UpdateUser(int userId, String email, String password)
        {
            // Open the database connection and execute the command            
            Db.ExecuteNonQuery(StoredProcedureNames.UpdateUser, CommandType.StoredProcedure,
                               Db.CreateParameter("UserID", userId),
                               Db.CreateParameter("Email", email),
                               Db.CreateParameter("Password", password));
        }

        /// <summary>
        /// 
        /// </summary>        
        public IList<PortalRole> GetRolesByUser(String email)
        {
            return Db.MapReader<PortalRole>(StoredProcedureNames.GetRolesByUser, CommandType.StoredProcedure,
                                            Db.CreateParameter("Email", email));
        }

        /// <summary>
        /// details about a specific user 
        /// </summary>       
        public PortalUser GetSingleUser(String email)
        {
            return Db.Map<PortalUser>(StoredProcedureNames.GetSingleUser, CommandType.StoredProcedure,
                                      Db.CreateParameter("Email", email));
        }

        /// <summary>
        /// a list of role names for the user
        /// </summary>        
        public String[] GetRoles(String email)
        {
            //get a datareader
            IDataReader dr = Db.ExecuteReader(StoredProcedureNames.GetRolesByUser, CommandType.StoredProcedure,
                                              Db.CreateParameter("Email", email));

            // create a String array from the data
            List<string> userRoles = new List<string>();

            while (dr.Read())
            {
                userRoles.Add(dr["RoleName"].ToString());
            }

            dr.Close();

            // Return the String array of roles
            return userRoles.ToArray();
        }

        /// <summary>
        /// validates a email/password pair against credentials
        /// stored in the users database.  If the email/password pair is valid,
        /// the method returns user's name.
        /// </summary>       
        public String Login(String email, String password)
        {
            DbParameter parameterUserName = Db.CreateParameter("UserName", DbType.String, 100);
            parameterUserName.Direction = ParameterDirection.Output;

            // Open the database connection and execute the command            
            Db.ExecuteNonQuery(StoredProcedureNames.UserLogin, CommandType.StoredProcedure,
                               Db.CreateParameter("Email", email),
                               Db.CreateParameter("Password", password),
                               parameterUserName);

            if ((parameterUserName.Value != null) && (parameterUserName.Value != DBNull.Value))
            {
                return ((String) parameterUserName.Value).Trim();
            }
            else
            {
                return String.Empty;
            }
        }
    }
}