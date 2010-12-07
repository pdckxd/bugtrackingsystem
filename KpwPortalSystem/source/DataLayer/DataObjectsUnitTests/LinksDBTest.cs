using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class LinksDBTest 
    {
        private ILinkDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.LinkDB;
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


        #region ILinkDB Tests
        [Test]
        public void GetLinks()
        {
            //IList<PortalLink> GetLinks(int moduleId)
            dao.GetLinks(0);
        }

        [Test]
        public void GetSingleLink()
        {
            //PortalLink GetSingleLink(int itemId)
            dao.GetSingleLink(0);
        }

        [Test]
        public void DeleteLink()
        {
            //void DeleteLink(int itemID)
            dao.DeleteLink(0);
        }

        [Test]
        public void AddLink()
        {
            //int AddLink(int moduleId, int itemId, string userName, string title, string url, 
            //string mobileUrl, int viewOrder, string description)
            dao.AddLink(0, 0, "un", "t", "url", "mu", 0, "d");
        }

        [Test]
        public void UpdateLink()
        {
            //void UpdateLink(int moduleId, int itemId, string userName, string title, string url, 
            //string mobileUrl, int viewOrder, string description)
            dao.UpdateLink(0, 0, "un", "t", "url", "mu", 0, "d");
        }

        #endregion
    }
}