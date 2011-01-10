<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyApplies.ascx.cs" Inherits="DesktopModules.Web.MyApplies" %>
<%@ Register TagPrefix="ASPNETPortal" TagName="Title" Src="../../Shared/DesktopModuleTitle.ascx" %>

<script type="text/javascript">
    document.write('<style>.noscript { display: none; }</style>');

    $(function() {
    $("#" + DatePickerID()).datepicker();
    });
    </script>


        <div>
            <h4>
                观测预约申请</h4>
            <hr />
        </div>
        <div>
        <table>
         <tr>
                <td colspan="6">
                    选择日期:
                    <input type="text" id="datepicker" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        </div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table style="width: 100%;">
           <tr>
                <td colspan="6">
                    <asp:Button ID="btnOk" runat="server" Text="查询预约" onclick="btnOk_Click" />
                </td>
            </tr>
            <tr style="height: 30px;">
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
            <tr style="height: 30px;">
                <td>
                    <asp:CheckBox ID="CheckBox7" Text="6:00-7:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox8" Text="7:00-8:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox9" Text="8:00-9:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox10" Text="9:00-10:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox11" Text="10:00-11:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox12" Text="11:00-12:00" runat="server" />
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>
                    <asp:CheckBox ID="CheckBox13" Text="12:00-13:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox14" Text="13:00-14:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox15" Text="14:00-15:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox16" Text="15:00-16:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox17" Text="16:00-17:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox18" Text="17:00-18:00" runat="server" />
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>
                    <asp:CheckBox ID="CheckBox19" Text="18:00-19:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox20" Text="19:00-20:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox21" Text="20:00-21:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox22" Text="21:00-22:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox23" Text="22:00-23:00" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox24" Text="23:00-24:00" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
                <td align="right">
                    <asp:Button ID="btnUpdate" Text="确认申请" runat="server" 
                        onclick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
        <div>
            <h4>
                我的预约</h4>
            <hr />
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="15" AllowPaging="true">
                <Columns>
                    <asp:BoundField HeaderText="日期" DataField="Date" DataFormatString="{0:yyyy-MM-dd}">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="时间段" DataField="TimeRange">
                        <HeaderStyle Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="状态" DataField="Status">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">

    function DatePickerID() {
        return '<%= datepicker.ClientID %>';
    }

</script>
