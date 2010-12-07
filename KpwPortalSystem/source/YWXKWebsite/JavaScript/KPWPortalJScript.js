	
/*Jquery Vertival tabs--------*/
$(function() {
    $("#tabs").tabs().addClass('ui-tabs-vertical ui-helper-clearfix');
    $("#tabs li").removeClass('ui-corner-top').addClass('ui-corner-left');
});



function SendCommand(command) {
    YWXKPortalApp.KPWPortalWebService.SendCommand(command, onSendCommandSucceeded);
    $get("result").innerHTML = command;
}

function FindStarByPosition(RaH,RaM,RaS,DecFlag,DecD,DecM,DecS) {
    YWXKPortalApp.KPWPortalWebService.FindStarByPosition(RaH, RaM, RaS, DecFlag, DecD, DecM, DecS, onSendCommandSucceeded);
    $get("result").innerHTML = "坐标找星...";
}

function FindStarByPosition(position) {
    YWXKPortalApp.KPWPortalWebService.FindStarByPosition(position, onSendCommandSucceeded);
    $get("result").innerHTML = "坐标找星...";
}

function FindStarByName(name) {
    YWXKPortalApp.KPWPortalWebService.FindStarByName(name, onSendCommandSucceeded);
    $get("result").innerHTML = "星名找星...";
}

function StopFindStar() {
    YWXKPortalApp.KPWPortalWebService.StopFindStar(onSendCommandSucceeded);
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
    YWXKPortalApp.KPWPortalWebService.TakePicture(grabTime, onTakePictureSucceeded);
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

function CatchImage() {

    var Netocx1 = document.getElementById("NetOCX1");
    Netocx1.BMPCapturePicture("C:\OCXBMPCaptureFiles", true);
}

function Stop1() {
    var Netocx1 = document.getElementById("NetOCX1");
    Netocx1.StopRealPlay();
    Netocx1.Logout();
}