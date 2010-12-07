namespace Nairc.KPWPortal.DataLayer.Interface
{
    public interface IPortalModulesDB
    {
        void DeletePortalModule(params int[] ModuleIdList);
    }
}