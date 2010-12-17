<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Kpw60Console.ascx.cs" Inherits="DesktopModules.Web.Kpw60Console" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../../Shared/DesktopModuleTitle.ascx" %>
<%@ Register TagPrefix="whPlayer" TagName= "vaPlayer" Src="VideoPlayer.ascx" %>


 <script type="text/javascript">

     $(function() {

         $("#dialogPic").dialog({ autoOpen: false, width: 700, height: 570, modal: true, close: function(event, ui) { $("#vPlayer").show(); } });

         $("#btnFindStarByName").click(function() {
             var starname = $("#selectStarName").val();
             FindStarByName(starname);
             $("#selectStarName").get(0).selectedIndex = starname;
         });


         $("#btnFindStarByPosition").click(function() {

             var raH = $("#txtRaH").val();
             var raM = $("#txtRaM").val();
             var raS = $("#txtRaS").val();
             var decD = $("#txtDecD").val();
             var decM = $("#txtDecM").val();
             var decS = $("#txtDecS").val();
             var decFlag = $("#selectDecFlag").val();

             var isChecked = $("#checkboxPastPosition").attr("checked");
             var position = $("#txtPosotion").val();

             if (isChecked == true) {
                 FindStarByPosition(position);
             }
             else {
                 FindStarByPosition(raH, raM, raS, decFlag, decD, decM, decS);
             }

         });

         $("#checkboxPastPosition").click(function() {

             var isChecked = $("#checkboxPastPosition").attr("checked");

             if (isChecked == true) {
                 $("#txtPosotion").removeAttr("disabled");
             }
             else {
                 $("#txtPosotion").attr("disabled", "disabled");
             }
         });
         
         $("#progressbar").progressbar();

         $("#btnPhoto").click(function() {
             //$("#divWait").attr("style", "width:120px; height:80px; float:left;");
             var grabtime = parseInt($("#txtGrabTime").val());

             var divprogressbar = $("#divprogressbar");
             $("#progressbar", divprogressbar).everyTime(grabtime * 10, function(i) {
                 var i = $("#progressbar").progressbar("value");

                 if (i < 98)
                     var i = $("#progressbar").progressbar("value", i + 1);
             });
             TakePicture(grabtime);
             //setTimeout('$("#dialogPic").dialog("open");$("#vPlayer").hide(); $("#divWait").attr("style", "display:none");', 5000);
         });

         //网页加载即显示视频
         setTimeout('Play1()', 2000);
     });

    

  
</script>
 
     
 <table style="width: 100%; height: 100%">
            <tr>
                <td>
                    <div class="RoundedCorner" style="width: 100%; height: 100%">
                        <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4">
                        </b></b>
                        <div>
                            <div style="float: left; width: 70%;">
                                <div id="vPlayer" align="center">
                                    <table width="100%">
                                    <tr>
                                    <td>
                                     <object classid="clsid:CAFCF48D-8E34-4490-8154-026191D73924" codebase="codebase/NetVideoActiveX23.cab#version=2,3,9,1"
		  standby="Waiting..." id="NetOCX1"  name="ocx1" width="530" height="360" align="center" ></object>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                    <button id="btnCatchImage" type="reset" class="sexybutton" onclick="CatchImage()">
                                                            <span><span><span class="camerapicture">抓图</span></span></span></button>&nbsp;&nbsp;
                                    <button id="btnStartVideo" type="reset" class="sexybutton" onclick="StartVideo()">
                                                            <span><span><span class="camerastart" >开始录像</span></span></span></button>
                                    <button id="btnStopVideo" type="reset" class="sexybutton" onclick="StopVideo()" style=" display:none">
                                                            <span><span><span class="camerastop">停止录像</span></span></span></button>
                                    <div id="txtFolderMessge" style="color:Red"></div>
                                    </td>
                                    </tr>
                                    </table>
                                    <%--<whPlayer:vaPlayer ID="videoPlayer" autoStart="true" runat="server">
                                    </whPlayer:vaPlayer>--%>                                   
                                </div>
                            </div>
                            <div id="col">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="timer1" runat="server" OnTick="Timer1_Tick" Interval="1000">
                                        </asp:Timer>
                                        <table style=" margin-top:30px; margin-left:10px;">
                                        <tr class="ContentBox">
                                        <td>连接状态：</td>
                                        <td><asp:Label ID="txtConnect" CssClass="ContentInformation"  runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        <tr class="ContentBox">
                                        <td>赤经位置：</td>
                                        <td><asp:Label ID="txtRa" CssClass="ContentInformation"  runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        <tr class="ContentBox">
                                        <td>赤纬位置：</td>
                                        <td><asp:Label ID="txtDec" CssClass="ContentInformation" runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        <tr class="ContentBox">
                                        <td>赤经限位：</td>
                                        <td><asp:Label ID="txtRaOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        <tr class="ContentBox">
                                        <td>赤纬限位：</td>
                                        <td><asp:Label ID="txtDecOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        <tr class="ContentBox">
                                        <td>水平限位：</td>
                                        <td><asp:Label ID="txtLevelOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        <tr class="ContentBox">
                                        <td>镜盖状态：</td>
                                        <td><asp:Label ID="txtMirrorStatus" CssClass="ContentInformation" runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        <tr class="ContentBox">
                                        <td>找星状态：</td>
                                        <td><asp:Label ID="txtSearchStar" CssClass="ContentInformation" runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        <tr class="ContentBox">
                                        <td>圆顶状态：</td>
                                        <td><asp:Label ID="txtDome" CssClass="ContentInformation" runat="server" ReadOnly="true" /></td>
                                        </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div id="ControlPanel">
                        <div id="noRights" visible="false" runat="server" align="center" style="height:230px; vertical-align:middle; color:Red; font-size:15px;">您未授权观测，不能操作望远镜!</div>
                        <div id="hasRights" runat="server" style="height:300px;">
                            <table style="width: 100%; height: 100%; vertical-align: top">
                                <tr>
                                    <td width="30%">
                                        <fieldset id="Dec">
                                            <legend>经度</legend>
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td>
                                                        <button id="btnLPFast" type="button" class="sexybutton" onmousedown="SendCommand('1')"
                                                            onmouseup="SendCommand('9')">
                                                            <span><span><span class="add">快动</span></span></span></button>
                                                    </td>
                                                    <td>
                                                        <button id="Button11" type="button" class="sexybutton" onmousedown="SendCommand('3')"
                                                            onmouseup="SendCommand('9')">
                                                            <span><span><span class="delete">快动</span></span></span></button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <button id="btnLPSlow" type="button" class="sexybutton" onmousedown="SendCommand('2')"
                                                            onmouseup="SendCommand('9')">
                                                            <span><span><span class="add">慢动</span></span></span></button>
                                                    </td>
                                                    <td>
                                                        <button id="Button1" type="button" class="sexybutton" onmousedown="SendCommand('4')"
                                                            onmouseup="SendCommand('9')">
                                                            <span><span><span class="delete">慢动</span></span></span></button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td width="30%">
                                        <fieldset id="RA">
                                            <legend>纬度</legend>
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td>
                                                        <button id="Button2" type="button" class="sexybutton" onmousedown="SendCommand('5')"
                                                            onmouseup="SendCommand('9')">
                                                            <span><span><span class="add">快动</span></span></span></button>
                                                    </td>
                                                    <td>
                                                        <button id="Button3" type="button" class="sexybutton" onmousedown="SendCommand('7')"
                                                            onmouseup="SendCommand('9')">
                                                            <span><span><span class="delete">快动</span></span></span></button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <button id="Button4" type="button" class="sexybutton" onmousedown="SendCommand('6')"
                                                            onmouseup="SendCommand('9')">
                                                            <span><span><span class="add">慢动</span></span></span></button>
                                                    </td>
                                                    <td>
                                                        <button id="Button5" type="button" class="sexybutton" onmousedown="SendCommand('8')"
                                                            onmouseup="SendCommand('9')">
                                                            <span><span><span class="delete">慢动</span></span></span></button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td width="40%">
                                        <fieldset id="focus">
                                            <legend>找星与跟踪</legend>
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td>
                                                        <button id="btnFindStarByPosition1" type="button" class="sexybutton" style="width: 100px;">
                                                            <span><span><span class="search">开镜盖</span></span></span></button>&nbsp;&nbsp;&nbsp;
                                                        <button id="btnFindStarByName1" type="button" class="sexybutton" style="width: 100px;">
                                                            <span><span><span class="search">关镜盖</span></span></span></button>&nbsp;&nbsp;&nbsp;
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        
                                                        <button type="button" class="sexybutton" style="width: 100px;">
                                                            <span><span><span class="stop">跟踪</span></span></span></button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="2" style="border: solid 1px #9AB2F5;">
                                <table style="width: 100%; height: 100%;">
                                                    <tr>
                                                        <td>
                                                            赤经：
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtRaH" style="width: 100px; text-align: right" value="0" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            h
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtRaM" style="width: 100px; text-align: right" value="0" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            m
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtRaS" style="width: 100px; text-align: right" value="0" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            s
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            赤纬：
                                                        </td>
                                                        <td>
                                                            <select id="selectDecFlag">
                                                                <option label="+" value="+" selected="selected"></option>
                                                                <option label="-" value="-"></option>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtDecD" style="width: 100px; text-align: right" value="0" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            °
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtDecM" style="width: 100px; text-align: right" value="0" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            ′
                                                        </td>
                                                        <td>
                                                            <input type="text" id="txtDecS" style="width: 100px; text-align: right" value="0" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            ″
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        <input id="checkboxPastPosition" type="checkbox" value="使用粘贴坐标" name="粘贴坐标" title="使用粘贴坐标" />粘贴坐标&nbsp;&nbsp;&nbsp;
                                                                        <input type="text" id="txtPosotion" style="width: 200px; text-align: right" value=""
                                                                            disabled="disabled" />
                                                                    </td>
                                                                    <td align="right">
                                                                        <button id="btnFindStarByPosition" type="button" class="sexybutton" style="width: 120px;">
                                                                            <span><span><span class="search">按坐标找星</span></span></span></button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                
                                                    <tr>
                                                   <td colspan="8">
                                                <table style="border: solid 1px #9AB2F5; width: 100%; height: 100%;">
                                                    <tr>
                                                        <td align="left">
                                                            <select id="selectStarName" style="width: 140px;">
                                                                <%--<option label="太阳" value="1"></option>
                                                <option label="月亮" value="2"></option>--%>
                                                                <optgroup label="行星">
                                                                </optgroup>
                                                                <option label="水星" value="3"></option>
                                                                <option label="金星" value="4"></option>
                                                                <option label="火星" value="5"></option>
                                                                <option label="木星" value="6"></option>
                                                                <option label="土星" value="7"></option>
                                                                <option label="天王星" value="8"></option>
                                                                <optgroup label="恒星">
                                                                </optgroup>
                                                                <option label="天狼星(大犬座α)" value="11"></option>
                                                                <option label="大角(御夫座α)" value="12"></option>
                                                                <option label="织女星(天琴座α)" value="13"></option>
                                                                <option label="牛郎星(天鹰座α)" value="14"></option>
                                                                <option label="軒轅十四(狮子座α)" value="15"></option>
                                                                <option label="參 宿 四(猎户座α)" value="16"></option>
                                                                <option label="天津四(天鹅座α)" value="17"></option>
                                                            </select>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <button id="btnFindStarByName" type="button" class="sexybutton" style="width: 120px;">
                                                                <span><span><span class="search">按星名找星</span></span></span></button>
                                                        </td>
                                                        <td align="right">
                                                            <button id="btnStopFindStar" type="button" class="sexybutton" style="width: 120px;"
                                                                onmouseup="StopFindStar()">
                                                                <span><span><span class="stop">停止找星</span></span></span></button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                                </table>
                                </td>
                                <td align="center" style="border:solid 1px #9AB2F5;">
                                <table width="100%">
                                <tr height="50%">
                                <%--<td width="50%"> 
                                <div id="divWait" style="width:120px; height:80px; float:left; display:none">
                                 <img src="<%=WebSiteUrl %>/images/loader.gif" alt="" />
                                </div>  
                                </td>--%>
                                <td > 
                                
                                曝光时间：<input type="text" id="txtGrabTime" style="width:50px" value="5"/>&nbsp;ms
                                <button id="btnPhoto" type="button" class="sexybutton" style="width: 120px;">
                                <span><span><span class="find">拍照</span></span></span></button>
                               
                                </td>
                                </tr>
                                <tr height="50%">
                                <td>
                                <div id="divprogressbar" style="height:17px;">
                                <div id="progressbar" style="height:15px;"></div>
                                </div> 
                                </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                            </table>
                        </div>
                        </div>
                        <div id="result">
                        </div>
                        <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1">
                        </b></b>
                    </div>
                </td>
            </tr>
 </table>
<div id="dialogPic" title="拍照完成">
 <table width="100%">
 <tr>
 <td style="width:520px;">
 <img src="<%=Url %>N001.jpg" alt="" width="500px" height="500px" />
 </td>
 <td>
 <%--<a id="aImage1" class="aImage" href="#" data="<%=Url %>N001_big.jpg" title="下载JPG格式照片" >下载JPG格式照片</a>--%>
 <asp:LinkButton ID="lnkImageJPG" runat="server" CommandArgument="N001_big.jpg" CommandName="JPG" Text="下载JPG格式照片" OnClick="lnkImage_Click"></asp:LinkButton> 
 <br />
 <br />
 <asp:LinkButton ID="lnkImageFIT" runat="server" CommandArgument="N001_big.fit" CommandName="FIT" Text="下载FIT格式照片" OnClick="lnkImage_Click"></asp:LinkButton>
 <%--<a id="aImage2" class="aImage" href="<%=Url %>astronomy/N001_big.tif" rel="nofollow" title="下载FIT格式照片" >下载FIT格式照片</a>--%>
 </td>
 </tr>
 </table>
 </div>
 
 <script type="text/javascript">

      
</script>