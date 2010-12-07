<%@ Control Inherits="Admin.Web.Roles" CodeBehind="Roles.ascx.cs" Language="C#" AutoEventWireup="true" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="Title1" runat="server"></ASPNETPortal:title>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr valign="top">
            <td class="Normal" width="100">
                &nbsp; 
            </td>
            <td>
                <asp:DataList id="rolesList" runat="server" DataKeyField="RoleID" OnItemCommand="RolesList_ItemCommand">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/images/edit.gif" CommandName="edit" AlternateText="编辑该项" runat="server" />
                        <asp:ImageButton ImageUrl="~/images/delete.gif" CommandName="delete" AlternateText="删除该项" runat="server" />
                        &nbsp;&nbsp; 
                        <asp:Label Text='<%#DataBinder.Eval(Container.DataItem, "RoleName")%>' cssclass="Normal" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Textbox id="roleName" width="200" cssclass="NormalTextBox" Text='<%#DataBinder.Eval(Container.DataItem, "RoleName")%>' runat="server" />
                        &nbsp; 
                        <asp:LinkButton Text="应用" CommandName="apply" cssclass="CommandButton" runat="server" />
                        &nbsp; 
                        <asp:LinkButton Text="修改角色成员" CommandName="members" cssclass="CommandButton" runat="server" />
                    </EditItemTemplate>
                </asp:DataList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; 
            </td>
            <td>
                <asp:LinkButton id="LinkButton1" onclick="AddRole_Click" runat="server" Text="新增角色" cssclass="CommandButton"></asp:LinkButton>
            </td>
        </tr>
    </tbody>
</table>