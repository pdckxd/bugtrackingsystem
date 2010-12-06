<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="Users.aspx.cs" Inherits="Nairc.KpwPortal.Website.Accounts.Users" %>
 
<asp:Content ContentPlaceHolderID="Content" Runat="Server">
    <div>
        <div >
            <h3 align="left">
                创建新用户</h3>
                <hr />
                <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
                    <WizardSteps>
                        <asp:CreateUserWizardStep runat="server" />
                        <asp:CompleteWizardStep runat="server" />
                    </WizardSteps>
                </asp:CreateUserWizard>
        </div>
        <br />
        <div align="center" >
           <h3 align="left">
                用户管理</h3>
                <hr />
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
            <asp:BoundField HeaderText="用户名"  HeaderStyle-Width="120px" />
            <asp:BoundField HeaderText="E-Mail" HeaderStyle-Width="200px" />
            <asp:TemplateField HeaderText="角色" HeaderStyle-Width="250px" >
                <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" Text="管理员" runat="server" />
                <asp:CheckBox ID="CheckBox2" Text="操作员" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField HeaderText="编辑" />
            <asp:HyperLinkField HeaderText="删除" />
            </Columns>
        </asp:GridView>
        </div>
        
    </div>
</asp:Content>
