<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Kpw60Camera.ascx.cs" Inherits="DesktopModules.Web.Kpw60Camera" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../../Shared/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="whPlayer" TagName= "vaPlayer" Src="VideoPlayer.ascx" %>

 <table style="width: 100%; height: 100%">
            <tr>
                <td>
                    <div class="RoundedCorner" style="width: 100%; height: 100%">
                        <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4">
                        </b></b>
                        <div>
                            <div style="float: left; width: 70%;">
                                <div align="center">
                                    <whPlayer:vaPlayer ID="videoPlayer" autoStart="true" runat="server">
                                    </whPlayer:vaPlayer>
                                </div>
                            </div>
                            <div id="col">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="timer2" runat="server" OnTick="Timer2_Tick" Interval="1000">
                                        </asp:Timer>
                                        <div class="ContentBox">
                                            <div style="float:left;">连接状态：</div>
                                            <asp:Label ID="txtConnect" CssClass="ContentInformation"  runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="ContentBox">
                                            <div style="float:left;">赤经位置：</div>
                                            <asp:Label ID="txtRa" CssClass="ContentInformation"  runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="ContentBox">
                                            <div style="float:left;">赤纬位置：</div>
                                        
                                            <asp:Label ID="txtDec" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="ContentBox">
                                            <div style="float:left;">赤经限位：</div>
                                        
                                            <asp:Label ID="txtRaOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="ContentBox">
                                            <div style="float:left;">赤纬限位：</div>
                                       
                                            <asp:Label ID="txtDecOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="ContentBox">
                                            <div style="float:left;">水平限位：</div>
                                       
                                            <asp:Label ID="txtLevelOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="ContentBox">
                                            <div style="float:left;">镜盖状态：</div>
                                        
                                            <asp:Label ID="txtMirrorStatus" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="ContentBox">
                                            <div style="float:left;">找星状态：</div>
                                      
                                            <asp:Label ID="txtSearchStar" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="ContentBox">
                                            <div style="float:left;">圆顶状态：</div>
                                       
                                            <asp:Label ID="txtDome" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div id="ControlPanel">
                            拍照控制按钮
                        </div>
                        <div id="result">
                        </div>
                        <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1">
                        </b></b>
                    </div>
                </td>
            </tr>
        </table>
