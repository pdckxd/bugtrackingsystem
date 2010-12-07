<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.DiscussDetails" CodeBehind="DiscussDetails.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<%@ Import Namespace="Nairc.KPWPortal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="0" width="600">
        <tbody>
            <tr>
                <td align="left">
                    <span class="Head">Message Detail</span>
                </td>
                <td align="right">
                    <asp:Panel ID="ButtonPanel" runat="server">
                        <a class="CommandButton" id="prevItem" title="Previous Message" runat="server">
                            <img src='<%=Global.GetApplicationPath(Request) + "/images/rew.gif"%>' border="0" /></a>&nbsp;
                        <a class="CommandButton" id="nextItem" title="Next Message" runat="server">
                            <img src='<%=Global.GetApplicationPath(Request) + "/images/fwd.gif"%>' border="0" /></a>&nbsp;
                        <asp:LinkButton ID="ReplyBtn" OnClick="ReplyBtn_Click" runat="server" Text="Reply to this Message"
                            CssClass="CommandButton" EnableViewState="false"></asp:LinkButton>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr noshade="noshade" size="1" />
                </td>
            </tr>
        </tbody>
    </table>
    <asp:Panel ID="EditPanel" runat="server" Visible="false">
        <table cellspacing="0" cellpadding="4" width="600" border="0">
            <tbody>
                <tr valign="top">
                    <td class="SubHead" width="150">
                        Title:
                    </td>
                    <td rowspan="4">
                        &nbsp;
                    </td>
                    <td width="*">
                        <asp:TextBox ID="TitleField" runat="server" CssClass="NormalTextBox" Width="500"
                            Columns="40" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="top">
                    <td class="SubHead">
                        Body:
                    </td>
                    <td width="*">
                        <asp:TextBox ID="BodyField" runat="server" Width="500" Columns="59" TextMode="Multiline"
                            Rows="15"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:LinkButton class="CommandButton" ID="updateButton" OnClick="UpdateBtn_Click"
                            runat="server" Text="Submit"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton class="CommandButton" ID="cancelButton" OnClick="CancelBtn_Click"
                            runat="server" Text="Cancel" CausesValidation="False"></asp:LinkButton>
                        &nbsp;
                    </td>
                </tr>
                <tr valign="top">
                    <td class="SubHead">
                        Original Message:
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <table cellspacing="0" cellpadding="4" width="600" border="0">
        <tbody>
            <tr valign="top">
                <td class="Message" align="left">
                    <b>Subject: </b>
                    <asp:Label ID="Title" runat="server"></asp:Label>
                    <br />
                    <b>Author: </b>
                    <asp:Label ID="CreatedByUser" runat="server"></asp:Label>
                    <br />
                    <b>Date: </b>
                    <asp:Label ID="CreatedDate" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Body" runat="server"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
