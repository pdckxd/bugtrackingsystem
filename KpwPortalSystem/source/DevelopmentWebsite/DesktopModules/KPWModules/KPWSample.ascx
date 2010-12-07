<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KPWSample.ascx.cs" Inherits="DesktopModules.Web.KPWSample" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../../Shared/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="whPlayer" TagName= "vaPlayer" Src="VideoPlayer.ascx" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<table style="width:100%; height:100%">
<tr>
<td>
<div class="RoundedCorner" style="width:100%; height:100%">
<b class="rtop"><b class="r1"></b><b class="r2"></b>
<b class="r3"></b><b class="r4"></b></b>
<div>
    <div style="float:left; width:70%;">
        <div align="center">
        <whPlayer:vaPlayer ID="videoPlayer" autoStart="true" runat="server" ></whPlayer:vaPlayer>                       
        </div>
    </div>
    <div id="col">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
        <asp:Timer ID="timer1" runat="server" OnTick="Timer1_Tick" Interval="1000">
        </asp:Timer>
            <div style="margin:0 20px;">
            服务器时间：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtDatetime" runat="server" ReadOnly="true" />
            </div>
            <div style="margin:0 20px;">
            赤经位置：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtRa" runat="server" ReadOnly="true" />
            </div>
            <div style="margin:0 20px;">
            赤纬位置：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtDec" runat="server" ReadOnly="true" />
            </div>
             <div style="margin:0 20px;">
            赤经限位：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtRaOverflow" runat="server" ReadOnly="true" />
            </div>
             <div style="margin:0 20px;">
            赤纬限位：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtDecOverflow" runat="server" ReadOnly="true" />
            </div>
             <div style="margin:0 20px;">
            水平限位：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtLevelOverflow" runat="server" ReadOnly="true" />
            </div>
             <div style="margin:0 20px;">
            镜盖状态：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtMirrorStatus" runat="server" ReadOnly="true" />
            </div>
             <div style="margin:0 20px;">
            找星状态：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtSearchStar" runat="server" ReadOnly="true" />
            </div>
             <div style="margin:0 20px;">
            圆顶状态：
            </div>
            <div style="margin:0 20px;">
               <asp:TextBox ID="txtDome" runat="server" ReadOnly="true" />
            </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
 </div>
 <div id="ControlPanel">
            
            <table style="width:100%; height:100%; vertical-align:top">
                <tr>
                    <td width="25%">  
                        <fieldset id="Dec">
                        <legend>经度</legend>
                        <table style="width:100%;height:100%">
                            <tr>
                                <td >
                                   <button id="btnLPFast" type="reset" class="sexybutton" onmousedown="SendCommand('1')" onmouseup="SendCommand('9')"><span><span><span class="add">快动</span></span></span></button>
                                </td>
                                <td>
                                   <button id="Button11" type="reset" class="sexybutton" onmousedown="SendCommand('3')" onmouseup="SendCommand('9')"><span><span><span class="delete">快动</span></span></span></button>
                                
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <button id="btnLPSlow" type="reset" class="sexybutton" onmousedown="SendCommand('2')" onmouseup="SendCommand('9')"><span><span><span class="add">慢动</span></span></span></button>                                    
                                </td>
                                <td>
                                   <button id="Button1" type="reset" class="sexybutton" onmousedown="SendCommand('4')" onmouseup="SendCommand('9')"><span><span><span class="delete">慢动</span></span></span></button>                                    
                                </td>
                            </tr>
                        </table>
                        </fieldset>
                    </td>
                    <td width="25%">
                       <fieldset id="RA">
                       <legend>纬度</legend>
                        <table style="width:100%;height:100%">
                            <tr>
                                <td >
                                   <button id="Button2" type="reset" class="sexybutton" onmousedown="SendCommand('5')" onmouseup="SendCommand('9')"><span><span><span class="add">快动</span></span></span></button>
                                </td>
                                <td>
                                   <button id="Button3" type="reset" class="sexybutton" onmousedown="SendCommand('7')" onmouseup="SendCommand('9')"><span><span><span class="delete">快动</span></span></span></button>
                                
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <button id="Button4" type="reset" class="sexybutton" onmousedown="SendCommand('6')" onmouseup="SendCommand('9')"><span><span><span class="add">慢动</span></span></span></button>                                    
                                </td>
                                <td>
                                   <button id="Button5" type="reset" class="sexybutton" onmousedown="SendCommand('8')" onmouseup="SendCommand('9')"><span><span><span class="delete">慢动</span></span></span></button>                                    
                                </td>
                            </tr>
                        </table>
                        </fieldset>
                    </td>
                    <td width="50%">
                       <fieldset id="focus">
                       <legend>找星与跟踪</legend>
                       <table style="width:100%;height:100%">
                            <tr>
                            <td>
                            <button type="reset" class="sexybutton"  style=" width:100px;"><span><span><span class="search">自动找星</span></span></span></button>&nbsp;&nbsp;&nbsp;  
                            <button type="reset" class="sexybutton" style=" width:100px;"><span><span><span class="find">跟踪</span></span></span></button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                                   
                            </td>
                            </tr>
                            <tr>
                            <td>
                            <button type="reset" class="sexybutton" style=" width:100px;"><span><span><span class="play">开恒动</span></span></span></button>&nbsp;&nbsp;&nbsp; 
                            <button type="reset" class="sexybutton" style=" width:100px;"><span><span><span class="stop">关恒动</span></span></span></button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                            </td>
                            </tr>
                        </table>
                       </fieldset>
                    </td>
                </tr>
            </table>
            
</div>
<div id="result"></div>
<b class="rbottom"><b class="r4"></b><b class="r3"></b>
<b class="r2"></b><b class="r1"></b></b>
</div>
</td>
</tr>
</table>


