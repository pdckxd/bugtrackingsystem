using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class DocumentDBTest 
    {
        private IDocumentDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.DocumentDB;
        }

        [TearDown]
        public void TearDown()
        {
            
        }       

        [Test]
        public void DaoFactoryTest()
        {
            Assert.AreEqual(true, dao != null);
        }


        #region IDocumentDB Tests
        [Test]
        public void GetDocumentsTest()
        {
            //IList<PortalDocument> GetDocuments(int moduleId)
            dao.GetDocuments(0);
        }

        [Test]
        public void GetSingleDocumentTest()
        {
            //PortalDocument GetSingleDocument(int itemId)
            dao.GetSingleDocument(0);
        }

        [Test]
        public void GetDocumentContentTest()
        {
            //PortalDocument GetDocumentContent(int itemId)
            dao.GetDocumentContent(0);
        }

        [Test]
        public void DeleteDocumentTest()
        {
            //void DeleteDocument(int itemID)
            dao.DeleteDocument(0);
        }

        [Test]
        public void UpdateDocumentTest()
        {
            //void UpdateDocument(int moduleId, int itemId, string userName, string name, 
            //string url, string category, byte[] content, int size, string contentType)
            dao.UpdateDocument(0, 0, "un", "n", "u", "c", new byte[]{}, 0, "ct");
        }

        #endregion
    }
}