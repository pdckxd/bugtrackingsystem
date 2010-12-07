using System.Configuration;
using Nairc.KPWPortal.DataLayer.Interface;

namespace Nairc.KPWPortal.DataLayer.DataObjects
{
    /// <summary>
    /// This class shields the client from the details of database specific 
    /// data-access objects. It returns the appropriate data-access objects 
    /// according to the configuration in web.config.
    /// 
    /// GoF Design Patterns: Factory, Singleton, Proxy.
    /// </summary>
    /// <remarks>
    /// This class makes extensive use of the Factory pattern in determining which 
    /// database specific DAOs (Data Access Objects) to return.
    /// 
    /// This class is like a Singleton -- it is a static class (Shared in VB) and 
    /// therefore only one 'instance' ever will exist.
    /// 
    /// This class is a Proxy in that it 'stands in' for the actual Data Access Object Factory.
    /// </remarks>
    public static class DataAccess
    {
        // The static field initializers below are thread safe.
        // Furthermore, they are executed in the order in which they appear
        // in the class declaration. Note: if a static constructor
        // is present you want to initialize these in that constructor.        
        private static readonly string dataProvider =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;

        private static readonly DaoFactory factory = DaoFactories.GetFactory(dataProvider);

        public static IAnnouncementsDB AnnouncementsDB
        {
            get { return factory.AnnouncementsDB; }
        }

        public static IContactsDB ContactsDB
        {
            get { return factory.ContactsDB; }
        }

        public static IDiscussionDB DiscussionDB
        {
            get { return factory.DiscussionDB; }
        }

        public static IDocumentDB DocumentDB
        {
            get { return factory.DocumentDB; }
        }

        public static IEventsDB EventsDB
        {
            get { return factory.EventsDB; }
        }

        public static IHtmlTextDB HtmlTextDB
        {
            get { return factory.HtmlTextDB; }
        }

        public static ILinkDB LinkDB
        {
            get { return factory.LinkDB; }
        }

        public static IRolesDB RolesDB
        {
            get { return factory.RolesDB; }
        }

        public static IUsersDB UsersDB
        {
            get { return factory.UsersDB; }
        }

        public static IPortalModulesDB PortalModulesDB
        {
            get { return factory.PortalModulesDB; }
        }
    }
}