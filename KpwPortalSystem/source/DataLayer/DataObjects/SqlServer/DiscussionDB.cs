using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    internal class DiscussionDB : IDiscussionDB
    {
        /// <summary>
        /// details for all of the messages in the discussion specified by ModuleID
        /// </summary>        
        public IList<PortalDiscussion> GetTopLevelMessages(int moduleId)
        {
            return Db.MapReader<PortalDiscussion>(StoredProcedureNames.GetTopLevelMessages, CommandType.StoredProcedure,
                                                  Db.CreateParameter("ModuleID", moduleId));
        }

        /// <summary>
        /// details for all of the messages the thread, as identified by the Parent id string
        /// </summary>
        public IList<PortalDiscussion> GetThreadMessages(String parent)
        {
            return Db.MapReader<PortalDiscussion>(StoredProcedureNames.GetThreadMessages, CommandType.StoredProcedure,
                                                  Db.CreateParameter("Parent", parent));
        }

        /// <summary>
        /// details for the message specified by the itemId parameter
        /// </summary>
        public PortalDiscussion GetSingleMessage(int itemId)
        {
            return Db.Map<PortalDiscussion>(StoredProcedureNames.GetSingleMessage, CommandType.StoredProcedure,
                                            Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// adds a new message within the Discussions database table, and returns ItemID value as a result
        /// </summary>
        public int AddMessage(int moduleId, int parentId, String userName, String title, String body)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            DbParameter parameterItemID = Db.CreateParameter("ItemID", DbType.Int32, 4);
            parameterItemID.Direction = ParameterDirection.Output;

            Db.ExecuteNonQuery(StoredProcedureNames.AddMessage, CommandType.StoredProcedure,
                               parameterItemID,
                               Db.CreateParameter("Title", title),
                               Db.CreateParameter("Body", body),
                               Db.CreateParameter("ParentID", parentId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("ModuleID", moduleId)
                );

            return (int) parameterItemID.Value;
        }
    }
}