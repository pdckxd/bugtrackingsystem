<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.EditContacts" CodeBehind="EditContacts.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr valign="top">
                <td width="150">
                    &nbsp;
                </td>
                <td>
                    <table cellspacing="0" cellpadding="0" width="500" border="0">
                        <tbody>
                            <tr>
                                <td class="Head" align="left">
                                    联系人详细信息
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="750" border="0">
                        <tbody>
                            <tr valign="top">
                                <td class="SubHead" width="100">
                                    名字:
                                </td>
                                <td rowspan="5">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="NameField" runat="server" CssClass="NormalTextBox" Width="390" Columns="30"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                <td width="25" rowspan="5">
                                    &nbsp;
                                </td>
                                <td class="Normal" width="250">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Static"
                                        ErrorMessage="You Must Enter a Valid Name" ControlToValidate="NameField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    角色:
                                </td>
                                <td>
                                    <asp:TextBox ID="RoleField" runat="server" CssClass="NormalTextBox" Width="390" Columns="30"
                                        MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    Email:
                                </td>
                                <td>
                                    <asp:TextBox ID="EmailField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    联系信息1:
                                </td>
                                <td>
                                    <asp:TextBox ID="Contact1Field" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="250"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    联系信息2:
                                </td>
                                <td>
                                    <asp:TextBox ID="Contact2Field" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="250"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <asp:LinkButton class="CommandButton" ID="updateButton" OnClick="UpdateBtn_Click"
                            runat="server" Text="Update" BorderStyle="none"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton class="CommandButton" ID="cancelButton" OnClick="CancelBtn_Click"
                            runat="server" Text="Cancel" BorderStyle="none" CausesValidation="False"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton class="CommandButton" ID="deleteButton" OnClick="DeleteBtn_Click"
                            runat="server" Text="Delete this item" BorderStyle="none" CausesValidation="False"></asp:LinkButton>
                        <hr width="500" noshade="noshade" size="1" />
                        <span class="Normal">由
                            <asp:Label ID="CreatedBy" runat="server"></asp:Label>
                            创建于
                            <asp:Label ID="CreatedDate" runat="server"></asp:Label>
                            <br />
                        </span>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
