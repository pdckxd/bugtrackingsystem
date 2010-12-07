<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.EditAnnouncements" CodeBehind="EditAnnouncements.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr valign="top">
                <td width="150">
                    &nbsp;
                </td>
                <td width="*">
                    <table cellspacing="0" cellpadding="0" width="520">
                        <tbody>
                            <tr>
                                <td class="Head" align="left">
                                    Announcement Details
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="750">
                        <tbody>
                            <tr valign="top">
                                <td class="SubHead" width="100">
                                    Title:
                                </td>
                                <td rowspan="5">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="TitleField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="100"></asp:TextBox>
                                </td>
                                <td width="25" rowspan="5">
                                    &nbsp;
                                </td>
                                <td class="Normal" width="250">
                                    <asp:RequiredFieldValidator ID="Req1" runat="server" Display="Static" ErrorMessage="You Must Enter a Valid Title"
                                        ControlToValidate="TitleField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    Read More Link:
                                </td>
                                <td>
                                    <asp:TextBox ID="MoreLinkField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead" nowrap="nowrap">
                                    Read More (Mobile):
                                </td>
                                <td>
                                    <asp:TextBox ID="MobileMoreField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    Description:
                                </td>
                                <td>
                                    <asp:TextBox ID="DescriptionField" runat="server" Width="390" Columns="44" TextMode="Multiline"
                                        Rows="6"></asp:TextBox>
                                </td>
                                <td class="Normal">
                                    <asp:RequiredFieldValidator ID="Req2" runat="server" Display="Static" ErrorMessage="You Must Enter a Valid Description"
                                        ControlToValidate="DescriptionField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    Expires:
                                </td>
                                <td>
                                    <asp:TextBox ID="ExpireField" runat="server" CssClass="NormalTextBox" Width="100"
                                        Columns="8" Text="12/31/2001"></asp:TextBox>
                                </td>
                                <td class="Normal">
                                    <asp:RequiredFieldValidator ID="RequiredExpireDate" runat="server" Display="Static"
                                        ErrorMessage="You Must Enter a Valid Expiration Date" ControlToValidate="ExpireField"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="VerifyExpireDate" runat="server" Display="Static" ErrorMessage="You Must Enter a Valid Expiration Date"
                                        ControlToValidate="ExpireField" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <asp:LinkButton ID="updateButton" OnClick="UpdateBtn_Click" runat="server" Text="Update"
                            CssClass="CommandButton" BorderStyle="none"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="cancelButton" OnClick="CancelBtn_Click" runat="server" Text="Cancel"
                            CssClass="CommandButton" BorderStyle="none" CausesValidation="False"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="deleteButton" OnClick="DeleteBtn_Click" runat="server" Text="Delete this item"
                            CssClass="CommandButton" BorderStyle="none" CausesValidation="False"></asp:LinkButton>
                        <hr width="520" noshade="noshade" size="1" />
                        <span class="Normal">Created by
                            <asp:Label ID="CreatedBy" runat="server"></asp:Label>
                            on
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
