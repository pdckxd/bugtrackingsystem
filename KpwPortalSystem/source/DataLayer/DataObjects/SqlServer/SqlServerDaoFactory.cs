using Nairc.KPWPortal.DataLayer.Interface;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    /// <summary>
    /// Sql Server specific factory that creates Sql Server specific data access objects.
    /// 
    /// GoF Design Pattern: Factory.
    /// </summary>
    internal class SqlServerDaoFactory : DaoFactory
    {
        public override IAnnouncementsDB AnnouncementsDB
        {
            get { return new AnnouncementsDB(); }
        }

        public override IContactsDB ContactsDB
        {
            get { return new ContactsDB(); }
        }

        public override IDiscussionDB DiscussionDB
        {
            get { return new DiscussionDB(); }
        }

        public override IDocumentDB DocumentDB
        {
            get { return new DocumentDB(); }
        }

        public override IEventsDB EventsDB
        {
            get { return new EventsDB(); }
        }

        public override IHtmlTextDB HtmlTextDB
        {
            get { return new HtmlTextDB(); }
        }

        public override ILinkDB LinkDB
        {
            get { return new LinkDB(); }
        }

        public override IRolesDB RolesDB
        {
            get { return new RolesDB(); }
        }

        public override IUsersDB UsersDB
        {
            get { return new UsersDB(); }
        }

        public override IPortalModulesDB PortalModulesDB
        {
            get { return new PortalModulesDB(); }
        }
    }
}