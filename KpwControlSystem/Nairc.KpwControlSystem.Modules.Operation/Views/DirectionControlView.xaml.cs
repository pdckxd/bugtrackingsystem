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
    /// Interaction logic for DirectionControlView.xaml
    /// </summary>
    public partial class DirectionControlView : UserControl
    {
        public DirectionControlView()
        {
            InitializeComponent();

            this.Model = new DirectionControlViewModel();
        }

        public DirectionControlViewModel Model
        {
            get
            {
                return DataContext as DirectionControlViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello,KPW.");
        }
    
    
    }
}
