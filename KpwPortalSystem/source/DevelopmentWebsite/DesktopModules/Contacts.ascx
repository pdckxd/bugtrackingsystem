<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.Contacts" CodeBehind="Contacts.ascx.cs" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="UserControl1" EditText="添加新联系人" EditUrl="../DesktopModules/EditContacts.aspx" runat="server"></ASPNETPortal:title>

<asp:datagrid id="myDataGrid" runat="server" Border="0" width="100%" AutoGenerateColumns="false" EnableViewState="false">
    <Columns>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:HyperLink ImageUrl="~/images/edit.gif" NavigateUrl='<%#"~/DesktopModules/EditContacts.aspx?ItemID=" +
                              DataBinder.Eval(Container.DataItem, "ItemID") + "&mid=" + ModuleId%>' Visible="<%#IsEditable%>" runat="server" />
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn HeaderText="Name" DataField="Name" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn HeaderText="Role" DataField="Role" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:HyperLinkColumn HeaderText="Email" DataTextField="Email" DataNavigateUrlField="Email" DataNavigateUrlFormatString="mailto:{0}" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn HeaderText="Contact 1" DataField="Contact1" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn HeaderText="Contact 2" DataField="Contact2" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
    </Columns>
</asp:datagrid>