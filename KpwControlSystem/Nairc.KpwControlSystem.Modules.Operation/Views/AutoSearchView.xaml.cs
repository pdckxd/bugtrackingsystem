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
    /// Interaction logic for AutoSearchView.xaml
    /// </summary>
    public partial class AutoSearchView : UserControl
    {
        public AutoSearchView()
        {
            InitializeComponent();

            this.Model = new AutoSearchViewModel();
        }

        public AutoSearchViewModel Model
        {
            get
            {
                return DataContext as AutoSearchViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}
