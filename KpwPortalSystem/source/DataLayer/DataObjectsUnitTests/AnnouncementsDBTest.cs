using System;
using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{    
    [TestFixture]    
    public class AnnouncementsDBTest 
    {
        private IAnnouncementsDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.AnnouncementsDB;            
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


        #region IAnnouncementsDB Tests

        [Test]
        public void GetAnnouncementsTest()
        {
            //IList<PortalAnnouncement> GetAnnouncements(int moduleId)
            dao.GetAnnouncements(0);
        }

        [Test]
        public void GetSingleAnnouncementTest()
        {
            //PortalAnnouncement GetSingleAnnouncement(int itemId)
            dao.GetSingleAnnouncement(0);
        }

        [Test]
        public void DeleteAnnouncementTest()
        {
            //void DeleteAnnouncement(int itemID)
            dao.DeleteAnnouncement(0);
        }

        [Test]
        public void AddAnnouncementTest()
        {
            //int AddAnnouncement(int moduleId, int itemId, string userName, string title, 
            //DateTime expireDate, string description, string moreLink, string mobileMoreLink)
            dao.AddAnnouncement(0, 0, "un", "t", new DateTime(2011, 1, 1), "d", "ml", "mml");
        }

        [Test]
        public void UpdateAnnouncementTest()
        {
            //void UpdateAnnouncement(int moduleId, int itemId, string userName, string title, 
            //DateTime expireDate, string description, string moreLink, string mobileMoreLink)
            dao.UpdateAnnouncement(0, 0, "un", "t", new DateTime(2011, 1, 1), "d", "ml", "mml");
        }

        #endregion
    }
}