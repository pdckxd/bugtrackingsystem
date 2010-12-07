<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.EditHtml" CodeBehind="EditHtml.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr valign="top">
                <td width="100">
                    &nbsp;
                </td>
                <td width="*">
                    <table cellspacing="0" cellpadding="0" width="750">
                        <tbody>
                            <tr>
                                <td class="Head" align="left">
                                    Html 设置
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="720">
                        <tbody>
                            <tr valign="top">
                                <td class="SubHead">
                                    Html 内容:
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="DesktopText" runat="server" Columns="75" Width="650" Rows="12" TextMode="multiline"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    移动网页简述 (可选):
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="MobileSummary" runat="server" Columns="75" Width="650" Rows="3"
                                        TextMode="multiline"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    移动网页详细信息 (可选):
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="MobileDetails" runat="server" Columns="75" Width="650" Rows="5"
                                        TextMode="multiline"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <asp:LinkButton class="CommandButton" ID="updateButton" OnClick="UpdateBtn_Click"
                            runat="server" Text="更新" BorderStyle="none"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton class="CommandButton" ID="cancelButton" OnClick="CancelBtn_Click"
                            runat="server" Text="取消" BorderStyle="none" CausesValidation="False"></asp:LinkButton>
                        &nbsp;
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
