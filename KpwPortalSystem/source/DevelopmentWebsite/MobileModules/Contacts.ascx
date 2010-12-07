<%@ Import namespace="Nairc.KPWPortal.DataLayer.DataObjects"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nairc.KPWPortal.MobilePortalModuleControl" Debug="true" %>
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
    // obtain a DataSet of contact information from the Contacts
    // database, and then databind the results to the module contents.
    //
    //*********************************************************************
    
    void Page_Load(Object sender, EventArgs e) {
    
        // Obtain announcement information from Contacts table        
        ds = DataAccess.ContactsDB.GetContacts(ModuleId);
    
        // DataBind User Control
        DataBind();
    }
    
    //*********************************************************************
    //
    // SummaryView_OnClick Event Handler
    //
    // The SummaryView_OnClick event handler is called when the user
    // clicks on the link in the summary view. It shows the list of
    // contacts.
    //
    //*********************************************************************
    
    void SummaryView_OnClick(Object sender, EventArgs e) {
    
        // Switch the visible pane of the multi-panel view to show
        // the list of contacts.
        MainView.ActivePane = ContactsList;
    
        // Make the parent tab switch to details mode, showing this module.
        Tab.ShowDetails(this);
    }
    
    //*********************************************************************
    //
    // ContactsList_OnItemCommand Event Handler
    //
    // The ContactsList_OnItemCommand event handler is called when the user
    // clicks on a contact in the contact list. It shows the details of the
    // contact.
    //
    //*********************************************************************
    
    void ContactsList_OnItemCommand(Object sender, ListCommandEventArgs e) {
    
        currentIndex = e.ListItem.Index;
        ContactDetails.DataBind();
    
        // Switch the visible pane of the multi-panel view to show
        // contact details.
        MainView.ActivePane = ContactDetails;
    
        // rebind the details panel
        ContactDetails.DataBind();
    }
    
    //*********************************************************************
    //
    // DetailsView_OnClick Event Handler
    //
    // The DetailsView_OnClick event handler is called when the user
    // clicks on a link in the contact details view to return to the
    // list of contacts.
    //
    //*********************************************************************
    
    void DetailsView_OnClick(Object sender, EventArgs e) {
    
        ContactsList.DataBind();
    
        // Switch the visible pane of the multi-panel view to show
        // the list of contacts.
        MainView.ActivePane = ContactsList;
    }
    
    
    //*********************************************************************
    //
    // GetPhoneNumber Method
    //
    // The GetPhoneNumber method extracts a phone number from a contact
    // entry, if possible, using a regular expression search.
    //
    //*********************************************************************
    
    private String GetPhoneNumber(String s) {
    
        if (s != String.Empty) {
    
            // Look for a phone number.
            Match phoneMatch = Regex.Match(s, "\\+?[\\d\\(\\)\\.-]+");
            s = phoneMatch.Success ? phoneMatch.ToString() : String.Empty;
        }
    
        return s;
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

    //****************************************************************************
    //
    // The Contacts Mobile User Control renders Contacts modules in the
    // mobile portal. 
    //
    // The control consists of two pieces: a summary panel that is rendered when
    // portal view shows a summarized view of all modules, and a multi-part panel 
    // that renders the module details.
    //
    //****************************************************************************

</script>

<mobile:Panel id="summary" runat="server">
    <mobile:DeviceSpecific id="DeviceSpecific1" runat="server">
        <Choice filter="isJScript">
            <CONTENTTEMPLATE>
                <ASPNETPortal:TITLE runat="server" />
                <font face="Verdana" size="-2">Click <a runat="server" onserverclick="SummaryView_OnClick">here</a> to
                access the directory of contacts. </font> 
                <br />
            </CONTENTTEMPLATE>
        </Choice>
    </mobile:DeviceSpecific>
</mobile:Panel>
<ASPNETPORTAL:MULTIPANEL id="MainView" runat="server" Font-Name="Verdana" Font-Size="Small">
    <ASPNETPORTAL:CHILDPANEL id="ContactsList" runat="server">
        <ASPNETPORTAL:TITLE runat="server" />
        <mobile:List id="List1" runat="server" OnItemCommand="ContactsList_OnItemCommand" DataTextField="Name" DataSource="<%# ds %>">
            <DeviceSpecific>
                <Choice filter="isJScript">
                    <HeaderTemplate>
                        <table width="95%" border="0" cellspacing="0" cellpadding="0">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <font face="Verdana" size="-2"><a href='<%# "mailto:" + DataBinder.Eval(((MobileListItem)Container).DataItem, "Email") %>'> <%# DataBinder.Eval(((MobileListItem)Container).DataItem, "Name") %> </a></font> 
                            </td>
                            <td align="right">
                                <font face="Verdana" size="-2"> 
                                <asp:LinkButton runat="server" Text="more" />
                                </font> 
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </Choice>
            </DeviceSpecific>
        </mobile:List>
    </ASPNETPORTAL:CHILDPANEL>
    <ASPNETPORTAL:CHILDPANEL id="ContactDetails" runat="server">
        <ASPNETPORTAL:TITLE runat="server" Text='<%# FormatChildField("Name") %>' />
        <b>Role: </b>
        <mobile:Label id="Label1" runat="server" Text='<%# FormatChildField("Role") %>'></mobile:Label>
        <b>Email: </b>
        <mobile:Link id="Link1" runat="server" Text='<%# FormatChildField("Email") %>' NavigateUrl='<%# "mailto:" + FormatChildField("Email") %>'></mobile:Link>
        <b>Contacts: </b>
        <br />
        <mobile:PhoneCall id="PhoneCall1" runat="server" Text='<%# FormatChildField("Contact1") %>' Visible='<%# FormatChildField("Contact1") != String.Empty %>' PhoneNumber='<%# GetPhoneNumber(FormatChildField("Contact1")) %>' AlternateFormat="{0}"></mobile:PhoneCall>
        <mobile:PhoneCall id="PhoneCall2" runat="server" Text='<%# FormatChildField("Contact2") %>' Visible='<%# FormatChildField("Contact2") != String.Empty %>' PhoneNumber='<%# GetPhoneNumber(FormatChildField("Contact2")) %>' AlternateFormat="{0}"></mobile:PhoneCall>
        <ASPNETPORTAL:LINKCOMMAND onclick="DetailsView_OnClick" runat="server" Text="back to list" />
    </ASPNETPORTAL:CHILDPANEL>
</ASPNETPORTAL:MULTIPANEL>
