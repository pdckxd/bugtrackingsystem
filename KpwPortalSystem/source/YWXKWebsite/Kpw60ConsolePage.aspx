<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kpw60ConsolePage.aspx.cs" Inherits="YWXKPortal.Web.Kpw60ConsolePage" %>
<%@ Register TagPrefix="kpw60Console" TagName="kpw60" Src="Kpw60Console.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="~/css/KpwStyle.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" media="screen,projection" type="text/css" href="~/CrystalX/css/main.css" />
    <link rel="stylesheet" media="print" type="text/css" href="~/CrystalX/css/print.css" />
    <link rel="stylesheet" media="aural" type="text/css" href="~/CrystalX/css/aural.css" />
    <link rel="stylesheet" href="~/JavaScript/SexyButtons/sexybuttons.css" type="text/css" />
    <link type="text/css" href="~/css/redmond/jquery-ui-1.8.1.custom.css" rel="stylesheet" />
    
    <script type="text/javascript" src="JavaScript/jquery-1.4.1.js"></script>  
    <script type="text/javascript" src="JavaScript/jquery-ui-1.8.1.custom.min.js"></script>      
    <script src="JavaScript/KPWPortalJScript.js" type="text/javascript"></script>
    
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
          <Scripts>
              <asp:ScriptReference Path="~/JavaScript/KPWPortalJScript.js" />
          </Scripts>
          <Services>
            <asp:ServiceReference Path="~/KPWPortalWebService.asmx" />
         </Services>
    </asp:ScriptManager>
    <div align="center">
    <div style="width:780px;" >
    <div id="loginDiv" runat="server" style="vertical-align:middle; margin-top:100px">
        <asp:Login ID="Login1" runat="server" BackColor="Transparent" 
            BorderColor="#3399FF" BorderPadding="4" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Arial Black" Font-Size="Medium" 
            ForeColor="#333333" Width="350px" Height="150px" FailureText="登陆失败，请重试！" 
            LoginButtonText="登陆" PasswordLabelText="密码:" 
            PasswordRequiredErrorMessage="请输入密码." RememberMeText="记住我" TitleText="登陆" 
            UserNameLabelText="用户名:" UserNameRequiredErrorMessage="请输入用户名." 
            UserName="guest" DisplayRememberMe="false" 
            onauthenticate="Login1_Authenticate">
            <TextBoxStyle Font-Size="0.8em" />
            <LoginButtonStyle BackColor="#336699" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Names="Arial" Font-Size="0.8em" ForeColor="White" 
                Font-Bold="True" Width="70px" />
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
                ForeColor="White" />
        </asp:Login>
    </div>
    <div id="controlDiv" runat="server">
    <%--<kpw60Console:kpw60 ID="kpw60Console" runat="server" ></kpw60Console:kpw60>--%>
    </div>
    </div>
    </div>    
    </form>
</body>
</html>
