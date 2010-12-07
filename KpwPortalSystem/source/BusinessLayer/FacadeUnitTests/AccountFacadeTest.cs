using System;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;
using NUnit.Framework;

namespace Nairc.KPWPortal.BusinessLayer.FacadeUnitTests
{    
    [TestFixture]
    public class AccountFacadeTest 
    {        
        #region IAccountFacade Tests
        [Test]
        public void RolesByUserTest()
        {
            //IList<PortalRole> RolesByUser(string email)
            AccountFacade facade = new AccountFacade();
            facade.RolesByUser("e");
        }

        [Test]
        public void SingleUserTest()
        {
            //PortalRole SingleUser(string email)
            AccountFacade facade = new AccountFacade();
            facade.SingleUser("e");
        }

        [Test]
        public void PortalRolesTest()
        {
            //IList<PortalRole> GetPortalRoles(int portalId)
            AccountFacade facade = new AccountFacade();
            facade.PortalRoles(0);
        }

        [Test]
        public void RoleMembersTest()
        {
            //IList<PortalUser> GetRoleMembers(int roleId)
            AccountFacade facade = new AccountFacade();
            facade.RoleMembers(0);
        }

        [Test]
        public void UsersTest()
        {
            //IList<PortalUser> GetUsers()
            AccountFacade facade = new AccountFacade();
            facade.Users();
        }

        [Test]
        public void RolesTest()
        {
            //string[] Roles(string email)
            AccountFacade facade = new AccountFacade();
            facade.Roles("e");
        }

        [Test]
        public void LoginTest()
        {
            //string Login(string email, string password)
            AccountFacade facade = new AccountFacade();
            facade.Login("e", "a");
        }

        [Test]
        public void AddUser()
        {
            //int AddUser(string fullName, string email, string password)
            AccountFacade facade = new AccountFacade();

            PortalUser user = new PortalUser();
            user.Name="user" + DateTime.Now.Ticks;
            user.Email = "e";
            user.Password = "p";

            facade.AddUser(user);
        }

        [Test]
        public void DeleteUser()
        {
            //void DeleteUser(int userId)
            AccountFacade facade = new AccountFacade();
            facade.DeleteUser(0);
        }

        [Test]
        public void UpdateUser()
        {
            //void UpdateUser(int userId, string email, string password)
            AccountFacade facade = new AccountFacade();

            PortalUser user= new PortalUser();
            user.UserID=0;
            user.Email = "e";
            user.Password = "p";

            facade.UpdateUser(user);
        }

        [Test]
        public void AddRoleTest()
        {
            //int AddRole(int portalId, String roleName)
            AccountFacade facade = new AccountFacade();

            PortalRole role = new PortalRole();
            role.PortalID = 0;
            role.RoleName = "role" + DateTime.Now.Ticks;

            facade.AddRole(role);
        }

        [Test]
        public void DeleteRoleTest()
        {
            //void DeleteRole(int roleId)
            AccountFacade facade = new AccountFacade();
            facade.DeleteRole(0);
        }

        [Test]
        public void UpdateRoleTest()
        {
            //void UpdateRole(int roleId, String roleName)
            AccountFacade facade = new AccountFacade();

            PortalRole role = new PortalRole();
            role.PortalID = 0;
            role.RoleName = "rl";

            facade.UpdateRole(role);
        }

        [Test]
        public void AddUserRoleTest()
        {
            //void AddUserRole(int roleId, int userId)
            AccountFacade facade = new AccountFacade();
            int UserID = facade.Users()[0].UserID;

            PortalRole role = new PortalRole();
            role.PortalID = 0;
            role.RoleName = "role" + DateTime.Now.Ticks;

            int RoleID = facade.AddRole(role);
            facade.AddUserRole(RoleID, UserID);
        }

        [Test]
        public void DeleteUserRoleTest()
        {
            //void DeleteUserRole(int roleId, int userId)
            AccountFacade facade = new AccountFacade();
            facade.DeleteUserRole(0, 0);
        }

        #endregion
    }
}