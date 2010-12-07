<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.Links" CodeBehind="Links.ascx.cs" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>
<aspnetportal:title id="UserControl1" runat="server" edittext="Add Link" editurl="~/DesktopModules/EditLinks.aspx"></aspnetportal:title>
<asp:datalist id="myDataList" runat="server" width="100%" cellpadding="4">
	<itemtemplate>
		<span class="Normal">
			<asp:hyperlink id="editLink" imageurl="<%#linkImage%>" navigateurl='<%#
                ChooseURL(Convert.ToString(DataBinder.Eval(Container.DataItem, "ItemID")), ModuleId.ToString(),
                          (string) DataBinder.Eval(Container.DataItem, "Url"))%>' target='<%#ChooseTarget()%>' tooltip='<%#ChooseTip((string) DataBinder.Eval(Container.DataItem, "Description"))%>' runat="server" />
			<asp:hyperlink text='<%#DataBinder.Eval(Container.DataItem, "Title")%>' navigateurl='<%#DataBinder.Eval(Container.DataItem, "Url")%>' tooltip='<%#DataBinder.Eval(Container.DataItem, "Description")%>' target="_new" runat="server"/>
		</span>
		<br />
	</itemtemplate>
</asp:datalist>