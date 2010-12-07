<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<%@ import Namespace="Nairc.KPWPortal" %>
<script runat="server">

    public String Text;
    
    //*********************************************************************
    //
    // Page_Load Event Handler
    //
    // The Page_Load event handler executes after the user control is loaded
    // and inserted into the control tree.
    //
    // The Page_Load event handler checks to see if
    //
    //*********************************************************************
    
    
    void Page_Load(Object sender, EventArgs e) {
    
        if (Text == null) {
    
            // If the Text property has not been explicitly specified,
            // walk the parent control chain to find a MobilePortalModuleControl,
            // and obtain the title from the corresponding module.
    
            MobilePortalModuleControl module = null;
            Control control = this;
    
            while (module == null && (control = control.Parent) != null) {
                module = control as MobilePortalModuleControl;
            }
    
            Text = module.ModuleTitle;
        }
    
        // Databind the User control.
        DataBind();
    }

    //******************************************************************************
    //
    // The MobileModuleTitle User Control is responsible for displaying the title of 
    // each portal module within the mobile portal. It include device-specific
    // templates for richer rendering of the title on Pocket PCs.
    //
    //******************************************************************************


</script>

<mobile:Panel id="Panel1" runat="server">
    <DEVICESPECIFIC>
        <CHOICE filter="isJScript">
            <CONTENTTEMPLATE>
                <font face="Verdana" color="#666633" size="-1"><b><%# Text %></b></font>
                <br />
                <hr color="#666633" noshade="noshade" size="1" />
            </CONTENTTEMPLATE>
        </CHOICE>
    </DEVICESPECIFIC>
    <mobile:Label id="Label1" runat="server" ForeColor="#666633" Font-Size="Large" Font-Bold="True">
        <%# Text %> 
    </mobile:Label>
</mobile:Panel>
