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
                    <span class="Head">�ܾ�����</span>
                    <br />
                    <br />
                    <hr noshade="noshade" size="1" />
                    <br />
                    ����Ȩ���ʸ�ҳ�棬����ϵ����Ա��
                    <br />
                    <br />
                    <a href="<%=Global.GetApplicationPath(Request)%>/DesktopDefault.aspx">�������������Ĺ۲�ϵͳ��ҳ</a>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
