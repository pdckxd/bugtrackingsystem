<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppliesManagement.ascx.cs" Inherits="DesktopModules.Web.AppliesManagement" %>
<div>
<h4>观测预约管理</h4>
<hr />
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="申请人">
            <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="日期">
            <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="时间段">
            <HeaderStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="状态">
            <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:HyperLinkField HeaderText="预约批准">
            <HeaderStyle Width="100px" />
            </asp:HyperLinkField>
        </Columns>
    </asp:GridView>
</div>