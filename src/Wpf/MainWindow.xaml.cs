using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Wpf.Data;
using Wpf.Models;
using static Wpf.Models.ViewModelProperty;

namespace Wpf
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModelData.g;
            ViewModelData.g.SRList = SR;
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ViewModelData.g.FlyoutIsOpen)
            {
                ViewModelData.g.FlyoutIsOpen = false;
                e.Cancel = true;
            }

            if (ViewModelData.g.FlyoutOptionsIsOpen)
            {
                ViewModelData.g.FlyoutOptionsIsOpen = false;
                e.Cancel = true;
            }

            if (ViewModelData.g.FlyoutFilterIsOpen)
            {
                ViewModelData.g.FlyoutFilterIsOpen = false;
            }
        }


        private void ListBoxItemRemove_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModelData.g.SRModelList.Remove(ViewModelData.g.SRModelList.Where(w => w.Id.ToString() == ((Button)sender).Tag.ToString()).LastOrDefault());
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            FlyoutsControl_Options.DataContext = ViewModelData.g;

            try
            {
                ViewModelData.g.SRList.SelectedItem = ViewModelData.g.SelectedSRModel;
            }
            catch { }
            //    ViewModelData.g.FlyoutOptionsIsOpen = !ViewModelData.g.FlyoutOptionsIsOpen;
            ViewModelData.g.SRList.ScrollIntoView(ViewModelData.g.SRList.SelectedItem);
        }


        private void SR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ViewModelData.g.SRList.SelectedIndex >= 0)
            {

                ViewModelData.g.SelectedSRModel = (ViewModelProperty.SRModel)ViewModelData.g.SRList.SelectedItem;

                try
                {
                    ViewModelData.g.CTID.ToList().ForEach(f =>
                    {
                        if (ViewModelData.g.SelectedSRModel.TriggerID.Where(w => w == (int)f.TriggerDescription.DescriptionToEnum<TID>()).Count() > 0)
                        {
                            f.IsEnable = true;
                        }
                        else
                        {
                            f.IsEnable = false;
                        }
                    });
                }
                catch { }
            }
        }

        private void Button_FlyoutViewFilter(object sender, RoutedEventArgs e)
        {
            try
            {
                FlyoutsControl_Filter.DataContext = ViewModelData.g;
                ViewModelData.g.FlyoutFilterIsOpen = !ViewModelData.g.FlyoutFilterIsOpen;
                if (ViewModelData.g.FlyoutFilterIsOpen)
                {
                    ViewModelData.g.IsEnable_Filter = true;
                }
            }
            catch { }
        }

        private void ApplicationSetting_ButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModelData.g.FlyoutIsOpen = !ViewModelData.g.FlyoutIsOpen;
        }

        private void SRAdd_ButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModelData.g.SRModelList.Add(new ViewModelProperty.SRModel
            {
                TypeId = 0,
                TriggerID = new System.Collections.ObjectModel.ObservableCollection<long>() { 0 }
            });
            ViewModelData.g.SRList.SelectedIndex = ViewModelData.g.SRList.Items.Count - 1;
            ViewModelData.g.SRList.ScrollIntoView(ViewModelData.g.SRList.SelectedItem);
            ViewModelData.g.FlyoutOptionsIsOpen = true;
            ViewModelData.Save();
        }

        private void SRsave_ButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModelData.Save();
        }
    }
}
