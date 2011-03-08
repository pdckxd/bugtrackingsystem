namespace OutlookAddInSample
{
    partial class MyFormRegion : Microsoft.Office.Tools.Outlook.ImportedFormRegionBase
    {
        private Microsoft.Office.Interop.Outlook.OlkInfoBar olkInfoBar1;
        private Microsoft.Office.Interop.Outlook._DDocSiteControl _DocSiteControl1;
        private Microsoft.Office.Interop.Outlook.OlkDateControl olkEndDateControl;
        private Microsoft.Office.Interop.Outlook.OlkTimeControl olkEndTimeControl;
        private Microsoft.Office.Interop.Outlook.OlkTimeControl olkStartTimeControl;
        private Microsoft.Office.Interop.Outlook.OlkDateControl olkStartDateControl;
        private Microsoft.Office.Interop.Outlook.OlkFrameHeader olkFrameHeader;
        private Microsoft.Office.Interop.Outlook.OlkCategory olkCategory;
        private Microsoft.Office.Interop.Outlook.OlkLabel lblStartTime;
        private Microsoft.Office.Interop.Outlook.OlkLabel lblEndTime;
        private Microsoft.Office.Interop.Outlook.OlkCheckBox allDayEventCheckBox;

        public MyFormRegion(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
            : base(Globals.Factory, formRegion)
        {
            this.FormRegionShowing += new System.EventHandler(this.MyFormRegion_FormRegionShowing);
            this.FormRegionClosed += new System.EventHandler(this.MyFormRegion_FormRegionClosed);
        }

        protected override void InitializeControls()
        {
            this.olkInfoBar1 = (Microsoft.Office.Interop.Outlook.OlkInfoBar)GetFormRegionControl("OlkInfoBar1");
            this._DocSiteControl1 = (Microsoft.Office.Interop.Outlook._DDocSiteControl)GetFormRegionControl("_DocSiteControl1");
            this.olkEndDateControl = (Microsoft.Office.Interop.Outlook.OlkDateControl)GetFormRegionControl("OlkEndDateControl");
            this.olkEndTimeControl = (Microsoft.Office.Interop.Outlook.OlkTimeControl)GetFormRegionControl("OlkEndTimeControl");
            this.olkStartTimeControl = (Microsoft.Office.Interop.Outlook.OlkTimeControl)GetFormRegionControl("OlkStartTimeControl");
            this.olkStartDateControl = (Microsoft.Office.Interop.Outlook.OlkDateControl)GetFormRegionControl("OlkStartDateControl");
            this.olkFrameHeader = (Microsoft.Office.Interop.Outlook.OlkFrameHeader)GetFormRegionControl("OlkFrameHeader");
            this.olkCategory = (Microsoft.Office.Interop.Outlook.OlkCategory)GetFormRegionControl("OlkCategory");
            this.lblStartTime = (Microsoft.Office.Interop.Outlook.OlkLabel)GetFormRegionControl("lblStartTime");
            this.lblEndTime = (Microsoft.Office.Interop.Outlook.OlkLabel)GetFormRegionControl("lblEndTime");
            this.allDayEventCheckBox = (Microsoft.Office.Interop.Outlook.OlkCheckBox)GetFormRegionControl("AllDayEventCheckBox");

        }

        public partial class MyFormRegionFactory : Microsoft.Office.Tools.Outlook.IFormRegionFactory
        {
            public event Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler FormRegionInitializing;

            private Microsoft.Office.Tools.Outlook.FormRegionManifest _Manifest;

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public MyFormRegionFactory()
            {
                this._Manifest = Globals.Factory.CreateFormRegionManifest();
                this.InitializeManifest();
                this.FormRegionInitializing += new Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler(this.MyFormRegionFactory_FormRegionInitializing);
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public Microsoft.Office.Tools.Outlook.FormRegionManifest Manifest
            {
                get
                {
                    return this._Manifest;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            Microsoft.Office.Tools.Outlook.IFormRegion Microsoft.Office.Tools.Outlook.IFormRegionFactory.CreateFormRegion(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
            {
                MyFormRegion form = new MyFormRegion(formRegion);
                form.Factory = this;
                return form;
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            byte[] Microsoft.Office.Tools.Outlook.IFormRegionFactory.GetFormRegionStorage(object outlookItem, Microsoft.Office.Interop.Outlook.OlFormRegionMode formRegionMode, Microsoft.Office.Interop.Outlook.OlFormRegionSize formRegionSize)
            {
                System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MyFormRegion));
                return (byte[])resources.GetObject("HolidayFormRegion");
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            bool Microsoft.Office.Tools.Outlook.IFormRegionFactory.IsDisplayedForItem(object outlookItem, Microsoft.Office.Interop.Outlook.OlFormRegionMode formRegionMode, Microsoft.Office.Interop.Outlook.OlFormRegionSize formRegionSize)
            {
                if (this.FormRegionInitializing != null)
                {
                    Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs cancelArgs = Globals.Factory.CreateFormRegionInitializingEventArgs(outlookItem, formRegionMode, formRegionSize, false);
                    this.FormRegionInitializing(this, cancelArgs);
                    return !cancelArgs.Cancel;
                }
                else
                {
                    return true;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            Microsoft.Office.Tools.Outlook.FormRegionKindConstants Microsoft.Office.Tools.Outlook.IFormRegionFactory.Kind
            {
                get
                {
                    return Microsoft.Office.Tools.Outlook.FormRegionKindConstants.Ofs;
                }
            }
        }
    }

    partial class WindowFormRegionCollection
    {
        internal MyFormRegion MyFormRegion
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.GetType() == typeof(MyFormRegion))
                        return (MyFormRegion)item;
                }
                return null;
            }
        }
    }
}
