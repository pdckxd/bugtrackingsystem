<%@ Import namespace="Nairc.KPWPortal.DataLayer.DataObjects"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nairc.KPWPortal.MobilePortalModuleControl" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/MobileModuleTitle.ascx" %>
<%@ Register TagPrefix="ASPNETPortal" Namespace="Nairc.KPWPortal.MobileControls" Assembly="ASPNETPortal" %>
<%@ import Namespace="System.Data" %>
<script runat="server">

    DataSet ds = null;
         int currentIndex = 0;
    
         //*********************************************************************
         //
         // Page_Load Event Handler
         //
         // The Page_Load event handler on this User Control is used to
         // obtain a DataSet of announcement information from the Announcements
         // table, and then databind the results to the module contents.  It uses
         // the Nairc.KPWPortal.AnnouncementsDB() data component
         // to encapsulate all data functionality.
         //
         //*******************************************************
    
         void Page_Load(Object sender, EventArgs e) {
    
             // Obtain announcement information from Announcements table             
             ds = DataAccess.AnnouncementsDB.GetAnnouncements(ModuleId);
    
             // DataBind the User Control
             DataBind();
         }
    
         //*********************************************************************
         //
         // SummaryView_OnItemCommand Event Handler
         //
         // The SummaryView_OnItemCommand event handler is called when the user
         // clicks on a "More" link in the summary view. It calls the
         // ShowAnnouncementDetails utility method to show details of the
         // announcement.
         //
         //*********************************************************************
    
         void SummaryView_OnItemCommand(Object sender, RepeaterCommandEventArgs e) {
             ShowAnnouncementDetails(e.Item.ItemIndex);
         }
    
         //*********************************************************************
         //
         // AnnouncementsList_OnItemCommand Event Handler
         //
         // The AnnouncementsList_OnItemCommand event handler is called when the user
         // clicks on an item in the list of announcements. It calls the
         // ShowAnnouncementDetails utility method to show details of the
         // announcement.
         //
         //*********************************************************************
    
         void AnnouncementsList_OnItemCommand(Object sender, ListCommandEventArgs e) {
             ShowAnnouncementDetails(e.ListItem.Index);
         }
    
         //*********************************************************************
         //
         // DetailsView_OnClick Event Handler
         //
         // The DetailsView_OnClick event handler is called when the user
         // clicks in the details view to return to the summary view.
         //
         //*********************************************************************
    
         void DetailsView_OnClick(Object sender, EventArgs e) {
    
             // Make the parent tab show module summaries again.
             Tab.SummaryView = true;
         }
    
         //*********************************************************************
         //
         // ShowAnnouncementDetails Method
         //
         // The ShowAnnouncementDetails method sets the active pane of
         // the module to the details view, and shows the details of the
         // given item.
         //
         //*********************************************************************
    
         void ShowAnnouncementDetails(int itemIndex) {
    
             currentIndex = itemIndex;
    
             // Switch the visible pane of the multi-panel view to show
             // announcement details
             MainView.ActivePane = AnnouncementDetails;
    
             // Rebind the details panel
             AnnouncementDetails.DataBind();
    
             // Make the parent tab switch to details mode, showing this module
             Tab.ShowDetails(this);
         }
    
         //*********************************************************************
         //
         // FormatChildField Method
         //
         // The FormatChildField method returns the selected field as a string,
         // if the row is not empty.  If empty, it returns String.Empty.
         //
         //*********************************************************************
    
         string FormatChildField (string fieldName) {
    
             if (ds.Tables[0].Rows.Count > 0)
                 return ds.Tables[0].Rows[currentIndex][fieldName].ToString();
             else
                 return String.Empty;
         }
    
    
         //***************************************************************************
         //
         // The Announcements Mobile User Control renders announcement modules in the
         // portal for mobile devices.
         //
         // The control consists of two pieces: a summary panel that is rendered when
         // portal view shows a summarized view of all modules, and a multi-part panel
         // that renders the module details.
         //
         //***************************************************************************

</script>
<mobile:Panel id="summary" runat="server">
    <mobile:DeviceSpecific id="DeviceSpecific1" runat="server">
        <Choice filter="isJScript">
            <CONTENTTEMPLATE>
                <ASPNETPortal:TITLE runat="server" />
                <font face="Verdana" size="-2"> 
                <asp:Repeater id="announcementList" runat="server" OnItemCommand="SummaryView_OnItemCommand" DataSource="<%# ds %>">
                    <ItemTemplate>
                        <asp:LinkButton runat="server">
                            <%# DataBinder.Eval(Container.DataItem, "Title") %> 
                        </asp:LinkButton>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                </font> 
                <br />
            </CONTENTTEMPLATE>
        </Choice>
    </mobile:DeviceSpecific>
</mobile:Panel>
<ASPNETPORTAL:MULTIPANEL id="MainView" runat="server" Font-Name="Verdana" Font-Size="Small">
    <ASPNETPORTAL:CHILDPANEL id="AnnouncementsList" runat="server">
        <ASPNETPORTAL:TITLE runat="server" />
        <mobile:List id="List1" runat="server" DataTextField="Title" DataSource="<%# ds %>" OnItemCommand="AnnouncementsList_OnItemCommand"></mobile:List>
    </ASPNETPORTAL:CHILDPANEL>
    <ASPNETPORTAL:CHILDPANEL id="AnnouncementDetails" runat="server">
        <ASPNETPORTAL:TITLE runat="server" Text='<%# FormatChildField("Title") %>' />
        <mobile:TextView id="TextView1" runat="server" Text='<%# FormatChildField("Description") %>'></mobile:TextView>
        <mobile:Link id="Link1" runat="server" Text="read more" NavigateUrl='<%# FormatChildField("MobileMoreLink") %>' Visible='<%# FormatChildField("MobileMoreLink") != String.Empty %>'></mobile:Link>
        <ASPNETPORTAL:LINKCOMMAND onclick="DetailsView_OnClick" runat="server" Text="back" />
    </ASPNETPORTAL:CHILDPANEL>
</ASPNETPORTAL:MULTIPANEL>
