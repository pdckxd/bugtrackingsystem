using System;
using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class EventsDBTest 
    {
        private IEventsDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.EventsDB;
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


        #region IEventsDB Tests
        [Test]
        public void GetEvents()
        {
            //IList<PortalEvent> GetEvents(int moduleId)
            dao.GetEvents(0);
        }

        [Test]
        public void GetSingleEvent()
        {
            //PortalEvent GetSingleEvent(int itemId)
            dao.GetSingleEvent(0);
        }

        [Test]
        public void DeleteEvent()
        {
            //void DeleteEvent(int itemID)
            dao.DeleteEvent(0);
        }

        [Test]
        public void AddEvent()
        {
            //int AddEvent(int moduleId, int itemId, string userName, string title, 
            //DateTime expireDate, string description, string wherewhen)
            dao.AddEvent(0, 0, "un", "t", new DateTime(2011, 1, 1), "d", "ww");
        }

        [Test]
        public void UpdateEvent()
        {
            //void UpdateEvent(int moduleId, int itemId, string userName, string title, 
            //DateTime expireDate, string description, string wherewhen)
            dao.UpdateEvent(0, 0, "un", "t", new DateTime(2011, 1, 1), "d", "ww");
        }

        #endregion
    }
}