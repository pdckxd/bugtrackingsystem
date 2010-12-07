using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class PortalModulesDBTest 
    {
        private IPortalModulesDB dao;

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.PortalModulesDB;
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

        #region IPortalModulesDB Tests
        [Test]
        public void DeletePortalModule()
        {
            dao.DeletePortalModule(0);
        }
        #endregion
    }
}