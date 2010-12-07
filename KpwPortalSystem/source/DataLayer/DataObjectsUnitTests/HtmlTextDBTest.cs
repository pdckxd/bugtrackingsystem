using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using NUnit.Framework;

namespace Nairc.KPWPortal.DataLayer.DataObjectsUnitTests
{
    [TestFixture]
    public class HtmlTextDBTest 
    {
        private IHtmlTextDB dao;        

        [SetUp]
        public void SetUp()
        {
            dao = DataAccess.HtmlTextDB;
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


        #region IHtmlTextDB Tests
        [Test]
        public void GetHtmlText()
        {
            //PortalHtmlText GetHtmlText(int moduleId)
            dao.GetHtmlText(0);
        }

        [Test]
        public void UpdateHtmlText()
        {
            //void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails)
            dao.UpdateHtmlText(0, "dh", "ms", "md");
        }

        #endregion
    }
}