using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Wpf.Data;

namespace Wpf.Models
{

    public class ViewModelProperty : ViewModelBase
    {
        private ICollectionView _SRCollectionView;
        private string _SRfilterText;
        private bool _IsEnable_Filter = false;
        private Boolean _flyoutIsOpen = false;
        private Boolean _flyoutOptionsIsOpen = false;
        private Boolean _flyoutFilterIsOpen = false;
        private int _ListHeight = 540;

        public void PrepareFiltering()
        {
            this._SRCollectionView = CollectionViewSource.GetDefaultView(this.SRModelList);
            this._SRCollectionView.Filter = o => this.FilterSRPredicate(this.SRFilterText, (ISRViewModel)o);
        }

        private bool FilterSRPredicate(string filterText, ISRViewModel iSRViewModel)
        {
            if (!IsEnable_Filter)
            {
                return true;
            }

            bool a = string.IsNullOrWhiteSpace(filterText)
                   || iSRViewModel.Condition.IndexOf(filterText, StringComparison.CurrentCultureIgnoreCase) >= 0
                   || (!string.IsNullOrWhiteSpace(iSRViewModel.Condition) && iSRViewModel.Condition.IndexOf(filterText, StringComparison.CurrentCultureIgnoreCase) >= 0);

            return a;
        }

        public string SRFilterText
        {
            get { return _SRfilterText; }
            set
            {
                if (value == _SRfilterText) return;
                _SRfilterText = value;
                OnPropertyChanged();
                this._SRCollectionView.Refresh();
            }
        }

        public bool IsEnable_Filter
        {
            get { return _IsEnable_Filter; }
            set
            {
                if (value == _IsEnable_Filter) return;
                _IsEnable_Filter = value;
                OnPropertyChanged();
                this._SRCollectionView.Refresh();
            }
        }




        public interface ISRViewModel
        {
            int Id { get; set; }
            int TypeId { get; set; }
            ObservableCollection<long> TriggerID { get; set; }
            bool IsEnabled { get; set; }
            string Condition { get; set; }
            string Value { get; set; }
            //string SType { get; }
            //string STrigger { get; }
            ObservableCollection<long> SUser { get; set; }
            ObservableCollection<long> SGroup { get; set; }
            ObservableCollection<long> STime { get; set; }
            ObservableCollection<long> SPluginID { get; set; }
            ObservableCollection<long> TGGroups { get; set; }
            ObservableCollection<long> TGQQ { get; set; }
        }


        public class SRModel : ViewModelBase, ISRViewModel
        {

            private sr srList;

            public SRModel()
            {
                srList = new sr();
            }

            [SQLite.AutoIncrement]
          //  [YamlMember(Alias = "序号", Order = 0)]
            [JsonProperty(PropertyName = "序号")]
            [Description("序号")]
            public int Id
            {
                get
                {
                    return srList.Id;
                }
                set
                {
                    srList.Id = value;
                    OnPropertyChanged();
                }
            }

            [SQLite.Indexed]
          //  [YamlMember(Alias = "类别号", Order = 1)]
            [JsonProperty(PropertyName = "类别号")]
            [Description("类别号")]
            public int TypeId
            {
                get
                {
                    return srList.TypeId;
                }
                set
                {
                    srList.TypeId = value;
                    OnPropertyChanged();
                //    OnPropertyChanged("SType");
                }
            }

          //  [YamlMember(Alias = "触发号", Order = 2)]
            [JsonProperty(PropertyName = "触发号")]
            [Description("触发号")]
            public ObservableCollection<long> TriggerID
            {
                get
                {
                    return srList.TriggerID;
                }
                set
                {
                    srList.TriggerID = value;
                    OnPropertyChanged();
                 //   OnPropertyChanged("STrigger");
                }
            }

            //private void OnListChanged(object sender, NotifyCollectionChangedEventArgs e)
            //{
            //    // create any new elements
            //    if (e.NewItems != null)
            //        foreach (long item in e.NewItems)
            //            this.TriggerID.Add(item);

            //    // remove any new elements
            //    if (e.OldItems != null)
            //        foreach (long item in e.OldItems)
            //            this.TriggerID.Remove(item);
            //              //  this.MyListItemViewModels.First(x => x.Model == item)
            //             //  );
            //}


          //  [YamlMember(Alias = "启用", Order = 3)]
            [JsonProperty(PropertyName = "启用")]
            [Description("启用")]
            public bool IsEnabled
            {
                get
                {
                    return srList.IsEnabled;
                }
                set
                {
                    srList.IsEnabled = value;
                    OnPropertyChanged();
                }
            }

          //  [YamlMember(Alias = "条件", Order = 4)]
            [JsonProperty(PropertyName = "条件")]
            [Description("条件")]
            public string Condition
            {
                get
                {
                    return srList.Condition;
                }
                set
                {
                    srList.Condition = value;
                    OnPropertyChanged();
                }
            }

           // [YamlMember(Alias = "值", Order = 5)]
            [JsonProperty(PropertyName = "值")]
            [Description("值")]
            public string Value
            {
                get
                {
                    return srList.Value;
                }
                set
                {
                    srList.Value = value;
                    OnPropertyChanged();
                }
            }

            //[SQLite.NotNull]
            //[YamlMember(Alias = "类别")]
            //[JsonProperty(PropertyName = "类别")]
            //[Description("类别")]
            //public string SType
            //{
            //    get
            //    {
            //        return srList.SType;
            //    }
            //}

            //[YamlMember(Alias = "触发")]
            //[JsonProperty(PropertyName = "触发")]
            //[Description("触发")]
            //public string STrigger
            //{
            //    get
            //    {
            //        return srList.STrigger;
            //    }
            //}

           // [YamlMember(Alias = "触发_指定QQ")]
            [JsonProperty(PropertyName = "触发_指定QQ")]
            [Description("触发_指定QQ")]
            public ObservableCollection<long> SUser
            {
                get
                {
                    return srList.SUser;
                }
                set
                {
                    srList.SUser = value;
                    OnPropertyChanged();
                }
            }

           // [YamlMember(Alias = "触发_指定群")]
            [JsonProperty(PropertyName = "触发_指定群")]
            [Description("触发_指定群")]
            public ObservableCollection<long> SGroup
            {
                get
                {
                    return srList.SGroup;
                }
                set
                {
                    srList.SGroup = value;
                    OnPropertyChanged();
                }
            }

           // [YamlMember(Alias = "触发_指定时段")]
            [JsonProperty(PropertyName = "触发_指定时段")]
            [Description("触发_指定时段")]
            public ObservableCollection<long> STime
            {
                get
                {
                    return srList.STime;
                }
                set
                {
                    srList.STime = value;
                    OnPropertyChanged();
                }
            }

           // [YamlMember(Alias = "触发_指定插件ID")]
            [JsonProperty(PropertyName = "触发_指定插件ID")]
            [Description("触发_指定插件ID")]
            public ObservableCollection<long> SPluginID
            {
                get
                {
                    return srList.SPluginID;
                }
                set
                {
                    srList.SPluginID = value;
                    OnPropertyChanged();
                }
            }

           // [YamlMember(Alias = "对像群")]
            [JsonProperty(PropertyName = "对像群")]
            [Description("对像群")]
            public ObservableCollection<long> TGGroups
            {
                get
                {
                    return srList.TGGroups;
                }
                set
                {
                    srList.TGGroups = value;
                    OnPropertyChanged();
                }
            }

          //  [YamlMember(Alias = "对像QQ")]
            [JsonProperty(PropertyName = "对像QQ")]
            [Description("对像QQ")]
            public ObservableCollection<long> TGQQ
            {
                get
                {
                    return srList.TGQQ;
                }
                set
                {
                    srList.TGQQ = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SRModel> _SRModelList = new ObservableCollection<SRModel>();
        public ObservableCollection<SRModel> SRModelList
        {
            set
            {
                SetField(ref _SRModelList, value);
            }
            get
            {
                return _SRModelList;
            }
        }

        private SRModel _SelectedSRModel = new SRModel();
        public SRModel SelectedSRModel
        {

            set
            {
                SetField(ref _SelectedSRModel, value);
            }
            get
            {
                return _SelectedSRModel;
            }
        }

        private ListBox _SRList = new ListBox();
        public ListBox SRList
        {
            set
            {
                SetField(ref _SRList, value);
            }
            get
            {
                return _SRList;
            }
        }


        public Boolean FlyoutIsOpen
        {
            get
            {
                return _flyoutIsOpen;
            }
            set
            {
                _flyoutIsOpen = value;
                OnPropertyChanged();
            }
        }

        public Boolean FlyoutOptionsIsOpen
        {
            get
            {
                return _flyoutOptionsIsOpen;
            }
            set
            {
                _flyoutOptionsIsOpen = value;
                ListBoxShort = value;
                OnPropertyChanged();
            }
        }

        public Boolean FlyoutFilterIsOpen
        {
            get
            {
                return _flyoutFilterIsOpen;
            }
            set
            {
                _flyoutFilterIsOpen = value;
                OnPropertyChanged();
            }
        }

        public int ListHeight
        {
            get
            {
                return _ListHeight;
            }
            set
            {
                _ListHeight = value;
                OnPropertyChanged();
            }
        }

        public Boolean ListBoxShort
        {
            get
            {
                return ListHeight == 540;
            }
            set
            {
                ListHeight = value ? 300 : 540;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<string> _CSRID = new ObservableCollection<string>();

        public ObservableCollection<string> CSRID
        {
            set
            {
                SetField(ref _CSRID, value);
            }
            get
            {
                return _CSRID;
            }
        }

        public class SRtrigger
        {
            public string TriggerDescription { get; set; }
            public bool IsEnable { get; set; }
        }

        public class SRtriggerModel : ViewModelBase
        {
            SRtrigger s = new SRtrigger();

            public string TriggerDescription {
                get
                {
                    return s.TriggerDescription;
                }
                set
                {
                    s.TriggerDescription = value;
                    OnPropertyChanged();
                }
            }
            public bool IsEnable {
                get
                {
                    return s.IsEnable;
                }
                set
                {
                    s.IsEnable = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SRtriggerModel> _CTID = new ObservableCollection<SRtriggerModel>();

        public ObservableCollection<SRtriggerModel> CTID
        {
            set
            {
                SetField(ref _CTID, value);
            }
            get
            {
                return _CTID;
            }
        }



    }

}
