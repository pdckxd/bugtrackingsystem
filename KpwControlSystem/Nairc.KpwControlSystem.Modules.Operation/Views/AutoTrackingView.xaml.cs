using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nairc.KpwControlSystem.Modules.Operation.ViewModels;

namespace Nairc.KpwControlSystem.Modules.Operation.Views
{
    /// <summary>
    /// Interaction logic for AutoTrackingView.xaml
    /// </summary>
    public partial class AutoTrackingView : UserControl
    {
        public AutoTrackingView()
        {
            InitializeComponent();

            this.Model = new AutoTrackingViewModel();
        }

        public AutoTrackingViewModel Model
        {
            get
            {
                return DataContext as AutoTrackingViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}
