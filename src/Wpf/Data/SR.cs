using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Newtonsoft.Json;

namespace Wpf.Data
{

    public class sr
    {

        private static int auto_increment = -1;

        [SQLite.AutoIncrement]
      //  [YamlMember(Alias = "序號", Order = 0)]
        [JsonProperty(PropertyName = "序號")]
        [Description("序號")]
        public int Id { get; set; } = System.Threading.Interlocked.Increment(ref auto_increment);

        [SQLite.Indexed]
      // [YamlMember(Alias = "類別號", Order = 1)]
        [JsonProperty(PropertyName = "類別號")]
        [Description("類別號")]
        public int TypeId { get; set; } = 0;

      //  [YamlMember(Alias = "觸發號", Order = 2)]
        [JsonProperty(PropertyName = "觸發號")]
        [Description("觸發號")]
        public ObservableCollection<long> TriggerID { get; set; } = new ObservableCollection<long>();

      //  [YamlMember(Alias = "啟用", Order = 3)]
        [JsonProperty(PropertyName = "啟用")]
        [Description("啟用")]
        public bool IsEnabled { get; set; } = false;

      //  [YamlMember(Alias = "條件", Order = 4)]
        [JsonProperty(PropertyName = "條件")]
        [Description("條件")]
        public string Condition { get; set; } = "";

       // [YamlMember(Alias = "值", Order = 5)]
        [JsonProperty(PropertyName = "值")]
        [Description("值")]
        public string Value { get; set; } = "";

        //[SQLite.NotNull]
        //[YamlMember(Alias = "類別")]
        //[JsonProperty(PropertyName = "類別")]
        //[Description("類別")]
        //public string SType
        //{
        //    get
        //    {
        //        SRType _srtype = (SRType)Enum.ToObject(typeof(SRType), TypeId);
        //        return _srtype.ToDescription();
        //    }
        //}

        //[YamlMember(Alias = "觸發")]
        //[JsonProperty(PropertyName = "觸發")]
        //[Description("觸發")]
        //public string STrigger
        //{
        //    get
        //    {
        //        if (TriggerID == null) { return "任何"; }
        //        if (TriggerID.Count == 0) { return "任何"; }
        //        string _strigger = string.Empty;


        //        TriggerID.ToList().ForEach(f =>
        //        {
        //            TRNo _srtype = (TRNo)Enum.ToObject(typeof(TRNo), f);
        //            _strigger += _srtype.ToDescription() + ",";
        //        });
        //        return _strigger.Substring(0, _strigger.Length - 1);
        //    }
        //}

       // [YamlMember(Alias = "觸發_指定QQ")]
        [JsonProperty(PropertyName = "觸發_指定QQ")]
        [Description("觸發_指定QQ")]
        public ObservableCollection<long> SUser { get; set; } = new ObservableCollection<long>();

       // [YamlMember(Alias = "觸發_指定群")]
        [JsonProperty(PropertyName = "觸發_指定群")]
        [Description("觸發_指定群")]
        public ObservableCollection<long> SGroup { get; set; } = new ObservableCollection<long>();

       // [YamlMember(Alias = "觸發_指定時段")]
        [JsonProperty(PropertyName = "觸發_指定時段")]
        [Description("觸發_指定時段")]
        public ObservableCollection<long> STime { get; set; } = new ObservableCollection<long>();

       // [YamlMember(Alias = "觸發_指定插件ID")]
        [JsonProperty(PropertyName = "觸發_指定插件ID")]
        [Description("觸發_指定插件ID")]
        public ObservableCollection<long> SPluginID { get; set; } = new ObservableCollection<long>();

       // [YamlMember(Alias = "對像群")]
        [JsonProperty(PropertyName = "對像群")]
        [Description("對像群")]
        public ObservableCollection<long> TGGroups { get; set; } = new ObservableCollection<long>();

       // [YamlMember(Alias = "對像QQ")]
        [JsonProperty(PropertyName = "對像QQ")]
        [Description("對像QQ")]
        public ObservableCollection<long> TGQQ { get; set; } = new ObservableCollection<long>();

        public void SetId(int id)
        {
            auto_increment = id;
        }

    }

    public enum TID : int
    {
        [Description("每次")]
        PS = 0,
        [Description("第一次")]
        FT = 1,
        [Description("指定QQ")]
        SU = 2,
        [Description("指定群")]
        SG = 3,
        [Description("指定時段")]
        ST = 4,
        [Description("指定插件ID")]
        SP = 5,
    }

    public enum SRID : int
    {

        [Description("前置")]
        FS = 0,
        [Description("後置")]
        LS = 1,
        [Description("插入")]
        IP = 2,
        [Description("加入")]
        IC = 3,
        [Description("取代")]
        RP = 4,
        [Description("攔截")]
        BL = 5,
        [Description("跳出")]
        BR = 6,
        //[Description("转发")]
        //FW = 7,
        //[Description("通知")]
        //IF = 8,

        //[Description("消除")]
        //RM = 4,
        //[Description("延迟")]
        //DL = 10,
        //[Description("储存")]
        //SV = 11,
        //[Description("合併")]
        //GB = 12,
    }

}
