<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.EditEvents" CodeBehind="EditEvents.aspx.cs"
    MasterPageFile="../Shared/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table cellspacing="0" cellpadding="4" width="98%" border="0">
        <tbody>
            <tr valign="top">
                <td width="100">
                    &nbsp;
                </td>
                <td width="*">
                    <table cellspacing="0" cellpadding="0" width="500">
                        <tbody>
                            <tr>
                                <td class="Head" align="left">
                                    事件详情
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
                                    标题:
                                </td>
                                <td rowspan="4">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="TitleField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="150"></asp:TextBox>
                                </td>
                                <td width="25" rowspan="4">
                                    &nbsp;
                                </td>
                                <td class="Normal" width="250">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Static"
                                        ErrorMessage="You Must Enter a Valid Title" ControlToValidate="TitleField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    描述:
                                </td>
                                <td>
                                    <asp:TextBox ID="DescriptionField" runat="server" Width="390" Columns="44" TextMode="Multiline"
                                        Rows="6"></asp:TextBox>
                                </td>
                                <td class="Normal">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Static"
                                        ErrorMessage="You Must Enter a Valid Description" ControlToValidate="DescriptionField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    地点/时间:
                                </td>
                                <td>
                                    <asp:TextBox ID="WhereWhenField" runat="server" CssClass="NormalTextBox" Width="390"
                                        Columns="30" MaxLength="150"></asp:TextBox>
                                </td>
                                <td class="Normal">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Static"
                                        ErrorMessage="You Must Enter a Valid Time/Location" ControlToValidate="WhereWhenField"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="SubHead">
                                    过期时间:
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
