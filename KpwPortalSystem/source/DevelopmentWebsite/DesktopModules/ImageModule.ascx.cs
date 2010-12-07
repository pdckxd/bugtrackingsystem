using System;
using Nairc.KPWPortal;

namespace DesktopModules.Web
{
    public partial class ImageModule : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control uses
        // the Portal configuration system to obtain image details.
        // It then sets these properties on an <asp:Image> server control.
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            String imageSrc = (String) Settings["src"];
            String imageHeight = (String) Settings["height"];
            String imageWidth = (String) Settings["width"];

            // Set Image Source, Width and Height Properties
            if ((imageSrc != null) && (imageSrc != ""))
            {
                Image1.ImageUrl = imageSrc;
            }

            if ((imageWidth != null) && (imageWidth != ""))
            {
                Image1.Width = Int32.Parse(imageWidth);
            }

            if ((imageHeight != null) && (imageHeight != ""))
            {
                Image1.Height = Int32.Parse(imageHeight);
            }
        }
    }
}