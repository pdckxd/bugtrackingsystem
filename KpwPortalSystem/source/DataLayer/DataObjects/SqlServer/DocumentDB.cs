using System;
using System.Collections.Generic;
using System.Data;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    internal class DocumentDB : IDocumentDB
    {
        /// <summary>
        /// all of the documents for a specific portal module 
        /// </summary>
        public IList<PortalDocument> GetDocuments(int moduleId)
        {
            return Db.MapReader<PortalDocument>(StoredProcedureNames.GetDocuments, CommandType.StoredProcedure,
                                                Db.CreateParameter("ModuleID", moduleId));
        }

        /// <summary>
        /// details about a specific document 
        /// </summary>
        public PortalDocument GetSingleDocument(int itemId)
        {
            return Db.Map<PortalDocument>(StoredProcedureNames.GetSingleDocument, CommandType.StoredProcedure,
                                          Db.CreateParameter("ItemID", itemId));
        }

        /// <summary>
        /// the contents of the specified document 
        /// </summary>
        public PortalDocument GetDocumentContent(int itemId)
        {
            return Db.Map<PortalDocument>(StoredProcedureNames.GetDocumentContent, CommandType.StoredProcedure,
                                          Db.CreateParameter("ItemID", itemId));
        }


        /// <summary>
        /// deletes the specified document 
        /// </summary>
        public void DeleteDocument(int itemId)
        {
            Db.ExecuteNonQuery(StoredProcedureNames.DeleteDocument, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId));
        }


        /// <summary>
        /// updates the specified document 
        /// </summary>
        public void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category,
                                   byte[] content, int size, String contentType)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            Db.ExecuteNonQuery(StoredProcedureNames.UpdateDocument, CommandType.StoredProcedure,
                               Db.CreateParameter("ItemID", itemId),
                               Db.CreateParameter("ModuleID", moduleId),
                               Db.CreateParameter("UserName", userName),
                               Db.CreateParameter("FileFriendlyName", name),
                               Db.CreateParameter("FileNameUrl", url),
                               Db.CreateParameter("Category", category),
                               Db.CreateParameter("Content", content),
                               Db.CreateParameter("ContentType", contentType),
                               Db.CreateParameter("ContentSize", size));
        }
    }
}