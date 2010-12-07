<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoPlayer.ascx.cs" Inherits="DesktopModules.Web.VideoPlayer" %>
<asp:PlaceHolder ID="phError" runat="server" Visible="false">
<asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Error" />
</asp:PlaceHolder>
<asp:Table ID="tblPlayer" runat="server" BorderWidth="1" Width="100%" Height="100%">
<asp:TableRow>
<asp:TableCell>
<asp:Literal ID="ltVideo" runat="server" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>
