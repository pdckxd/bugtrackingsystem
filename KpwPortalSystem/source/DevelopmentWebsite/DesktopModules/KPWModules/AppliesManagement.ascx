<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppliesManagement.ascx.cs" Inherits="DesktopModules.Web.AppliesManagement" %>
<div>
<h4>观测预约管理</h4>
<hr />
</div>
<div>
<asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" OnRowUpdating="GridView_RowUpdating" OnDataBinding="GridView_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="申请人" DataField="Name">
            <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="日期" DataField="Date">
            <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="时间段" DataField="TimeRange">
            <HeaderStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="状态" DataField="Status">
            <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:CommandField HeaderText="预约批准"  HeaderStyle-Width="100px" />
        </Columns>
    </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>