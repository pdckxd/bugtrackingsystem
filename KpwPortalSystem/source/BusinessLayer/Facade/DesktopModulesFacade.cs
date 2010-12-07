using System.Collections.Generic;
using System.ComponentModel;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.DataObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.BusinessLayer.Facade
{
    /// <summary>
    /// Facade (also called Service Layer) that controls all access 
    /// to data related activities 
    /// IAnnouncementsDB, IContactsDB, IDiscussionDB, IDocumentDB, IEventsDB, IHtmlTextDB, ILinkDB
    /// </summary>
    [DataObject(true)]
    public class DesktopModulesFacade : IDesktopModulesFacade
    {
        private readonly IAnnouncementsDB announcementsDAO = DataAccess.AnnouncementsDB;
        private readonly IContactsDB contactsDAO = DataAccess.ContactsDB;
        private readonly IDiscussionDB discussionDAO = DataAccess.DiscussionDB;
        private readonly IDocumentDB documentDAO = DataAccess.DocumentDB;
        private readonly IEventsDB eventsDAO = DataAccess.EventsDB;
        private readonly IHtmlTextDB htmlTextDAO = DataAccess.HtmlTextDB;
        private readonly ILinkDB linkDAO = DataAccess.LinkDB;

        /// <summary>
        /// Gets a list of announcements in a given moduleId
        /// </summary>        
        /// <returns>list of announcements</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalAnnouncement> Announcements(int moduleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return announcementsDAO.GetAnnouncements(moduleId);
        }

        /// <summary>
        /// Gets an announcement for a given itemId
        /// </summary>        
        /// <returns>announcement</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalAnnouncement SingleAnnouncement(int itemId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return announcementsDAO.GetSingleAnnouncement(itemId);
        }

        /// <summary>
        /// all of the contacts for a specific portal module 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns>list of contacts</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalContact> Contacts(int moduleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return contactsDAO.GetContacts(moduleId);
        }

        /// <summary>
        /// details about a specific contact 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>contact</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalContact SingleContact(int itemId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return contactsDAO.GetSingleContact(itemId);
        }

        /// <summary>
        /// details for all of the messages in the discussion specified by ModuleID
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalDiscussion> TopLevelMessages(int moduleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return discussionDAO.GetTopLevelMessages(moduleId);
        }

        /// <summary>
        /// details for all of the messages the thread, as identified by the Parent id string
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalDiscussion> ThreadMessages(string parent)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return discussionDAO.GetThreadMessages(parent);
        }

        /// <summary>
        /// details for the message specified by the itemId 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalDiscussion SingleMessage(int itemId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return discussionDAO.GetSingleMessage(itemId);
        }


        /// <summary>
        /// all of the links for a specific portal module 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalLink> Links(int moduleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return linkDAO.GetLinks(moduleId);
        }

        /// <summary>
        /// details about a specific link 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalLink SingleLink(int itemId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return linkDAO.GetSingleLink(itemId);
        }

        /// <summary>
        /// all of the documents for a specific portal module 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalDocument> Documents(int moduleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return documentDAO.GetDocuments(moduleId);
        }

        /// <summary>
        /// details about a specific document
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalDocument SingleDocument(int itemId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return documentDAO.GetSingleDocument(itemId);
        }

        /// <summary>
        /// the contents of the specified document 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalDocument DocumentContent(int itemId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return documentDAO.GetDocumentContent(itemId);
        }

        /// <summary>
        /// all of the events for a specific portal module 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<PortalEvent> Events(int moduleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return eventsDAO.GetEvents(moduleId);
        }

        /// <summary>
        /// details about a specific event 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalEvent SingleEvent(int itemId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return eventsDAO.GetSingleEvent(itemId);
        }

        /// <summary>
        /// details about a specific htmltext item 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public PortalHtmlText HtmlText(int moduleId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            return htmlTextDAO.GetHtmlText(moduleId);
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteAnnouncement(int itemID)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                announcementsDAO.DeleteAnnouncement(itemID);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int AddAnnouncement(PortalAnnouncement announcement)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            int retval;
            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                retval =
                    announcementsDAO.AddAnnouncement(announcement.ModuleID, announcement.ItemID,
                                                     announcement.CreatedByUser,
                                                     announcement.Title, announcement.ExpireDate.Value,
                                                     announcement.Description, announcement.MoreLink,
                                                     announcement.MobileMoreLink);
                transaction.Complete();
            }
            return retval;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateAnnouncement(PortalAnnouncement announcement)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                announcementsDAO.UpdateAnnouncement(announcement.ModuleID, announcement.ItemID,
                                                    announcement.CreatedByUser,
                                                    announcement.Title, announcement.ExpireDate.Value,
                                                    announcement.Description, announcement.MoreLink,
                                                    announcement.MobileMoreLink);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteContact(int itemID)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                contactsDAO.DeleteContact(itemID);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int AddContact(PortalContact contact)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            int retval;
            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                retval = contactsDAO.AddContact(contact.ModuleID, contact.ItemID, contact.CreatedByUser,
                                                contact.Name, contact.Role, contact.Email,
                                                contact.Contact1, contact.Contact2);
                transaction.Complete();
            }
            return retval;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateContact(PortalContact contact)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                contactsDAO.UpdateContact(contact.ModuleID, contact.ItemID, contact.CreatedByUser,
                                          contact.Name, contact.Role, contact.Email,
                                          contact.Contact1, contact.Contact2);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int AddMessage(PortalDiscussion discussion, int parentId)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            int retval;
            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                retval = discussionDAO.AddMessage(discussion.ModuleID, parentId,
                                                  discussion.CreatedByUser, discussion.Title, discussion.Body);
                transaction.Complete();
            }
            return retval;
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteDocument(int itemID)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                documentDAO.DeleteDocument(itemID);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateDocument(PortalDocument doc)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                documentDAO.UpdateDocument(doc.ModuleID, doc.ItemID, doc.CreatedByUser, doc.FileFriendlyName,
                                           doc.FileNameUrl, doc.Category,
                                           doc.Content, doc.ContentSize.Value, doc.ContentType);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteEvent(int itemID)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                eventsDAO.DeleteEvent(itemID);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int AddEvent(PortalEvent portalevent)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            int retval;
            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                retval = eventsDAO.AddEvent(portalevent.ModuleID, portalevent.ItemID, portalevent.CreatedByUser,
                                            portalevent.Title, portalevent.ExpireDate.Value,
                                            portalevent.Description, portalevent.WhereWhen);
                transaction.Complete();
            }
            return retval;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateEvent(PortalEvent portalevent)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                eventsDAO.UpdateEvent(portalevent.ModuleID, portalevent.ItemID, portalevent.CreatedByUser,
                                      portalevent.Title, portalevent.ExpireDate.Value,
                                      portalevent.Description, portalevent.WhereWhen);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateHtmlText(PortalHtmlText html)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                htmlTextDAO.UpdateHtmlText(html.ModuleID, html.DesktopHtml, html.MobileSummary, html.MobileDetails);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteLink(int itemID)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                linkDAO.DeleteLink(itemID);
                transaction.Complete();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public int AddLink(PortalLink link)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            int retval;
            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                retval =
                    linkDAO.AddLink(link.ModuleID, link.ItemID, link.CreatedByUser, link.Title, link.Url, link.MobileUrl,
                                    link.ViewOrder.Value, link.Description);
                transaction.Complete();
            }
            return retval;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateLink(PortalLink link)
        {
            // TODO: add access security here..
            // TODO: add argument validation here..

            // Run within the context of a database transaction.
            // The Decorator Design Pattern.
            using (TransactionDecorator transaction = new TransactionDecorator())
            {
                linkDAO.UpdateLink(link.ModuleID, link.ItemID, link.CreatedByUser, link.Title, link.Url, link.MobileUrl,
                                   link.ViewOrder.Value, link.Description);
                transaction.Complete();
            }
        }
    }
}