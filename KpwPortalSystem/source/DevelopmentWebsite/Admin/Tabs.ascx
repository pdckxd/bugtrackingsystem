<%@ Control Inherits="Admin.Web.Tabs" CodeBehind="Tabs.ascx.cs" Language="C#" AutoEventWireup="true" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="Title1" runat="server"></ASPNETPortal:title>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr>
            <td colspan="2">
                <asp:LinkButton id="addBtn" onclick="AddTab_Click" runat="server" cssclass="CommandButton" Text="新增Tab"></asp:LinkButton>
            </td>
        </tr>
        <tr valign="top">
            <td width="100">
                &nbsp; 
            </td>
            <td class="Normal" width="50">
                Tabs: 
            </td>
            <td>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr valign="top">
                            <td>
                                <asp:ListBox id="tabList" runat="server" width="200" DataSource="<%#portalTabs%>" DataTextField="TabName" DataValueField="TabId" rows="5"></asp:ListBox>
                            </td>
                            <td>
                                &nbsp; 
                            </td>
                            <td>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:ImageButton id="upBtn" onclick="UpDown_Click" runat="server" ImageUrl="~/images/up.gif" CommandName="up" AlternateText="向上移动"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton id="downBtn" onclick="UpDown_Click" runat="server" ImageUrl="~/images/dn.gif" CommandName="down" AlternateText="向下移动"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton id="editBtn" onclick="EditBtn_Click" runat="server" ImageUrl="~/images/edit.gif" AlternateText="编辑tab属性"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton id="deleteBtn" onclick="DeleteBtn_Click" runat="server" ImageUrl="~/images/delete.gif" AlternateText="删除该tab"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>