using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    internal class EventsDB : IEventsDB
    {
        /// <summary>
        /// all of the events for a specific portal module 
        /// </summary>        
        public IList<PortalEvent> GetEvents(int moduleId)
        {
            return Db.MapReader<PortalEvent>(StoredProcedureNames.GetEvents, CommandType.StoredProcedure,
                                             Db.CreateParameter("ModuleID", moduleId));
        }

        /// <summary>
        /// details about a specific event 
        /// </summary>
        public PortalEvent GetSingleEvent(int itemId)
        {
            return Db.Map<PortalEvent>(StoredProcedureNames.GetSingleEvent, CommandType.StoredProcedure,
                                       Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// deletes a specified event 
        /// </summary>
        public void DeleteEvent(int itemId)
        {
            Db.ExecuteNonQuery(StoredProcedureNames.DeleteEvent, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// adds a new event within the Events database table, 
        /// and returns the ItemID value as a result
        /// </summary>
        public int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                            String description, String wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            DbParameter parameterItemID = Db.CreateParameter("ItemID", DbType.Int32, 4);
            parameterItemID.Direction = ParameterDirection.Output;

            // Open the database connection and execute SQL Command            
            Db.ExecuteNonQuery(StoredProcedureNames.AddEvent, CommandType.StoredProcedure,
                               parameterItemID,
                               Db.CreateParameter("ModuleID", moduleId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("Title", title),
                               Db.CreateParameter("WhereWhen", wherewhen),
                               Db.CreateParameter("ExpireDate", expireDate),
                               Db.CreateParameter("Description", description));

            // Return the new Event ItemID
            return (int) parameterItemID.Value;
        }

        /// <summary>
        /// updates the specified event 
        /// </summary>
        public void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                String description, String wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            Db.ExecuteNonQuery(StoredProcedureNames.UpdateEvent, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("Title", title),
                               Db.CreateParameter("WhereWhen", wherewhen),
                               Db.CreateParameter("ExpireDate", expireDate),
                               Db.CreateParameter("Description", description));
        }
    }
}