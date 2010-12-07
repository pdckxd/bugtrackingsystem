<%@ Control Inherits="Admin.Web.SiteSettings" CodeBehind="SiteSettings.ascx.cs" Language="C#" AutoEventWireup="true" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="UserControl1" runat="server"></ASPNETPortal:title>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr>
            <td class="Normal" width="100">
                վ�����: 
            </td>
            <td class="NormalTextBox" colspan="2">
                <asp:Textbox id="siteName" runat="server" width="240"></asp:Textbox>
            </td>
        </tr>
        <tr>
            <td class="Normal">
                ������ʾ�༭��ť?: 
            </td>
            <td class="Normal" colspan="2">
                <asp:CheckBox id="showEdit" runat="server"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; 
            </td>
            <td colspan="2">
                <asp:LinkButton class="CommandButton" id="applyBtn" onclick="Apply_Click" runat="server" Text="�����޸�"></asp:LinkButton>
            </td>
        </tr>
    </tbody>
</table>