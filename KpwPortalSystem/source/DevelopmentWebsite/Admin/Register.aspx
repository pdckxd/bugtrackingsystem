<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin.Web.Register" CodeBehind="Register.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr>
                <td width="150">
                    &nbsp;
                </td>
                <td width="*">
                    <table cellspacing="1" cellpadding="2" border="0">
                        <tbody>
                            <tr>
                                <td width="450">
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <span class="Head">新建账户 </span>
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
                            <tr valign="top">
                                <td class="Normal">
                                    名字:
                                    <br />
                                    <asp:TextBox ID="Name" runat="server" size="25"></asp:TextBox>
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="'Name' must not be left blank."
                                        ControlToValidate="Name"></asp:RequiredFieldValidator>
                                    <p>
                                        Email:
                                        <br />
                                        <asp:TextBox ID="Email" runat="server" size="25"></asp:TextBox>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Must use a valid email address."
                                            ControlToValidate="Email" Display="Dynamic" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="'Email' must not be left blank."
                                            ControlToValidate="Email"></asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        密码:
                                        <br />
                                        <asp:TextBox ID="Password" runat="server" size="25" TextMode="Password"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="'Password' must not be left blank."
                                            ControlToValidate="Password"></asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        确认密码:
                                        <br />
                                        <asp:TextBox ID="ConfirmPassword" runat="server" size="25" TextMode="Password"></asp:TextBox>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="'Confirm' must not be left blank."
                                            ControlToValidate="ConfirmPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password fields do not match."
                                            ControlToValidate="ConfirmPassword" ControlToCompare="Password"></asp:CompareValidator>
                                    </p>
                                    <p>
                                        <asp:LinkButton class="CommandButton" ID="LinkButton1" OnClick="RegisterBtn_Click"
                                            runat="server" Text="注册并登陆"></asp:LinkButton>
                                        <br />
                                        <br />
                                    </p>
                                    <p>
                                        <asp:Label ID="Message" runat="server" CssClass="NormalRed"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
