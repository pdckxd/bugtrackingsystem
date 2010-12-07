using Nairc.KPWPortal.DataLayer.Interface;

namespace Nairc.KPWPortal.DataLayer.DataObjects
{
    /// <summary>
    /// Abstract factory class that creates data access objects.
    /// 
    /// GoF Design Pattern: Factory.
    /// </summary>
    public abstract class DaoFactory
    {
        public abstract IAnnouncementsDB AnnouncementsDB { get; }
        public abstract IContactsDB ContactsDB { get; }
        public abstract IDiscussionDB DiscussionDB { get; }
        public abstract IDocumentDB DocumentDB { get; }
        public abstract IEventsDB EventsDB { get; }
        public abstract IHtmlTextDB HtmlTextDB { get; }
        public abstract ILinkDB LinkDB { get; }
        public abstract IRolesDB RolesDB { get; }
        public abstract IUsersDB UsersDB { get; }
        public abstract IPortalModulesDB PortalModulesDB { get; }
    }
}