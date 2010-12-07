<%@ Control Inherits="Admin.Web.ModuleDefs" Language="C#" AutoEventWireup="true" CodeBehind="ModuleDefs.ascx.cs" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="Title1" runat="server"></ASPNETPortal:title>
<table cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr valign="top">
            <td>
                <asp:DataList id="defsList" runat="server" DataKeyField="ModuleDefID" OnItemCommand="DefsList_ItemCommand">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/images/edit.gif" AlternateText="�༭����" runat="server" />
                        &nbsp;&nbsp; 
                        <asp:Label Text='<%#DataBinder.Eval(Container.DataItem, "FriendlyName")%>' CssClass="Normal" runat="server" />
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton id="LinkButton1" onclick="AddDef_Click" runat="server" Text="����ģ������" cssclass="CommandButton"></asp:LinkButton>
            </td>
        </tr>
    </tbody>
</table>
