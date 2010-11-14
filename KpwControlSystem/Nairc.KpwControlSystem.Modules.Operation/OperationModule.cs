using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Nairc.KpwControlSystem.Modules.Operation.Views;

namespace Nairc.KpwControlSystem.Modules.Operation
{
    public class OperationModule:IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;

        public OperationModule(IRegionViewRegistry registry)
        {
            this.regionViewRegistry = registry;
        }
        public void Initialize()
        {
            this.regionViewRegistry.RegisterViewWithRegion("ActionRegion", typeof(DirectionControlView));
            this.regionViewRegistry.RegisterViewWithRegion("ActionRegion", typeof(AutoSearchView));
            this.regionViewRegistry.RegisterViewWithRegion("ActionRegion", typeof(AutoTrackingView));
            this.regionViewRegistry.RegisterViewWithRegion("ActionRegion", typeof(SettingsView));

        }
    }
}
