<%@ Application Inherits="Nairc.KPWPortal.Global" %>

<script Language="C#" RunAt="server">

protected void Application_Start(Object sender, EventArgs e)
{
   log4net.Config.DOMConfigurator.Configure();
}

</script>