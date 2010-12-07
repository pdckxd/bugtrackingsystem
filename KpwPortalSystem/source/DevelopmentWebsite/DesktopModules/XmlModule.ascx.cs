using System;
using System.IO;
using System.Web.UI;
using Nairc.KPWPortal;

namespace DesktopModules.Web
{
    public partial class XmlModule : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control uses
        // the Portal configuration system to obtain an xml document
        // and xsl/t transform file location.  It then sets these
        // properties on an <asp:Xml> server control.
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            String xmlsrc = (String) Settings["xmlsrc"];

            if ((xmlsrc != null) && (xmlsrc != ""))
            {
                if (File.Exists(Server.MapPath(xmlsrc)))
                {
                    xml1.DocumentSource = xmlsrc;
                }
                else
                {
                    Controls.Add(
                        new LiteralControl("<" + "br" + "><" + "span class=NormalRed" + ">" + "File " + xmlsrc +
                                           " not found.<" + "br" + ">"));
                }
            }

            String xslsrc = (String) Settings["xslsrc"];

            if ((xslsrc != null) && (xslsrc != ""))
            {
                if (File.Exists(Server.MapPath(xslsrc)))
                {
                    xml1.TransformSource = xslsrc;
                }
                else
                {
                    Controls.Add(
                        new LiteralControl("<" + "br" + "><" + "span class=NormalRed>File " + xslsrc + " not found.<" +
                                           "br" + ">"));
                }
            }
        }

    }
}
