using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Newtonsoft.Json;
using Wpf.Data;
using Wpf.Models;
using static Wpf.Models.ViewModelProperty;

namespace Wpf.Views
{

    public partial class FlyoutView_Options : UserControl
    {
        public FlyoutView_Options()
        {
            InitializeComponent();
            DataContext = ViewModelData.g;
        }

        private void CloseLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModelData.g.FlyoutOptionsIsOpen = false;
        }

        private void ToggleSwitch_IsCheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
