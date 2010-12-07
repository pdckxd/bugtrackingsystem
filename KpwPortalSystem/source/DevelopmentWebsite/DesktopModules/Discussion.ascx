<%@ Import namespace="Nairc.KPWPortal"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Discussion.ascx.cs" Inherits="DesktopModules.Web.Discussion" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>


<ASPNETPortal:title id="UserControl1" EditText="新增话题" EditUrl="~/DesktopModules/DiscussDetails.aspx" EditTarget="_new" runat="server"></ASPNETPortal:title>
<asp:DataList id="TopLevelList" runat="server" width="98%" ItemStyle-Cssclass="Normal" DataKeyField="Parent" OnItemCommand="TopLevelList_Select">
    <ItemTemplate>
        <asp:ImageButton id="btnSelect" ImageUrl='<%#NodeImage((int) DataBinder.Eval(Container.DataItem, "ChildCount"))%>' CommandName="select" runat="server" />
        <asp:hyperlink Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>' NavigateUrl='<%#FormatUrl((int) DataBinder.Eval(Container.DataItem, "ItemID"))%>' Target="_new" runat="server" />
        , from <%#DataBinder.Eval(Container.DataItem, "CreatedByUser")%>, posted <%#DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:g}")%> 
    </ItemTemplate>
    <SelectedItemTemplate>
        <asp:ImageButton id="btnCollapse" ImageUrl="~/images/minus.gif" runat="server" CommandName="collapse" />
        <asp:hyperlink Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>' NavigateUrl='<%#FormatUrl((int) DataBinder.Eval(Container.DataItem, "ItemID"))%>' Target="_new" runat="server" />
        , from <%#DataBinder.Eval(Container.DataItem, "CreatedByUser")%>, posted <%#DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:g}")%> 
        <asp:DataList id="DetailList" ItemStyle-Cssclass="Normal" datasource="<%#GetThreadMessages()%>" runat="server">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Indent")%> <img src="<%=Global.GetApplicationPath(Request)%>/images/1x1.gif" height="15" /> 
                <asp:hyperlink Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>' NavigateUrl='<%#FormatUrl((int) DataBinder.Eval(Container.DataItem, "ItemID"))%>' Target="_new" runat="server" />
                , from <%#DataBinder.Eval(Container.DataItem, "CreatedByUser")%>, posted <%#DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:g}")%> 
            </ItemTemplate>
        </asp:DataList>
    </SelectedItemTemplate>
</asp:DataList>