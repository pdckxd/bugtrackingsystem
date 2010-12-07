<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.EditLinks" CodeBehind="EditLinks.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr valign="top">
                <td width="150">
                    &nbsp;
                </td>
                <td width="*">
                    <table cellspacing="0" cellpadding="0" width="500">
                        <tbody>
                            <tr>
                                <td class="Head" align="left">
                                    链接详细信息
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
                            <tr>
                                <td class="SubHead" width="100">
                                    标题:
                                </td>
                                <td rowspan="5">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="TitleField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="150"></asp:TextBox>
                                </td>
                                <td width="25" rowspan="5">
                                    &nbsp;
                                </td>
                                <td class="Normal" width="250">
                                    <asp:RequiredFieldValidator ID="Req1" runat="server" Display="Static" ErrorMessage="You Must Enter a Valid Title"
                                        ControlToValidate="TitleField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead">
                                    Url:
                                </td>
                                <td>
                                    <asp:TextBox ID="UrlField" runat="server" CssClass="NormalTextBox" Width="390" Columns="30"
                                        MaxLength="150"></asp:TextBox>
                                </td>
                                <td class="Normal">
                                    <asp:RequiredFieldValidator ID="Req2" runat="server" Display="Static" ErrorMessage="You Must Enter a Valid URL"
                                        ControlToValidate="UrlField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead">
                                    移动页面Url:
                                </td>
                                <td>
                                    <asp:TextBox ID="MobileUrlField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="150"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead">
                                    描述:
                                </td>
                                <td>
                                    <asp:TextBox ID="DescriptionField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="150"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead">
                                    视图顺序:
                                </td>
                                <td>
                                    <asp:TextBox ID="ViewOrderField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="3"></asp:TextBox>
                                </td>
                                <td class="Normal">
                                    <asp:RequiredFieldValidator ID="RequiredViewOrder" runat="server" Display="Static"
                                        ErrorMessage="You Must Enter a Valid View Order" ControlToValidate="ViewOrderField"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="VerifyViewOrder" runat="server" Display="Static" ErrorMessage="You Must Enter a Valid View Order"
                                        ControlToValidate="ViewOrderField" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <asp:LinkButton ID="updateButton" OnClick="UpdateBtn_Click" runat="server" CssClass="CommandButton"
                            Text="Update" BorderStyle="none"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cancelButton" OnClick="CancelBtn_Click" runat="server" CssClass="CommandButton"
                            Text="Cancel" BorderStyle="none" CausesValidation="False"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="deleteButton" OnClick="DeleteBtn_Click" runat="server" CssClass="CommandButton"
                            Text="Delete this item" BorderStyle="none" CausesValidation="False"></asp:LinkButton>
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
