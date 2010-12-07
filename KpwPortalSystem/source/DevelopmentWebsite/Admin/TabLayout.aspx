<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin.Web.TabLayout" CodeBehind="TabLayout.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%">
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
                                                    Tab名称和布局
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
                                <td class="Normal" width="100">
                                    Tab名:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="tabName" runat="server" Width="300" OnTextChanged="TabSettings_Change"
                                        CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Normal" nowrap="nowrap">
                                    已授权角色:
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList ID="authRoles" runat="server" Width="300" OnSelectedIndexChanged="TabSettings_Change"
                                        RepeatColumns="2" Font-Names="Verdana,Arial" Font-Size="8pt">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td class="Normal" nowrap="nowrap">
                                    Show to mobile users?:
                                </td>
                                <td colspan="3">
                                    <asp:CheckBox ID="showMobile" runat="server" Font-Names="Verdana,Arial" Font-Size="8pt"
                                        OnCheckedChanged="TabSettings_Change"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td class="Normal" nowrap="nowrap">
                                    Mobile Tab Name:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="mobileTabName" runat="server" Width="300" OnTextChanged="TabSettings_Change"
                                        CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Normal">
                                    新增模块:
                                </td>
                                <td class="Normal">
                                    模块类型
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="moduleType" runat="server" DataValueField="ModuleDefID" DataTextField="FriendlyName">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="Normal">
                                    模块名:
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="moduleTitle" runat="server" Width="250" CssClass="NormalTextBox"
                                        EnableViewState="false" Text="新模块名"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <asp:LinkButton class="CommandButton" ID="LinkButton1" OnClick="AddModuleToPane_Click"
                                        runat="server" Text='<img src="../images/dn.gif" border=0> 增加到下面"组织模块"'></asp:LinkButton>
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
                            <tr valign="top">
                                <td class="Normal">
                                    组织模块:
                                </td>
                                <td width="120">
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="NormalBold">
                                                    &nbsp;左边栏
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    <table cellspacing="2" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr valign="top">
                                                                <td rowspan="2">
                                                                    <asp:ListBox ID="leftPane" runat="server" Width="110" DataValueField="ModuleId" DataTextField="ModuleTitle"
                                                                        DataSource="<%#leftList%>" Rows="7"></asp:ListBox>
                                                                </td>
                                                                <td valign="top" nowrap="nowrap">
                                                                    <asp:ImageButton ID="ImageButton1" OnClick="UpDown_Click" runat="server" ImageUrl="~/images/up.gif"
                                                                        CommandName="up" CommandArgument="LeftPane" AlternateText="向上移动所选模块">
                                                                    </asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton2" OnClick="RightLeft_Click" runat="server" ImageUrl="~/images/rt.gif"
                                                                        CommandName="right" AlternateText="移动所选模块到内容区域"
                                                                        sourcepane="LeftPane" targetpane="ContentPane"></asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton3" OnClick="UpDown_Click" runat="server" ImageUrl="~/images/dn.gif"
                                                                        CommandName="down" CommandArgument="LeftPane" AlternateText="向下移动所选模块">
                                                                    </asp:ImageButton>
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom" nowrap="nowrap">
                                                                    <asp:ImageButton ID="ImageButton4" OnClick="EditBtn_Click" runat="server" ImageUrl="~/images/edit.gif"
                                                                        CommandName="edit" CommandArgument="LeftPane" AlternateText="编辑该项"></asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton5" OnClick="DeleteBtn_Click" runat="server" ImageUrl="~/images/delete.gif"
                                                                        CommandName="delete" CommandArgument="LeftPane" AlternateText="删除该项">
                                                                    </asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="*">
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="NormalBold">
                                                    &nbsp;内容区域
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="middle">
                                                    <table cellspacing="2" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr valign="top">
                                                                <td rowspan="2">
                                                                    <asp:ListBox ID="contentPane" runat="server" Width="170" DataValueField="ModuleId"
                                                                        DataTextField="ModuleTitle" DataSource="<%#contentList%>" Rows="7"></asp:ListBox>
                                                                </td>
                                                                <td valign="top" nowrap="nowrap">
                                                                    <asp:ImageButton ID="ImageButton6" OnClick="UpDown_Click" runat="server" ImageUrl="~/images/up.gif"
                                                                        CommandName="up" CommandArgument="ContentPane" AlternateText="向上移动所选模块">
                                                                    </asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton7" OnClick="RightLeft_Click" runat="server" ImageUrl="~/images/lt.gif"
                                                                        AlternateText="移动所选模块到左边栏" sourcepane="ContentPane"
                                                                        targetpane="LeftPane"></asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton8" OnClick="RightLeft_Click" runat="server" ImageUrl="~/images/rt.gif"
                                                                        AlternateText="移动所选模块到右边栏" sourcepane="ContentPane"
                                                                        targetpane="RightPane"></asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton9" OnClick="UpDown_Click" runat="server" ImageUrl="~/images/dn.gif"
                                                                        CommandName="down" CommandArgument="ContentPane" AlternateText="向下移动所选模块">
                                                                    </asp:ImageButton>
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom" nowrap="nowrap">
                                                                    <asp:ImageButton ID="ImageButton10" OnClick="EditBtn_Click" runat="server" ImageUrl="~/images/edit.gif"
                                                                        CommandName="edit" CommandArgument="ContentPane" AlternateText="编辑该项">
                                                                    </asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton11" OnClick="DeleteBtn_Click" runat="server" ImageUrl="~/images/delete.gif"
                                                                        CommandName="delete" CommandArgument="ContentPane" AlternateText="删除该项">
                                                                    </asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="120">
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="NormalBold">
                                                    &nbsp;右边栏
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellspacing="2" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr valign="top">
                                                                <td rowspan="2">
                                                                    <asp:ListBox ID="rightPane" runat="server" Width="110" DataValueField="ModuleId"
                                                                        DataTextField="ModuleTitle" DataSource="<%#rightList%>" Rows="7"></asp:ListBox>
                                                                </td>
                                                                <td valign="top" nowrap="nowrap">
                                                                    <asp:ImageButton ID="ImageButton12" OnClick="UpDown_Click" runat="server" ImageUrl="~/images/up.gif"
                                                                        CommandName="up" CommandArgument="RightPane" AlternateText="向上移动所选模块">
                                                                    </asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton13" OnClick="RightLeft_Click" runat="server" ImageUrl="~/images/lt.gif"
                                                                        AlternateText="移动所选模块到左边栏" sourcepane="RightPane"
                                                                        targetpane="ContentPane"></asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton14" OnClick="UpDown_Click" runat="server" ImageUrl="~/images/dn.gif"
                                                                        CommandName="down" CommandArgument="RightPane" AlternateText="向下移动所选模块">
                                                                    </asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom" nowrap="nowrap">
                                                                    <asp:ImageButton ID="ImageButton15" OnClick="EditBtn_Click" runat="server" ImageUrl="~/images/edit.gif"
                                                                        CommandName="edit" CommandArgument="RightPane" AlternateText="编辑该项"></asp:ImageButton>
                                                                    <br />
                                                                    <asp:ImageButton ID="ImageButton16" OnClick="DeleteBtn_Click" runat="server" ImageUrl="~/images/delete.gif"
                                                                        CommandName="delete" CommandArgument="RightPane" AlternateText="删除该项">
                                                                    </asp:ImageButton>
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
                            <tr>
                                <td colspan="4">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:LinkButton class="CommandButton" ID="applyBtn" OnClick="Apply_Click" runat="server"
                                        Text="应用修改"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
