	
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