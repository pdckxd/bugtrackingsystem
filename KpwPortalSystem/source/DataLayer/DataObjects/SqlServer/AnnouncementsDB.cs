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
    /// Sql Server specific data access object that handles data access
    /// of announcements.
    /// </summary>
    internal class AnnouncementsDB : IAnnouncementsDB
    {
        /// <summary>
        /// all of the announcements for a specific portal module 
        /// </summary>
        public IList<PortalAnnouncement> GetAnnouncements(int moduleId)
        {
            return Db.MapReader<PortalAnnouncement>(StoredProcedureNames.GetAnnouncements, CommandType.StoredProcedure,
                                                    Db.CreateParameter("ModuleID", moduleId));
        }

        /// <summary>
        /// details about a specific announcement 
        /// </summary>
        public PortalAnnouncement GetSingleAnnouncement(int itemId)
        {
            return Db.Map<PortalAnnouncement>(StoredProcedureNames.GetSingleAnnouncement, CommandType.StoredProcedure,
                                              Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// deletes the specified announcement 
        /// </summary>
        public void DeleteAnnouncement(int itemID)
        {
            Db.ExecuteNonQuery(StoredProcedureNames.DeleteAnnouncement, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemID));
        }

        /// <summary>
        /// adds a new announcement to the
        /// Announcements database table, and returns the ItemId value as a result
        /// </summary>        
        public int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                   String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            DbParameter parameterItemID = Db.CreateParameter("ItemID", DbType.Int32, 4);
            parameterItemID.Direction = ParameterDirection.Output;

            Db.ExecuteNonQuery(StoredProcedureNames.AddAnnouncement, CommandType.StoredProcedure,
                               parameterItemID,
                               Db.CreateParameter("ModuleID", moduleId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("Title", title),
                               Db.CreateParameter("MoreLink", moreLink),
                               Db.CreateParameter("MobileMoreLink", mobileMoreLink),
                               Db.CreateParameter("ExpireDate", expireDate),
                               Db.CreateParameter("Description", description));

            return (int) parameterItemID.Value;
        }

        /// <summary>
        /// updates the specified announcement 
        /// </summary>       
        public void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                       String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            Db.ExecuteNonQuery(StoredProcedureNames.UpdateAnnouncement, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("Title", title),
                               Db.CreateParameter("MoreLink", moreLink),
                               Db.CreateParameter("MobileMoreLink", mobileMoreLink),
                               Db.CreateParameter("ExpireDate", expireDate),
                               Db.CreateParameter("Description", description)
                );
        }
    }
}