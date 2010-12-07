<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.XmlModule" CodeBehind="XmlModule.ascx.cs" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="UserControl1" runat="server" EditUrl="~/DesktopModules/EditXml.aspx" EditText="Edit"></ASPNETPortal:title>

<span class="Normal">
<asp:xml id="xml1" runat="server"></asp:xml>
</span>