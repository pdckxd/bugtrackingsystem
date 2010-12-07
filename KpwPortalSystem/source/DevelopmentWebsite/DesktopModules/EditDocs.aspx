<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.EditDocs" CodeBehind="EditDocs.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr valign="top">
                <td width="150">
                    &nbsp;
                </td>
                <td>
                    <table cellspacing="0" cellpadding="0" width="500">
                        <tbody>
                            <tr>
                                <td class="Head" align="left">
                                    Document Details
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr noshade="noshade" size="1" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="726" border="0">
                        <tbody>
                            <tr valign="top">
                                <td class="SubHead" width="100">
                                    Name:
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="NameField" runat="server" CssClass="NormalTextBox" Width="353" Columns="28"
                                        MaxLength="150"></asp:TextBox>
                                </td>
                                <td width="25" rowspan="6">
                                    &nbsp;
                                </td>
                                <td class="Normal" width="250">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Static"
                                        ErrorMessage="You Must Enter a Valid Name" ControlToValidate="NameField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    Category:
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="CategoryField" runat="server" CssClass="NormalTextBox" Width="353"
                                        Columns="28" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    <hr width="100%" noshade="noshade" size="1" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead" width="100">
                                    URL to Browse:
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="PathField" runat="server" CssClass="NormalTextBox" Width="353" Columns="28"
                                        MaxLength="250"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead">
                                    or
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead" nowrap="nowrap">
                                    Upload to Web Server:&nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:CheckBox ID="Upload" runat="server" CssClass="Normal" Text="Upload document to server">
                                    </asp:CheckBox>
                                    <br />
                                    <asp:CheckBox ID="storeInDatabase" runat="server" CssClass="Normal" Text="Store in database (web farm support)">
                                    </asp:CheckBox>
                                    <br />
                                    <input id="FileUpload" style="width: 353px; font-family: verdana" type="file" runat="server"
                                        width="300" />
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
