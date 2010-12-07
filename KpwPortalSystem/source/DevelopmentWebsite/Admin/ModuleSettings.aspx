<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin.Web.ModuleSettings" CodeBehind="ModuleSettings.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr valign="top">
                <td width="150">
                    &nbsp;
                </td>
                <td width="*">
                    <table cellspacing="1" cellpadding="2" border="0">
                        <tbody>
                            <tr>
                                <td colspan="4">
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="Head" align="left">
                                                    模块设置
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <hr noshade="noshade" size="1" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead" width="100">
                                    模块名:
                                </td>
                                <td colspan="3">
                                    &nbsp;<asp:TextBox ID="moduleTitle" runat="server" Width="300" CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead">
                                    缓存事件 (秒):
                                </td>
                                <td colspan="3">
                                    &nbsp;<asp:TextBox ID="cacheTime" runat="server" Width="100" CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead">
                                    具有编辑内容权限的角色:
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="authEditRoles" runat="server" Width="300" RepeatColumns="2"
                                        Font-Names="Verdana,Arial" Font-Size="8pt" CellPadding="0" CellSpacing="0">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead" nowrap="nowrap">
                                    是否显示给移动终端?:
                                </td>
                                <td colspan="3">
                                    <asp:CheckBox ID="showMobile" runat="server" Font-Names="Verdana,Arial" Font-Size="8pt">
                                    </asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:LinkButton class="CommandButton" ID="LinkButton1" OnClick="ApplyChanges_Click"
                                        runat="server" Text="保存修改"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
