<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Kpw60Pictures.ascx.cs" Inherits="DesktopModules.Web.Kpw60Pictures" %>
	
<table style="width: 100%;">
    <tr>
        <td colspan="2">
           选择日期: <input type="text" id="datepicker" runat="server"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <input type="button" id="btnOK" runat="server" value="确定" />
        </td>
    </tr>
    <tr style="height:560px;">
        <td>
            &nbsp;
        </td>
        <td style="width:220px;">
            &nbsp;
        </td>
    </tr>
</table>

<script type="text/javascript">

    function DatePickerID() {
        return '<%= datepicker.ClientID %>';
    }

</script>
