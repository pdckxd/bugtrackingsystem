<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin.Web.SecurityRoles" CodeBehind="SecurityRoles.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr valign="top" height="*">
                <td width="100">
                    &nbsp;
                </td>
                <td width="*">
                    <table cellspacing="4" cellpadding="2" width="450" border="0">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="left">
                                                    <span class="Head" id="title" runat="server">角色关系</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <hr noshade="noshade" size="1" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:Label ID="Message" runat="server" CssClass="NormalRed"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="windowsUserName" runat="server" Text="DOMAIN\username" Visible="False"></asp:TextBox>
                                                </td>
                                                <td class="Normal">
                                                    <asp:LinkButton ID="addNew" OnClick="AddUser_Click" runat="server" Text="新建一个用户并加入该角色"
                                                        Visible="False" CssClass="CommandButton"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="allUsers" runat="server" DataTextField="Email" DataValueField="UserID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="addExisting" OnClick="AddUser_Click" runat="server" Text="增加已存在用户到该角色"
                                                        CssClass="CommandButton"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:DataList ID="usersInRole" runat="server" RepeatColumns="2" OnItemCommand="usersInRole_ItemCommand"
                                        DataKeyField="UserId">
                                        <ItemStyle Width="225" />
                                        <ItemTemplate>
                                            &nbsp;&nbsp;<asp:ImageButton ImageUrl="~/images/delete.gif" CommandName="delete"
                                                AlternateText="从角色中删除该用户" runat="server" />
                                            <asp:Label Text='<%#DataBinder.Eval(Container.DataItem, "Email")%>' CssClass="Normal"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:LinkButton class="CommandButton" ID="saveBtn" OnClick="Save_Click" runat="server"
                                        Text="保存修改"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
