<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyApplies.ascx.cs" Inherits="DesktopModules.Web.MyApplies" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../../Shared/DesktopModuleTitle.ascx" %>

<div>
<h4>观测预约申请</h4>
<hr />
</div>
<table style="width: 100%;">
    <tr>
        <td colspan="6">
           选择日期: <input type="text" id="datepicker" runat="server"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <input type="button" id="btnOK" runat="server" value="查询预约" />
        </td>
    </tr>
    <tr style="height:30px;">
        <td>
            <asp:CheckBox ID="CheckBox1" Text="00:00-1:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox2" Text="1:00-2:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox3" Text="2:00-3:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox4" Text="3:00-4:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox5" Text="4:00-5:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox6" Text="5:00-6:00" runat="server" />
        </td>
    </tr>
    <tr style="height:30px;">
        <td>
            <asp:CheckBox ID="CheckBox7" Text="18:00-19:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox8" Text="19:00-20:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox9" Text="20:00-21:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox10" Text="21:00-22:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox11" Text="22:00-23:00" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="CheckBox12" Text="23:00-24:00" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="5"></td>
        <td align="right">
        <asp:Button ID="btnUpdate" Text="确认申请"  runat="server"/>
        </td>
    </tr>
</table>
<div>
<h4>我的预约</h4>
<hr />
</div>
<div>
</div>