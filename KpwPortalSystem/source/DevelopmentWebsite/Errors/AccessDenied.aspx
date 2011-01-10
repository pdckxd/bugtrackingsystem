<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Default.master" %>
<%@ Import namespace="Nairc.KPWPortal"%>

<%@ OutputCache Duration="36000" VaryByParam="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="server">
    <table width="500" border="0">
        <tbody>
            <tr>
                <td class="Normal">
                    <br />
                    <br />
                    <br />
                    <br />
                    <span class="Head">拒绝访问</span>
                    <br />
                    <br />
                    <hr noshade="noshade" size="1" />
                    <br />
                    你无权访问该页面，请联系管理员！
                    <br />
                    <br />
                    <a href="<%=Global.GetApplicationPath(Request)%>/DesktopDefault.aspx">返回深圳市天文观测系统首页</a>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
