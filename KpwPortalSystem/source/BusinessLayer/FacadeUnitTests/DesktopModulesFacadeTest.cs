using System;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;
using NUnit.Framework;

namespace Nairc.KPWPortal.BusinessLayer.FacadeUnitTests
{    
    [TestFixture]
    public class DesktopModulesFacadeTest 
    {

        #region IDesktopModulesFacade Tests
        [Test]
        public void AnnouncementsTest()
        {
            //IList<PortalAnnouncement> Announcements(int moduleId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.Announcements(0);
        }

        [Test]
        public void SingleAnnouncementTest()
        {
            //PortalAnnouncement SingleAnnouncement(int itemId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.SingleAnnouncement(0);
        }

        [Test]
        public void ContactsTest()
        {
            //IList<PortalContact> Contacts(int moduleId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.Contacts(0);
        }

        [Test]
        public void SingleContactTest()
        {
            //PortalContact SingleContact(int itemId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.SingleContact(0);
        }

        [Test]
        public void TopLevelMessagesTest()
        {
            //IList<PortalDiscussion> TopLevelMessages(int moduleId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.TopLevelMessages(0);
        }

        [Test]
        public void ThreadMessagesTest()
        {
            //IList<PortalDiscussion> ThreadMessages(string parent)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.ThreadMessages("p");
        }

        [Test]
        public void SingleMessageTest()
        {
            //PortalDiscussion SingleMessage(int itemId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.SingleMessage(0);
        }

        [Test]
        public void LinksTest()
        {
            //IList<PortalLink> Links(int moduleId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.Links(0);
        }

        [Test]
        public void SingleLinkTest()
        {
            //PortalLink SingleLink(int itemId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.SingleLink(0);
        }

        [Test]
        public void DocumentsTest()
        {
            //IList<PortalDocument> Documents(int moduleId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.Documents(0);
        }

        [Test]
        public void SingleDocumentTest()
        {
            //PortalDocument SingleDocument(int itemId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.SingleDocument(0);
        }

        [Test]
        public void DocumentContentTest()
        {
            //PortalDocument DocumentContent(int itemId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.DocumentContent(0);
        }

        [Test]
        public void EventsTest()
        {
            //IList<PortalEvent> Events(int moduleId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.Events(0);
        }

        [Test]
        public void SingleEventTest()
        {
            //PortalEvent SingleEvent(int itemId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.SingleEvent(0);
        }

        [Test]
        public void HtmlTextTest()
        {
            //PortalHtmlText HtmlText(int moduleId)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.HtmlText(0);
        }

        [Test]
        public void DeleteAnnouncementTest()
        {
            //void DeleteAnnouncement(int itemID)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.DeleteAnnouncement(0);
        }

        [Test]
        public void AddAnnouncementTest()
        {
            //int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
            //                String description, String moreLink, String mobileMoreLink)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalAnnouncement announcement = new PortalAnnouncement();
            announcement.ModuleID = 0;
            announcement.ItemID = 0;
            announcement.CreatedByUser = "un";
            announcement.Title = "t";           
            announcement.Description = "d";
            announcement.ExpireDate = new DateTime(2011, 1, 1);
            announcement.MoreLink = "ml";
            announcement.MobileMoreLink = "mml";
            facade.AddAnnouncement(announcement);
        }

        [Test]
        public void UpdateAnnouncementTest()
        {
            //void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
            //                    String description, String moreLink, String mobileMoreLink)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalAnnouncement announcement = new PortalAnnouncement();
            announcement.ModuleID = 0;
            announcement.ItemID = 0;
            announcement.CreatedByUser = "un";
            announcement.Title = "t";
            announcement.Description = "d";
            announcement.ExpireDate = new DateTime(2011, 1, 1);
            announcement.MoreLink = "ml";
            announcement.MobileMoreLink = "mml";
            facade.UpdateAnnouncement(announcement);
        }

        [Test]
        public void DeleteContactTest()
        {
            //void DeleteContact(int itemID)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.DeleteContact(0);
        }

        [Test]
        public void AddContactTest()
        {
            //int AddContact(int moduleId, int itemId, String userName, String name, String role, String email,
            //           String contact1, String contact2)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalContact contact = new PortalContact();
            contact.ModuleID=0;
            contact.ItemID=0;
            contact.CreatedByUser="un";
            contact.Name="n";
            contact.Role="r";
            contact.Email="e";
            contact.Contact1="c1";
            contact.Contact2="c2";            
            facade.AddContact(contact);
        }

        [Test]
        public void UpdateContactTest()
        {
            //void UpdateContact(int moduleId, int itemId, String userName, String name, String role, String email,
            //               String contact1, String contact2)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalContact contact = new PortalContact();
            contact.ModuleID = 0;
            contact.ItemID = 0;
            contact.CreatedByUser = "un";
            contact.Name = "n";
            contact.Role = "r";
            contact.Email = "e";
            contact.Contact1 = "c1";
            contact.Contact2 = "c2";   
            facade.UpdateContact(contact);
        }

        [Test]
        public void AddMessageTest()
        {
            //int AddMessage(int moduleId, int parentId, String userName, String title, String body)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalDiscussion discussion = new PortalDiscussion();
            discussion.ModuleID = 0;            
            discussion.CreatedByUser = "un";
            discussion.Title = "t";
            discussion.Body = "b";
            facade.AddMessage(discussion, 0);
        }

        [Test]
        public void DeleteDocumentTest()
        {
            //void DeleteDocument(int itemID)
            DesktopModulesFacade facade = new DesktopModulesFacade();            
            facade.DeleteDocument(0);

        }

        [Test]
        public void UpdateDocumentTest()
        {
            //void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category,
            //                byte[] content, int size, String contentType)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalDocument doc=new PortalDocument();
            doc.ModuleID = 0;
            doc.ItemID = 0;
            doc.CreatedByUser = "un";
            doc.FileFriendlyName = "n";
            doc.FileNameUrl = "url";
            doc.Category = "c";
            doc.Content = new byte[] {};
            doc.ContentSize = 0;
            doc.ContentType = "ct";
            facade.UpdateDocument(doc);

        }

        [Test]
        public void DeleteEventTest()
        {
            //void DeleteEvent(int itemID)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.DeleteEvent(0);

        }

        [Test]
        public void AddEventTest()
        {
            //int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
            //         String description, String wherewhen)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalEvent portalevent = new PortalEvent();
            portalevent.ModuleID = 0;
            portalevent.ItemID = 0;
            portalevent.CreatedByUser = "un";
            portalevent.Title = "t";
            portalevent.ExpireDate = new DateTime(2011, 1, 1);
            portalevent.Description = "d";
            portalevent.WhereWhen = "ww";
            facade.AddEvent(portalevent);
        }

        [Test]
        public void UpdateEventTest()
        {
            //void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
            //             String description, String wherewhen)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalEvent portalevent = new PortalEvent();
            portalevent.ModuleID = 0;
            portalevent.ItemID = 0;
            portalevent.CreatedByUser = "un";
            portalevent.Title = "t";
            portalevent.ExpireDate = new DateTime(2011, 1, 1);
            portalevent.Description = "d";
            portalevent.WhereWhen = "ww";
            facade.UpdateEvent(portalevent);
        }

        [Test]
        public void UpdateHtmlTextTest()
        {
            //void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalHtmlText html = new PortalHtmlText();
            html.ModuleID = 0;
            html.DesktopHtml = "dh";
            html.MobileSummary = "ms";
            html.MobileDetails = "md";
            facade.UpdateHtmlText(html);
        }

        [Test]
        public void DeleteLinkTest()
        {
            //void DeleteLink(int itemID)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            facade.DeleteLink(0);
        }

        [Test]
        public void AddLinkTest()
        {
            // int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
            //        int viewOrder, String description)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalLink link=new PortalLink();
            link.ModuleID = 0;
            link.ItemID = 0;
            link.CreatedByUser = "un";
            link.Title = "t";
            link.Url = "url";
            link.MobileUrl = "murl";
            link.ViewOrder = 0;
            link.Description = "d";
            facade.AddLink(link);
        }

        [Test]
        public void UpdateLinkTest()
        {            
            // void UpdateLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
            //            int viewOrder, String description)
            DesktopModulesFacade facade = new DesktopModulesFacade();
            PortalLink link = new PortalLink();
            link.ModuleID = 0;
            link.ItemID = 0;
            link.CreatedByUser = "un";
            link.Title = "t";
            link.Url = "url";
            link.MobileUrl = "murl";
            link.ViewOrder = 0;
            link.Description = "d";
            facade.UpdateLink(link);
        }

        #endregion
    }
}