namespace Nairc.KPWPortal.BusinessLayer.Facade
{
    public interface IAdminFacade
    {
        void DeletePortalModule(params int[] ModuleIdList);
    }
}