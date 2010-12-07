using System.Collections.Generic;
using System.ComponentModel;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;

namespace Nairc.KPWPortal.BusinessLayer.Facade
{
    public interface IDesktopModulesFacade
    {
        /// <summary>
        /// Gets a list of announcements in a given moduleId
        /// </summary>        
        /// <returns>list of announcements</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalAnnouncement> Announcements(int moduleId);

        /// <summary>
        /// Gets an announcement for a given itemId
        /// </summary>        
        /// <returns>announcement</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalAnnouncement SingleAnnouncement(int itemId);

        /// <summary>
        /// all of the contacts for a specific portal module 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns>list of contacts</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalContact> Contacts(int moduleId);

        /// <summary>
        /// details about a specific contact 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>contact</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalContact SingleContact(int itemId);

        /// <summary>
        /// details for all of the messages in the discussion specified by ModuleID
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalDiscussion> TopLevelMessages(int moduleId);

        /// <summary>
        /// details for all of the messages the thread, as identified by the Parent id string
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalDiscussion> ThreadMessages(string parent);

        /// <summary>
        /// details for the message specified by the itemId 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalDiscussion SingleMessage(int itemId);

        /// <summary>
        /// all of the links for a specific portal module 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalLink> Links(int moduleId);

        /// <summary>
        /// details about a specific link 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalLink SingleLink(int itemId);

        /// <summary>
        /// all of the documents for a specific portal module 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalDocument> Documents(int moduleId);

        /// <summary>
        /// details about a specific document
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalDocument SingleDocument(int itemId);

        /// <summary>
        /// the contents of the specified document 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalDocument DocumentContent(int itemId);

        /// <summary>
        /// all of the events for a specific portal module 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        IList<PortalEvent> Events(int moduleId);

        /// <summary>
        /// details about a specific event 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalEvent SingleEvent(int itemId);

        /// <summary>
        /// details about a specific htmltext item 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        PortalHtmlText HtmlText(int moduleId);

        [DataObjectMethod(DataObjectMethodType.Delete)]
        void DeleteAnnouncement(int itemID);

        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddAnnouncement(PortalAnnouncement announcement);

        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateAnnouncement(PortalAnnouncement announcement);

        [DataObjectMethod(DataObjectMethodType.Delete)]
        void DeleteContact(int itemID);

        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddContact(PortalContact contact);

        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateContact(PortalContact contact);

        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddMessage(PortalDiscussion discussion, int parentId);

        [DataObjectMethod(DataObjectMethodType.Delete)]
        void DeleteDocument(int itemID);

        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateDocument(PortalDocument doc);

        [DataObjectMethod(DataObjectMethodType.Delete)]
        void DeleteEvent(int itemID);

        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddEvent(PortalEvent portalevent);

        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateEvent(PortalEvent portalevent);

        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateHtmlText(PortalHtmlText html);

        [DataObjectMethod(DataObjectMethodType.Delete)]
        void DeleteLink(int itemID);

        [DataObjectMethod(DataObjectMethodType.Insert)]
        int AddLink(PortalLink link);

        [DataObjectMethod(DataObjectMethodType.Update)]
        void UpdateLink(PortalLink link);
    }
}