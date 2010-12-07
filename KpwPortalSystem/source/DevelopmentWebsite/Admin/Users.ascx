<%@ Control Inherits="Admin.Web.Users" CodeBehind="Users.ascx.cs" Language="C#" AutoEventWireup="true" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="Title1" runat="server"></ASPNETPortal:title>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr valign="top">
            <td width="100">
                &nbsp; 
            </td>
            <td class="Normal">
                <asp:Literal id="Message" runat="server"></asp:Literal>
                <br />
                <br />
            </td>
        </tr>
        <tr valign="top">
            <td>
                &nbsp; 
            </td>
            <td class="Normal">
                已注册用户:&nbsp; 
                <asp:DropDownList id="allUsers" runat="server" DataValueField="UserID" DataTextField="Email"></asp:DropDownList>
                &nbsp; 
                <asp:ImageButton id="Imagebutton1" runat="server" AlternateText="编辑该用户" OnCommand="EditUser_Click" CommandName="edit" ImageUrl="~/images/edit.gif"></asp:ImageButton>
                <asp:ImageButton id="Imagebutton2" onclick="DeleteUser_Click" runat="server" AlternateText="删除该用户" ImageUrl="~/images/delete.gif"></asp:ImageButton>
                &nbsp; 
                <asp:LinkButton id="addNew" runat="server" OnCommand="EditUser_Click" CommandName="Add" Text="新增用户" cssclass="CommandButton"></asp:LinkButton>
            </td>
        </tr>
    </tbody>
</table>
