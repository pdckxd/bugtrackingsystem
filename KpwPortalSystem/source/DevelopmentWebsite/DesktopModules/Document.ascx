<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.Document" CodeBehind="Document.ascx.cs" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="UserControl1" EditText="ÐÂÔöÎÄµµ" EditUrl="~/DesktopModules/EditDocs.aspx" runat="server"></ASPNETPortal:title>
<asp:datagrid id="myDataGrid" runat="server" Border="0" width="100%" AutoGenerateColumns="false" EnableViewState="false">
    <Columns>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:HyperLink id="editLink" ImageUrl="~/images/edit.gif" NavigateUrl='<%#"~/DesktopModules/EditDocs.aspx?ItemID=" + DataBinder.Eval(Container.DataItem, "ItemID") +
                              "&mid=" + ModuleId%>' Visible="<%#IsEditable%>" runat="server" />
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Title" HeaderStyle-CssClass="NormalBold">
            <ItemTemplate>
                <asp:HyperLink id="docLink" Text='<%#DataBinder.Eval(Container.DataItem, "FileFriendlyName")%>' NavigateUrl='<%#
                GetBrowsePath(DataBinder.Eval(Container.DataItem, "FileNameUrl").ToString(),
                              DataBinder.Eval(Container.DataItem, "ContentSize"),
                              (int) DataBinder.Eval(Container.DataItem, "ItemId"))%>' CssClass="Normal" Target="_new" runat="server" />
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn HeaderText="Owner" DataField="CreatedByUser" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn HeaderText="Area" DataField="Category" ItemStyle-Wrap="false" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
        <asp:BoundColumn HeaderText="Last Updated" DataField="CreatedDate" DataFormatString="{0:d}" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
    </Columns>
</asp:datagrid>