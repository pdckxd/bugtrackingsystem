using Nairc.KPWPortal.DataLayer.Interface;

namespace Nairc.KPWPortal.DataLayer.DataObjects.MSAccess
{
    /// <summary>
    /// Microsoft Access specific factory that creates Sql Server specific data access objects.
    /// 
    /// GoF Design Pattern: Factory.
    /// </summary>
    internal class MSAccessDaoFactory : DaoFactory
    {
        public override IAnnouncementsDB AnnouncementsDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override IContactsDB ContactsDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override IDiscussionDB DiscussionDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override IDocumentDB DocumentDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override IEventsDB EventsDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override IHtmlTextDB HtmlTextDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override ILinkDB LinkDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override IRolesDB RolesDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override IUsersDB UsersDB
        {
            get { throw new System.NotImplementedException(""); }
        }

        public override IPortalModulesDB PortalModulesDB
        {
            get { throw new System.NotImplementedException(""); }
        }
    }
}