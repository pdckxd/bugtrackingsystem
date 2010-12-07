<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.Signin" CodeBehind="Signin.ascx.cs" %>

<hr width="98%" noshade="noshade" size="1" />
<span class="SubSubHead" style="HEIGHT: 20px">账号登陆</span> 
<br />
<span class="Normal">用户名或Email:</span> 
<br />
<asp:TextBox id="email" runat="server" cssclass="NormalTextBox" width="130" columns="9"></asp:TextBox>
<br />
<span class="Normal">密码:</span> 
<br />
<asp:TextBox id="password" runat="server" cssclass="NormalTextBox" width="130" columns="9" textmode="password"></asp:TextBox>
<br />
<asp:checkbox class="Normal" id="RememberCheckbox" runat="server" Text="记住登陆"></asp:checkbox>
<table cellspacing="0" cellpadding="4" width="100%" border="0">
    <tbody>
        <tr>
            <td>
                <asp:ImageButton id="SigninBtn" onclick="LoginBtn_Click" runat="server" ImageUrl="~/images/signin.gif"></asp:ImageButton>
                <br />
                <a href="Admin/Register.aspx"><img src="images/register.gif" border="0" /></a> 
                <asp:label class="NormalRed" id="Message" runat="server"></asp:label>
            </td>
        </tr>
    </tbody>
</table>
<br />
