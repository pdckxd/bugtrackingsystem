<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.Events" CodeBehind="Events.ascx.cs" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<ASPNETPortal:title id="UserControl1" runat="server" EditUrl="~/DesktopModules/EditEvents.aspx" EditText="新增事件"></ASPNETPortal:title>
<asp:DataList id="myDataList" runat="server" EnableViewState="false" Width="98%" CellPadding="4">
    <ItemTemplate>
        <span class="ItemTitle"> 
        <asp:HyperLink id="editLink" ImageUrl="~/images/edit.gif" NavigateUrl='<%#"~/DesktopModules/EditEvents.aspx?ItemID=" + DataBinder.Eval(Container.DataItem, "ItemID") +
                              "&mid=" + ModuleId%>' Visible="<%#IsEditable%>" runat="server" />
        <asp:Label Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>' runat="server" />
        </span> 
        <br />
        <span class="Normal"><i> <%#DataBinder.Eval(Container.DataItem, "WhereWhen")%> </i></span> 
        <br />
        <span class="Normal"> <%#DataBinder.Eval(Container.DataItem, "Description")%> </span> 
        <br />
    </ItemTemplate>
</asp:DataList>