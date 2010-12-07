using System;
using System.Collections.Generic;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface IDocumentDB
    {
        IList<PortalDocument> GetDocuments(int moduleId);
        PortalDocument GetSingleDocument(int itemId);
        PortalDocument GetDocumentContent(int itemId);
        void DeleteDocument(int itemID);

        void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category,
                            byte[] content, int size, String contentType);
    }
}