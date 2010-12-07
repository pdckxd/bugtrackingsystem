<%@ Control Language="C#" AutoEventWireup="true" Inherits="DesktopModules.Web.ImageModule" CodeBehind="ImageModule.ascx.cs" %>
<%@ Register TagPrefix="Portal" TagName="Title" Src="../Shared/DesktopModuleTitle.ascx" %>

<portal:title id="UserControl1" runat="server" EditUrl="~/DesktopModules/EditImage.aspx" EditText="Edit"></portal:title>

<asp:image id="Image1" runat="server" border="0"></asp:image>
<br />