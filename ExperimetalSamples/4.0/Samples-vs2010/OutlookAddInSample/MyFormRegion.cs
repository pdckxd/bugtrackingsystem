using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookAddInSample
{
    public partial class MyFormRegion
    {
        #region Form Region Factory

        [Microsoft.Office.Tools.Outlook.FormRegionMessageClass("IPM.Appointment.Holiday")]
        [Microsoft.Office.Tools.Outlook.FormRegionName("OutlookAddInSample.MyFormRegion")]
        public partial class MyFormRegionFactory
        {
            private void InitializeManifest()
            {
                ResourceManager resources = new ResourceManager(typeof(MyFormRegion));
                this.Manifest.FormRegionType = Microsoft.Office.Tools.Outlook.FormRegionType.Replacement;
                this.Manifest.Title = resources.GetString("Title");
                this.Manifest.FormRegionName = resources.GetString("FormRegionName");
                this.Manifest.Description = resources.GetString("Description");
                this.Manifest.ShowInspectorCompose = true;
                this.Manifest.ShowInspectorRead = true;
                this.Manifest.ShowReadingPane = true;

            }

            // Occurs before the form region is initialized.
            // To prevent the form region from appearing, set e.Cancel to true.
            // Use e.OutlookItem to get a reference to the current Outlook item.
            private void MyFormRegionFactory_FormRegionInitializing(object sender, Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs e)
            {
            }
        }

        #endregion

        // Occurs before the form region is displayed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void MyFormRegion_FormRegionShowing(object sender, System.EventArgs e)
        {
        }

        // Occurs when the form region is closed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void MyFormRegion_FormRegionClosed(object sender, System.EventArgs e)
        {
        }
    }
}
