using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Wpf.Data;

namespace Wpf.Views
{


    public partial class FlyoutView_Filter : UserControl
    {

        public FlyoutView_Filter()
        {
            InitializeComponent();
            DataContext = ViewModelData.g;
        }
    }
}
