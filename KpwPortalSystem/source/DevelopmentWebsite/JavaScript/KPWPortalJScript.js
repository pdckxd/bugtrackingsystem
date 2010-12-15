	
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

function FindStarByPosition(position) {
    WebApplication.KPWPortalWebService.FindStarByPosition(position, onSendCommandSucceeded);
    $get("result").innerHTML = "坐标找星...";
}

function FindStarByName(name) {
    WebApplication.KPWPortalWebService.FindStarByName(name, onSendCommandSucceeded);
    $get("result").innerHTML = "星名找星...";
}

function StopFindStar() {
    WebApplication.KPWPortalWebService.StopFindStar(onSendCommandSucceeded);
    $get("result").innerHTML = "停止找星...";
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


function CatchImage() {

    //var Netocx1 = document.getElementById("NetOCX1");
    //Netocx1.BMPCapturePicture("C:\kpw\OCXBMPCaptureFiles", true);
    $get("txtFolderMessge").innerHTML = "图片保存在文件夹 C:\\kpw\\OCXBMPCaptureFiles";
}

function StartVideo() {

    $get("txtFolderMessge").innerHTML = "开始录像....";
    //var Netocx1 = document.getElementById("NetOCX1");
    //Netocx1.BMPCapturePicture("C:\kpw\OCXBMPCaptureFiles", true);
    $get("txtFolderMessge").innerHTML = "正在录像....";
    $("#btnStartVideo").hide();
    $("#btnStopVideo").show();
}

function StopVideo() {

    // var Netocx1 = document.getElementById("NetOCX1");
    //Netocx1.BMPCapturePicture("C:\kpw\OCXBMPCaptureFiles", true);
    $("#btnStartVideo").show();
    $("#btnStopVideo").hide();
    $get("txtFolderMessge").innerHTML = "视频保存在文件夹 C:\\kpw\\OCXVideoCaptureFiles";
    
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