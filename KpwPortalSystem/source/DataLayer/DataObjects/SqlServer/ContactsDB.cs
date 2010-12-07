using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    internal class ContactsDB : IContactsDB
    {
        /// <summary>
        /// all of the contacts for a specific portal module 
        /// </summary>
        public IList<PortalContact> GetContacts(int moduleId)
        {
            //        
            return Db.MapReader<PortalContact>(StoredProcedureNames.GetContacts, CommandType.StoredProcedure,
                                               Db.CreateParameter("ModuleID", moduleId));
        }

        /// <summary>
        /// details about a specific contact 
        /// </summary>
        public PortalContact GetSingleContact(int itemId)
        {
            return Db.Map<PortalContact>(StoredProcedureNames.GetSingleContact, CommandType.StoredProcedure,
                                         Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// deletes the specified contact 
        /// </summary>
        public void DeleteContact(int itemId)
        {
            Db.ExecuteNonQuery(StoredProcedureNames.DeleteContact, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// adds a new contact to the Contacts
        /// database table, and returns the ItemId value as a result
        /// </summary>        
        public int AddContact(int moduleId, int itemId, String userName, String name, String role, String email,
                              String contact1, String contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC            
            DbParameter parameterItemID = Db.CreateParameter("ItemID", DbType.Int32, 4);
            parameterItemID.Direction = ParameterDirection.Output;

            Db.ExecuteNonQuery(StoredProcedureNames.AddContact, CommandType.StoredProcedure,
                               parameterItemID,
                               Db.CreateParameter("ModuleID", moduleId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("Name", name),
                               Db.CreateParameter("Role", role),
                               Db.CreateParameter("Email", email),
                               Db.CreateParameter("Contact1", contact1),
                               Db.CreateParameter("Contact2", contact2));

            return (int) parameterItemID.Value;
        }

        /// <summary>
        /// updates the specified contact 
        /// </summary>        
        public void UpdateContact(int moduleId, int itemId, String userName, String name, String role, String email,
                                  String contact1, String contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            Db.ExecuteNonQuery(StoredProcedureNames.UpdateContact, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("Name", name),
                               Db.CreateParameter("Role", role),
                               Db.CreateParameter("Email", email),
                               Db.CreateParameter("Contact1", contact1),
                               Db.CreateParameter("Contact2", contact2));
        }
    }
}