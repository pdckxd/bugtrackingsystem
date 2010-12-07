using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class DiscussionDBTest 
    {
        private IDiscussionDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.DiscussionDB;
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


        #region IDiscussionDB Tests

        [Test]
        public void GetTopLevelMessagesTest()
        {
            //IList<PortalDiscussion> GetTopLevelMessages(int moduleId)
            dao.GetTopLevelMessages(0);
        }

        [Test]
        public void GetThreadMessagesTest()
        {
            //IList<PortalDiscussion> GetThreadMessages(string parent)
            dao.GetThreadMessages("p");
        }

        [Test]
        public void GetSingleMessageTest()
        {
            //PortalDiscussion GetSingleMessage(int itemId)
            dao.GetSingleMessage(0);
        }

        [Test]
        public void AddMessageTest()
        {
            //int AddMessage(int moduleId, int parentId, string userName, string title, string body)
            dao.AddMessage(0, 0, "un", "t", "b");
        }

        #endregion
    }
}