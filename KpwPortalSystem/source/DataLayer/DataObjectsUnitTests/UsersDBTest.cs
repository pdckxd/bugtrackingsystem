using System;
using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class UsersDBTest 
    {
        private IUsersDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.UsersDB;
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


        #region IUsersDB Tests
        [Test]
        public void AddUser()
        {            
            //int AddUser(string fullName, string email, string password)
            string username = "user" + DateTime.Now.Ticks;
            dao.AddUser(username, username + "@mail.com", "a");
        }

        [Test]
        public void DeleteUser()
        {
            //void DeleteUser(int userId)
            dao.DeleteUser(0);
        }

        [Test]
        public void UpdateUser()
        {
            //void UpdateUser(int userId, string email, string password)
            dao.UpdateUser(0,"e","p");
        }

        [Test]
        public void GetRolesByUser()
        {
            //IList<PortalRole> GetRolesByUser(string email)
            dao.GetRolesByUser("e");
        }

        [Test]
        public void GetSingleUser()
        {
            //PortalUser GetSingleUser(string email)
            dao.GetSingleUser("e");
        }

        [Test]
        public void GetRoles()
        {
            //string[] GetRoles(string email)
            dao.GetRoles("e");
        }

        [Test]
        public void Login()
        {
            //string Login(string email, string password)
            dao.Login("e", "p");
        }

        #endregion
    }
}