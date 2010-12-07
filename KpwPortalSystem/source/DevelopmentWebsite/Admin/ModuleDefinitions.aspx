<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin.Web.ModuleDefinitions" CodeBehind="ModuleDefinitions.aspx.cs"
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
                                    模块类型定义
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
                                    模块名字:
                                </td>
                                <td rowspan="5">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="FriendlyName" runat="server" MaxLength="150" Columns="30" Width="390"
                                        CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                                <td width="25" rowspan="5">
                                    &nbsp;
                                </td>
                                <td class="Normal" width="250">
                                    <asp:RequiredFieldValidator ID="Req1" runat="server" ControlToValidate="FriendlyName"
                                        ErrorMessage="Enter a Module NAme" Display="Static"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead" nowrap="nowrap">
                                    模块路径:
                                </td>
                                <td>
                                    <asp:TextBox ID="DesktopSrc" runat="server" MaxLength="150" Columns="30" Width="390"
                                        CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                                <td class="Normal">
                                    <asp:RequiredFieldValidator ID="Req2" runat="server" ControlToValidate="DesktopSrc"
                                        ErrorMessage="You Must Enter Source Path for the Desktop Module" Display="Static"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr visible="false" style="height:0%;">
                                <td class="SubHead">
                                    Mobile Source:
                                </td>
                                <td>
                                    <asp:TextBox ID="MobileSrc" runat="server" MaxLength="150" Columns="30" Width="390"
                                        CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <asp:LinkButton class="CommandButton" ID="updateButton" OnClick="UpdateBtn_Click"
                            runat="server" BorderStyle="none" Text="更新"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton class="CommandButton" ID="cancelButton" OnClick="CancelBtn_Click"
                            runat="server" BorderStyle="none" Text="取消" CausesValidation="False"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton class="CommandButton" ID="deleteButton" OnClick="DeleteBtn_Click"
                            runat="server" BorderStyle="none" Text="删除该模块定义" CausesValidation="False"></asp:LinkButton>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
