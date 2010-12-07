<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.HtmlModule" CodeBehind="HtmlModule.ascx.cs" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="UserControl1" runat="server" EditUrl="~/DesktopModules/EditHtml.aspx" EditText="±à¼­"></ASPNETPortal:title>
<table id="t1" cellspacing="0" cellpadding="0" runat="server">
    <tbody>
        <tr valign="top">
            <td id="HtmlHolder" runat="server">
            </td>
        </tr>
    </tbody>
</table>