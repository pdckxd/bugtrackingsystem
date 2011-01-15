	
/*Jquery Vertival tabs--------*/
$(function() {
    $("#tabs").tabs().addClass('ui-tabs-vertical ui-helper-clearfix');
    $("#tabs li").removeClass('ui-corner-top').addClass('ui-corner-left');
});



function SendCommand(command) {
    WebApplication.KPWPortalWebService.SendCommand(command, onSendCommandSucceeded);
    $get("result").innerHTML = command;
}

function FindStarByPosition(RaH,RaM,RaS,DecFlag,DecD,DecM,DecS) {
    WebApplication.KPWPortalWebService.FindStarByPosition(RaH,RaM,RaS,DecFlag,DecD,DecM,DecS, onSendCommandSucceeded);
    $get("result").innerHTML = "坐标找星...";
}

function FindStarByPastPosition(position) {
    WebApplication.KPWPortalWebService.FindStarByPosition(position, onSendCommandSucceeded);
    $get("result").innerHTML = "粘贴坐标找星...";
}

function FindStarByName(name) {
    WebApplication.KPWPortalWebService.FindStarByName(name, onSendCommandSucceeded);
    $get("result").innerHTML = "星名找星...";
}

function StopFindStar() {
    WebApplication.KPWPortalWebService.StopFindStar(onSendCommandSucceeded);
    $get("result").innerHTML = "停止找星...";
}

function StartTrackStar() {
    WebApplication.KPWPortalWebService.StartTrackStar(onSendCommandSucceeded);
    $get("result").innerHTML = "正在跟踪...";
}

function StopTrackStar() {
    WebApplication.KPWPortalWebService.StopTrackStar(onSendCommandSucceeded);
    $get("result").innerHTML = "跟踪停止！";
}

function onSendCommandSucceeded(result) {
    if (result == true) {
        $get("result").innerHTML = "命令发送成功！";
    }
    else {
        $get("result").innerHTML = "命令发送失败！";
    }
}

function TakePicture(grabTime) {
    WebApplication.KPWPortalWebService.TakePicture(grabTime, onTakePictureSucceeded);
    $get("result").innerHTML = "正在拍照...";
}

function onTakePictureSucceeded(result) {

    var divprogressbar = $("#divprogressbar");
    $("#progressbar", divprogressbar).stopTime();
    $("#progressbar").progressbar("value", 0);
    $("#dialogPic").dialog("open");
    $("#vPlayer").hide();
    $get("result").innerHTML = "";
}

function Play1() {
    var Netocx1 = document.getElementById("NetOCX1");
    var UserID = Netocx1.Login("159.226.75.29", 8000, "admin", "12345");
    Netocx1.StartRealPlay(0, 0, 0);

    var Object1 = document.getElementById("Object1");
    var UserID = Object1.Login("159.226.75.29", 8000, "admin", "12345");
    Object1.StartRealPlay(1, 0, 0);
}

function Stop1() {
    var Netocx1 = document.getElementById("NetOCX1");
    Netocx1.StopRealPlay();
    Netocx1.Logout();
}

function CatchImage() {
    
    var Netocx1 = document.getElementById("NetOCX1");
    var succeed = Netocx1.BMPCapturePicture("C:\\kpw\\OCXBMPCaptureFiles\\主镜", false);
    if(succeed == true)
        $get("txtFolderMessge").innerHTML = "图片保存在文件夹 C:\\kpw\\OCXBMPCaptureFiles\\主镜";
     else
         $get("txtFolderMessge").innerHTML = "图片抓取失败，请重试!";
}

function StartVideo() {

    $get("txtFolderMessge").innerHTML = "开始录像....";
    var Netocx1 = document.getElementById("NetOCX1");
    var succeed = Netocx1.StartRecord("C:\\kpw\\OCXVideoCaptureFiles\\主镜");

    if (succeed == true) {
        $get("txtFolderMessge").innerHTML = "正在录像....";
        $("#btnStartVideo").hide();
        $("#btnStopVideo").show();
    }
    else {
        $get("txtFolderMessge").innerHTML = "开始录像失败，请重试!";
    }
}

function StopVideo() {

    var Netocx1 = document.getElementById("NetOCX1");
    Netocx1.StopRecord(false);
    $("#btnStartVideo").show();
    $("#btnStopVideo").hide();
    $get("txtFolderMessge").innerHTML = "视频保存在文件夹 C:\\kpw\\OCXVideoCaptureFiles\\主镜";
    
}

function CatchImageX() {

    var Netocx2 = document.getElementById("Object1");
    var succeed = Netocx2.BMPCapturePicture("C:\\kpw\\OCXBMPCaptureFiles\\姿态", false);
    if (succeed == true)
        $get("txtFolderMessgeX").innerHTML = "图片保存在文件夹 C:\\kpw\\OCXBMPCaptureFiles\\姿态";
    else
        $get("txtFolderMessgeX").innerHTML = "图片抓取失败，请重试!";
}

function StartVideoX() {
    $get("txtFolderMessgeX").innerHTML = "开始录像....";
    var Netocx2 = document.getElementById("Object1");
    var succeed = Netocx2.StartRecord("C:\\kpw\\OCXVideoCaptureFiles\\姿态");

    if (succeed == true) {
        $get("txtFolderMessgeX").innerHTML = "正在录像....";
        $("#btnStartVideoX").hide();
        $("#btnStopVideoX").show();
    }
    else {
        $get("txtFolderMessgeX").innerHTML = "开始录像失败，请重试!";
    }
}

function StopVideoX() {

    var Netocx2 = document.getElementById("Object1");
    Netocx2.StopRecord(false);
    $("#btnStartVideoX").show();
    $("#btnStopVideoX").hide();
    $get("txtFolderMessgeX").innerHTML = "视频保存在文件夹 C:\\kpw\\OCXVideoCaptureFiles\\姿态";

}

jQuery(function($){
    $.datepicker.regional['zh-CN'] = {
        closeText: '关闭',
        prevText: '&#x3c;上月',
        nextText: '下月&#x3e;',
        currentText: '今天',
        monthNames: ['一月','二月','三月','四月','五月','六月',
        '七月','八月','九月','十月','十一月','十二月'],
        monthNamesShort: ['一','二','三','四','五','六',
        '七','八','九','十','十一','十二'],
        dayNames: ['星期日','星期一','星期二','星期三','星期四','星期五','星期六'],
        dayNamesShort: ['周日','周一','周二','周三','周四','周五','周六'],
        dayNamesMin: ['日','一','二','三','四','五','六'],
        weekHeader: '周',
        dateFormat: 'yy-mm-dd',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: true,
        yearSuffix: '年'};
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
});