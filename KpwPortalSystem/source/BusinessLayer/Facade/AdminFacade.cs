using System.ComponentModel;
using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.BusinessLayer.Facade
{
    /// <summary>
    /// Facade (also called Service Layer) that controls all access 
    /// to data related activities 
    /// </summary>
    public class AdminFacade : IAdminFacade
    {
        private readonly IPortalModulesDB portalModulesDAO = DataAccess.PortalModulesDB;

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeletePortalModule(params int[] ModuleIdList)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                portalModulesDAO.DeletePortalModule(ModuleIdList);
                transaction.Complete();
            }
        }
    }
}