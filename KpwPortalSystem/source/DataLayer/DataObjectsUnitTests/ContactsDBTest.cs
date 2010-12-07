using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class ContactsDBTest 
    {
        private IContactsDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.ContactsDB;
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


        #region IContactsDB Tests
        [Test]
        public void GetContactsTest()
        {
            //IList<PortalContact> GetContacts(int moduleId)
            dao.GetContacts(0);
        }

        [Test]
        public void GetSingleContactTest()
        {
            //PortalContact GetSingleContact(int itemId)
            dao.GetSingleContact(0);
        }

        [Test]
        public void DeleteContactTest()
        {
            //void DeleteContact(int itemID)
            dao.DeleteContact(0);
        }

        [Test]
        public void AddContactTest()
        {
            //int AddContact(int moduleId, int itemId, string userName, string name, 
            //string role, string email, string contact1, string contact2)
            dao.AddContact(0, 0, "un", "n", "r", "e", "c1", "c2");
        }

        [Test]
        public void UpdateContactTest()
        {
            //void UpdateContact(int moduleId, int itemId, string userName, string name, 
            //string role, string email, string contact1, string contact2)
            dao.UpdateContact(0, 0, "un", "n", "r", "e", "c1", "c2");
        }

        #endregion
    }
}