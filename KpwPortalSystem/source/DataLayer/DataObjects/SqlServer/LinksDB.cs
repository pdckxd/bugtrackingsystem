using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    internal class LinkDB : ILinkDB
    {
        /// <summary>
        /// all of the links for a specific portal module 
        /// </summary>        
        public IList<PortalLink> GetLinks(int moduleId)
        {
            return Db.MapReader<PortalLink>(StoredProcedureNames.GetLinks, CommandType.StoredProcedure,
                                            Db.CreateParameter("ModuleID", moduleId));
        }

        /// <summary>
        /// details about a specific link 
        /// </summary>
        public PortalLink GetSingleLink(int itemId)
        {
            return Db.Map<PortalLink>(StoredProcedureNames.GetSingleLink, CommandType.StoredProcedure,
                                      Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// deletes a specified link 
        /// </summary>
        public void DeleteLink(int itemId)
        {
            Db.ExecuteNonQuery(StoredProcedureNames.DeleteLink, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// adds a new link within the
        /// links database table, and returns ItemID value as a result
        /// </summary>        
        public int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                           int viewOrder, String description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            DbParameter parameterItemID = Db.CreateParameter("ItemID", DbType.Int32, 4);
            parameterItemID.Direction = ParameterDirection.Output;

            Db.ExecuteNonQuery(StoredProcedureNames.AddLink, CommandType.StoredProcedure,
                               parameterItemID,
                               Db.CreateParameter("ModuleID", moduleId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("Title", title),
                               Db.CreateParameter("Description", description),
                               Db.CreateParameter("Url", url),
                               Db.CreateParameter("MobileUrl", mobileUrl),
                               Db.CreateParameter("ViewOrder", viewOrder));

            return (int) parameterItemID.Value;
        }

        /// <summary>
        /// updates a specified link 
        /// </summary>
        public void UpdateLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                               int viewOrder, String description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            Db.ExecuteNonQuery(StoredProcedureNames.UpdateLink, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("Title", title),
                               Db.CreateParameter("Description", description),
                               Db.CreateParameter("Url", url),
                               Db.CreateParameter("MobileUrl", mobileUrl),
                               Db.CreateParameter("ViewOrder", viewOrder));
        }
    }
}