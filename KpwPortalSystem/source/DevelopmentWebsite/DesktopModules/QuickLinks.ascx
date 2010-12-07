<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.QuickLinks" CodeBehind="QuickLinks.ascx.cs" %>

<hr width="98%" size="1" noshade="noshade" />
<span class="SubSubHead">¿ìËÙÁ´½Ó</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:hyperlink id="EditButton" runat="server" enableviewstate="false" cssclass="CommandButton"></asp:hyperlink>
<asp:datalist id="myDataList" runat="server" enableviewstate="false" width="100%" cellpadding="4">
	<itemtemplate>
		<span class="Normal">
			<asp:hyperlink id="editLink" imageurl="<%#linkImage%>" navigateurl='<%#
                ChooseURL(Convert.ToString(DataBinder.Eval(Container.DataItem, "ItemID")), ModuleId.ToString(),
                          (string) DataBinder.Eval(Container.DataItem, "Url"))%>' runat="server" />
			<a href='<%#DataBinder.Eval(Container.DataItem, "Url")%>'>
				<%#DataBinder.Eval(Container.DataItem, "Title")%>
			</a>
		</span>
		<br />
	</itemtemplate>
</asp:datalist>
<br />
