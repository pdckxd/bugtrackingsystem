<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.EditImage" CodeBehind="EditImage.aspx.cs"
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
                                    Õº∆¨…Ë÷√
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
                                    Õº∆¨‘¥:
                                </td>
                                <td rowspan="3">
                                    &nbsp;
                                </td>
                                <td class="Normal">
                                    <asp:TextBox ID="Src" runat="server" CssClass="NormalTextBox" Width="390" Columns="30"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    Õº∆¨øÌ:
                                </td>
                                <td>
                                    <asp:TextBox ID="Width" runat="server" CssClass="NormalTextBox" Width="390" Columns="30"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    Õº∆¨∏ﬂ:
                                </td>
                                <td>
                                    <asp:TextBox ID="Height" runat="server" CssClass="NormalTextBox" Width="390" Columns="30"></asp:TextBox>
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
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
