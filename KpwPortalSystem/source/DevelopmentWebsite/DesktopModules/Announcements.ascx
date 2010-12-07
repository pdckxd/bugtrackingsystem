<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.Announcements" CodeBehind="Announcements.ascx.cs" %>

<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="UserControl1" runat="server" EditUrl="~/DesktopModules/EditAnnouncements.aspx" EditText="Add New Announcement"></ASPNETPortal:title>

<asp:DataList id="myDataList" runat="server" EnableViewState="false" Width="98%" CellPadding="4">
    <ItemTemplate>
        <asp:HyperLink id="editLink" ImageUrl="~/images/edit.gif" NavigateUrl='<%#"~/DesktopModules/EditAnnouncements.aspx?ItemID=" +
                              DataBinder.Eval(Container.DataItem, "ItemID") + "&mid=" + ModuleId%>' Visible="<%#IsEditable%>" runat="server" />
        <span class="ItemTitle"> <%#DataBinder.Eval(Container.DataItem, "Title")%> </span> 
        <br />
        <span class="Normal"> <%#DataBinder.Eval(Container.DataItem, "Description")%> &nbsp; 
        <asp:HyperLink id="moreLink" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "MoreLink")%>' Visible='<%#DataBinder.Eval(Container.DataItem, "MoreLink") != String.Empty%>' runat="server">
                read more...</asp:HyperLink>
        </span> 
        <br />
    </ItemTemplate>
</asp:DataList>