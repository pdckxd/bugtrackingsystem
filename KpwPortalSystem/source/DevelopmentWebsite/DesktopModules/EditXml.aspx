<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.EditXml" CodeBehind="EditXml.aspx.cs"
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
                                    XML 设置
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="500">
                        <tbody>
                            <tr valign="top">
                                <td class="SubHead" width="100">
                                    XML 数据文件:
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="XmlDataSrc" runat="server" Width="340" Columns="26" CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    XSL/T 转换文件:
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="XslTransformSrc" runat="server" Width="340" Columns="26" CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <asp:LinkButton class="CommandButton" ID="updateButton" OnClick="UpdateBtn_Click"
                            runat="server" BorderStyle="none" Text="Update"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton class="CommandButton" ID="cancelButton" OnClick="CancelBtn_Click"
                            runat="server" BorderStyle="none" Text="Cancel" CausesValidation="False"></asp:LinkButton>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
