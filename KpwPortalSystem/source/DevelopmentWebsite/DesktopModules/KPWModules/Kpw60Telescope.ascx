<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Kpw60Telescope.ascx.cs"
    Inherits="DesktopModules.Web.Kpw60Telescope" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../../Shared/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="whPlayer" TagName="vaPlayer" Src="VideoPlayer.ascx" %>
<%@ Register TagPrefix="kpw60Console" TagName="kpw60" Src="Kpw60Console.ascx" %>
<%@ Register TagPrefix="kpw60Camera" TagName="kpw60camera" Src="Kpw60Camera.ascx" %>
<%@ Register TagPrefix="kpw60Pictures" TagName="kpw60picture" Src="Kpw60Pictures.ascx" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"  >
</asp:ScriptManagerProxy>
<div id="tabs">
    <ul>
        <li><a href="#tabs-1">主控制台</a></li>
        <li><a href="#tabs-2" >图片管理</a></li>
        <li><a href="#tabs-4">关于</a></li>
    </ul>
   
    <div id="tabs-1">
       <kpw60Console:kpw60 ID="kpw60Console" runat="server" ></kpw60Console:kpw60>
    </div>
    <div id="tabs-2">
       <iframe src="DesktopModules/KPWModules/ImageGalleryEx.aspx" height="650px" width="100%" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes"></iframe> 
    </div>
     <div id="tabs-4">
        <h2>望远镜简介</h2>
        <p>60厘米望远镜介绍内容</p>
        <h2>望远镜参数</h2>
        <p>60厘米望远镜参数介绍</p>
        <h2>望远镜功能</h2>
        <p>60厘米望远镜功能介绍既操作方法</p>
    </div>
</div>
