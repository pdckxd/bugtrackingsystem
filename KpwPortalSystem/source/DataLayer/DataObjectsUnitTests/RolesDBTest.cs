using System;
using System.Collections.Generic;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class RolesDBTest 
    {
        private IRolesDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.RolesDB;
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


        #region IRolesDB Tests
        [Test]
        public void GetPortalRoles()
        {
            //IList<PortalRole> GetPortalRoles(int portalId)
            dao.GetPortalRoles(0);
        }

        [Test]
        public void AddRole()
        {
            //int AddRole(int portalId, string roleName)
            string rolename = "role" + DateTime.Now.Ticks;
            dao.AddRole(0, rolename);
        }

        [Test]
        public void DeleteRole()
        {
            //void DeleteRole(int roleId)
            dao.DeleteRole(0);
        }

        [Test]
        public void UpdateRole()
        {
            //void UpdateRole(int roleId, string roleName)
            dao.UpdateRole(0,"rn");
        }

        [Test]
        public void GetRoleMembers()
        {
            //IList<PortalUser> GetRoleMembers(int roleId)
            dao.GetRoleMembers(0);
        }

        /// <summary>
        /// we need to have at least one user and one role,
        /// otherwise the test will fail...
        /// </summary>
        [Test]        
        public void AddUserRole()
        {
            //void AddUserRole(int roleId, int userId)                        
            int UserID = dao.GetUsers()[0].UserID;
            int RoleID = dao.AddRole(0, "role" + DateTime.Now.Ticks);
            dao.AddUserRole(RoleID, UserID);            
        }

        [Test]
        public void DeleteUserRole()
        {
            //void DeleteUserRole(int roleId, int userId)
            dao.DeleteUserRole(0, 0);
        }

        [Test]
        public void GetUsers()
        {
            //IList<PortalUser> GetUsers()
            dao.GetUsers();
        }

        #endregion
    }
}