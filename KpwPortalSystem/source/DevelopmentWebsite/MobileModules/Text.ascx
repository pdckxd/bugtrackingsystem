<%@ Import namespace="System.Data"%>
<%@ Import namespace="Nairc.KPWPortal.DataLayer.DataObjects"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nairc.KPWPortal.MobilePortalModuleControl" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<%@ Register TagPrefix="ASPNETPortal" Namespace="Nairc.KPWPortal.MobileControls" Assembly="ASPNETPortal" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/MobileModuleTitle.ascx" %>
<%@ import Namespace="System.Data.SqlClient" %>
<script runat="server">

    String mobileSummary = "";
    String mobileDetails = "";
    
    //*********************************************************************
    //
    // Page_Load Event Handler
    //
    // The Page_Load event handler on this User Control is used to
    // load the contents of the text message from a file, and databind
    // the message to the module contents.
    //
    //*********************************************************************
    
    void Page_Load(Object sender, EventArgs e) {
    
        // Obtain the selected item from the HtmlText table        
        IDataReader dr = DataAccess.HtmlTextDB.GetHtmlText(ModuleId);
    
        if (dr.Read()) {
    
            // Dynamically add the file content into the page
            mobileSummary = Server.HtmlDecode((String) dr["MobileSummary"]);
            mobileDetails = Server.HtmlDecode((String) dr["MobileDetails"]);
        }
    
        DataBind();
    
        // Close the datareader
        dr.Close();
    }

    //***************************************************************************
    //
    // The Text Mobile User Control renders text modules in the mobile portal. 
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
                <font face="Verdana" size="-2"><%# mobileSummary %> 
                <asp:LinkButton id="LinkButton1" runat="server" CommandName="Details" Text="more" Visible="<%# mobileDetails != String.Empty %>"></asp:LinkButton>
                </font> 
                <br />
                <br />
            </CONTENTTEMPLATE>
        </Choice>
    </mobile:DeviceSpecific>
</mobile:Panel>
<ASPNETPORTAL:TITLE runat="server" />
<mobile:TextView id="TextView1" runat="server" Font-Name="Verdana" Font-Size="Small" Text="<%# mobileDetails %>"></mobile:TextView>
<ASPNETPORTAL:LINKCOMMAND runat="server" Font-Name="Verdana" Font-Size="Small" Text="back" CommandName="summary" />
