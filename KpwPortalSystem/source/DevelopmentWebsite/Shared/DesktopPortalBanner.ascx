<%@ Import namespace="Nairc.KPWPortal"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesktopPortalBanner.ascx.cs" Inherits="WebApplication.DesktopPortalBanner" %>

<div>
		
    <div id="header"> 
    
       <div style="position:relative; width:1000px; margin:10px 0 0 8px; float:right;">
				    <table width="100%" border="0">
				        <tr height="100%">
				           <td class="TopSiteLink" align="right"> <!--background="<%= Global.GetApplicationPath(Request) %>/images/bars.gif"> -->
				           <asp:label id="WelcomeMessage" forecolor="#eeeeee" runat="server"></asp:label>
				            <a class="SiteLink" href="<%= Global.GetApplicationPath(Request) %>/DesktopDefault.aspx">Õ¾µãÊ×Ò³</a>
				            <%--<span class="Accent">|</span>--%>
				            <%--<a class="SiteLink" href="<%= Global.GetApplicationPath(Request) %>/Docs/Docs.htm" target="_blank">
					            Portal Documentation</a>--%>
				            <%= LogoffLink %>
				            &nbsp;&nbsp;
				           </td>
				        </tr>
				    </table>
		</div>    
	    <h1><asp:label id="siteName" CssClass="SiteTitle" runat="server" EnableViewState="false" ></asp:label></h1>
	    <div style="float:right; margin-right:10px; margin-bottom:5px; margin-top:40px;"> 
          <asp:UpdatePanel ID="datetimepanel" runat="server">
              <ContentTemplate>
              <asp:Timer ID="timer1" runat="server" OnTick="Timer1_Tick" Interval="1000">
              </asp:Timer>
              <asp:Label id="lblDatatime" forecolor="#eeeeee" runat="server"></asp:Label>
              </ContentTemplate>
          </asp:UpdatePanel> 
        </div>     
    </div>
     <!-- Main menu (tabs) -->
     <div id="maintabs" class="noprint">
				<asp:Repeater ID="tabsRepeater" runat="server" >
				<HeaderTemplate><ul class="box" style="list-style:none;"></HeaderTemplate>
				<ItemTemplate><li id="<%#(string)hashTable[Container.ItemIndex] %>"><a href='<%= Global.GetApplicationPath(Request) %>/DesktopDefault.aspx?tabindex=<%# Container.ItemIndex %>&tabid=<%# ((TabStripDetails) Container.DataItem).TabId %>'><%# ((TabStripDetails) Container.DataItem).TabName %><span class="tab-l"></span><span class="tab-r"></span></a></li></ItemTemplate>
				<FooterTemplate></ul></FooterTemplate>
				</asp:Repeater>
        
     </div> <!-- /tabs -->
</div>
