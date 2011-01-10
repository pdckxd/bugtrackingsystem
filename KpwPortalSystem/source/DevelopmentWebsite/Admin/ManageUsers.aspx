<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin.Web.ManageUsers" CodeBehind="ManageUsers.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="450" border="0">
        <tbody>
            <tr valign="top" height="*">
                <td colspan="2">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <span class="Head" id="title" runat="server">�����û�</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr noshade size="1">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Normal">
                    �����ʼ������û�����:
                </td>
                <td>
                    <asp:TextBox ID="Email" runat="server" Width="200" CssClass="NormalTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Normal">
                    ����:
                </td>
                <td>
                    <asp:TextBox ID="Password" Width="200" CssClass="NormalTextBox" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="Password" CssClass="NormalRed" Display="Dynamic" ValidationGroup="PasswordGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="Normal">
                    ȷ������:
                </td>
                <td>
                    <asp:TextBox ID="ConfirmPassword" Width="200" CssClass="NormalTextBox" runat="server"
                        TextMode="Password" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        ControlToValidate="ConfirmPassword" CssClass="NormalRed" Display="Dynamic" ValidationGroup="PasswordGroup"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ConfirmPassword"
                        ControlToCompare="Password" CssClass="NormalRed" Display="Dynamic" ValidationGroup="PasswordGroup"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:LinkButton ID="Linkbutton1" OnClick="UpdateUser_Click" runat="server" CssClass="CommandButton" ValidationGroup="PasswordGroup"
                        Text="Ӧ���û��������޸�"></asp:LinkButton>
                    <br>
                    <br>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DropDownList ID="allRoles" runat="server" DataTextField="RoleName" DataValueField="RoleID">
                    </asp:DropDownList>
                    &nbsp;<asp:LinkButton ID="addExisting" OnClick="AddRole_Click" runat="server" CssClass="CommandButton"
                        Text="Add user to this role" CausesValidation="False">����û����ý�ɫ��</asp:LinkButton>
                </td>
            </tr>
            <tr valign="top">
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:DataList ID="userRoles" runat="server" RepeatColumns="2" OnItemCommand="UserRoles_ItemCommand"
                        DataKeyField="RoleId">
                        <ItemStyle Width="225" />
                        <ItemTemplate>
                            &nbsp;&nbsp;
                            <asp:ImageButton ImageUrl="~/images/delete.gif" CommandName="delete" AlternateText="�Ӹý�ɫ��ɾ�����û�"
                                runat="server" ID="Imagebutton1" />
                            <asp:Label Text='<%#DataBinder.Eval(Container.DataItem, "RoleName")%>' CssClass="Normal"
                                runat="server" ID="Label1" />
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr noshade size="1">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:LinkButton class="CommandButton" ID="saveBtn" OnClick="Save_Click" runat="server"
                        Text="Save User Changes" CausesValidation="False">�����û��޸�</asp:LinkButton>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
