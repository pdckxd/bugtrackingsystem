<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesktopModuleTitle.ascx.cs" Inherits="WebApplication.DesktopModuleTitle" %>

<table cellspacing="0" cellpadding="0" width="98%">
    <tbody>
        <tr>
            <td align="left">
                <h2 style="color:#192666;"><asp:label id="ModuleTitle" EnableViewState="false" runat="server"></asp:label>
                </h2>
            </td>
            <td align="right">
                <asp:hyperlink id="EditButton" cssclass="CommandButton" EnableViewState="false" runat="server"></asp:hyperlink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr noshade="noshade" size="1" />
            </td>
        </tr>
    </tbody>
</table>
