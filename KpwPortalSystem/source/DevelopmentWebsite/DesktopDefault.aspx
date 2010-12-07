<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesktopDefault.aspx.cs" Inherits="WebApplication.DesktopDefault"
    MasterPageFile="~/Shared/Default.master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="100%" border="0">
        <tbody>
            <tr valign="top" height="*">
                <td width="5">
                    &nbsp;
                </td>
                <td id="LeftPane" width="170" runat="server" visible="false">
                </td>
                <td width="1">
                </td>
                <td id="ContentPane" width="*" runat="server" visible="false">
                </td>
                <td id="RightPane" width="230" runat="server" visible="false">
                </td>
                <td width="10">
                    &nbsp;
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
