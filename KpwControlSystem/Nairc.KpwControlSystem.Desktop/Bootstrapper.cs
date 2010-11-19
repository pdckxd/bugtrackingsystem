using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Nairc.KpwControlSystem.Modules.Operation;
using Nairc.KpwControlSystem.Desktop.Views;

namespace Nairc.KpwControlSystem.Desktop
{
    class Bootstrapper:UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            // WPF will set Application.Current.MainWindow to the first window shown and it clicked
            // you need to do is first set the shutdown mode temporarilly to ShutdownMode.OnExplicitShutdown, 
            // then show your logon form, the WPF system will go and set that window to Application.Current.MainWindow 
            // so we need to set that to null so when you show your shell that gets set as the main window. 
            // Once more thing is to then set the shutdown mode back to ShutdownMode.OnMainWindowClose
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            Login login = new Login();
            login.ShowDialog();

            Application.Current.MainWindow = null;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            return this.Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(OperationModule));
        }
    }
}
