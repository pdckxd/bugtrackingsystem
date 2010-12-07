<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Kpw60Console.ascx.cs"
    Inherits="YWXKPortal.Web.Kpw60Console" %>

<script type="text/javascript">

    $(function() {

        $("#dialogPic").dialog({ autoOpen: false, width: 700, height: 570, modal: true, close: function(event, ui) { $("#vPlayer").show(); } });

        $("#btnFindStarByName").click(function() {
            var starname = $("#selectStarName").val();
            FindStarByName(starname);
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

        $("#btnCatchImage").click(function() {
            CatchImage();
        });

        $("#progressbar").progressbar();

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
                    <div style="float: left; width: 60%;">
                        <div id="vPlayer" align="center">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <object classid="clsid:CAFCF48D-8E34-4490-8154-026191D73924" codebase="../codebase/NetVideoActiveX23.cab#version=2,3,9,1"
                                            standby="Waiting..." id="NetOCX1" name="ocx1" width="450" height="390" align="center">
                                        </object>
                                        <input type="button" id="btnCatchImage" value="抓图" />
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="left" style="display:none;">
                                    <button id="Button6" type="reset" class="sexybutton" onclick="Play1()">
                                                            <span><span><span class="play">预览</span></span></span></button>
                                    <button id="Button7" type="reset" class="sexybutton" onclick="Stop1()">
                                                            <span><span><span class="stop">停止</span></span></span></button>
                                    </td>
                                    </tr>--%>
                            </table>
                            <%--<whPlayer:vaPlayer ID="videoPlayer" autoStart="true" runat="server">
                                    </whPlayer:vaPlayer>--%>
                        </div>
                    </div>
                    <div align="center">
                        <object classid="clsid:CAFCF48D-8E34-4490-8154-026191D73924" codebase="../codebase/NetVideoActiveX23.cab#version=2,3,9,1"
                            standby="Waiting..." id="Object1" name="ocx1" width="250" height="250" align="center">
                        </object>
                    </div>
                </div>
                <div style="height: 10px">
                </div>
                <div id="ControlPanel">
                    <table>
                        <tr>
                            <td width="60">
                                <div id="controlDiv" runat="server">
                                    <table style="width: 100%; height: 100%; vertical-align: top">
                                        <tr>
                                            <td width="50%">
                                                <fieldset id="Dec" style="vertical-align: top; margin: 0">
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
                                            <td width="50%">
                                                <fieldset id="RA" style="vertical-align: top; margin: 0">
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
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table style="border: solid 1px #9AB2F5; width: 100%; height: 100%;">
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
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
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
                                </div>
                            </td>
                            <td width="40%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Timer ID="timer1" runat="server" OnTick="Timer1_Tick" Interval="1000">
                                        </asp:Timer>
                                        <table style="margin-top: 30px; margin-left: 10px;">
                                            <tr class="ContentBox">
                                                <td>
                                                    连接状态：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtConnect" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr class="ContentBox">
                                                <td>
                                                    赤经位置：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtRa" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr class="ContentBox">
                                                <td>
                                                    赤纬位置：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtDec" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr class="ContentBox">
                                                <td>
                                                    赤经限位：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtRaOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr class="ContentBox">
                                                <td>
                                                    赤纬限位：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtDecOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr class="ContentBox">
                                                <td>
                                                    水平限位：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtLevelOverflow" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr class="ContentBox">
                                                <td>
                                                    镜盖状态：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtMirrorStatus" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr class="ContentBox">
                                                <td>
                                                    找星状态：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtSearchStar" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr class="ContentBox">
                                                <td>
                                                    圆顶状态：
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtDome" CssClass="ContentInformation" runat="server" ReadOnly="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="result">
                </div>
                <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1">
                </b></b>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">

      
</script>

