using CQ.Hook;
using Robot.API;
using Robot.Extension;
using Robot.Property;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wpf.Data;
using Wpf.Models;
using static Wpf.Models.ViewModelProperty;

namespace link.toroko.stringreplace
{
    public class Program
    {
        public static CancellationTokenSource close = new CancellationTokenSource();
        public static List<string> words = new List<string>();
        public static object locker = new object();
        public static void Init()
        {

            lock (locker)
            {
                Setup();
                Setup_OutPut();
                CQHook.Init(Pm, Gm, Dm);
                CQHook.Hook();
                ViewModelData.g.FlyoutIsOpen = false;
                ViewModelData.g.FlyoutOptionsIsOpen = false;
                ViewModelData.g.SRModelList = new System.Collections.ObjectModel.ObservableCollection<ViewModelProperty.SRModel>();
                ViewModelData.g.CTID = new System.Collections.ObjectModel.ObservableCollection<SRtriggerModel>();
                ViewModelData.g.CSRID = new System.Collections.ObjectModel.ObservableCollection<string>();
                ViewModelData.Load();
                ViewModelData.g.PrepareFiltering();

                foreach (SRID srid in Enum.GetValues(typeof(SRID)))
                {
                    ViewModelData.g.CSRID.Add(srid.ToDescription());
                }

                foreach (TID tid in Enum.GetValues(typeof(TID)))
                {
                    ViewModelData.g.CTID.Add(new SRtriggerModel { TriggerDescription = tid.ToDescription() });
                }

                RobotBase.isinit = true;
            }

        }
        
        private static long Pm(int ac, long id, string msg)
        {
        //    string gid = Guid.NewGuid().ToString("X");
            CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "原私聊消息", $"({ac})QQ: {id} {msg}");

            if (RobotBase.isenableplugin)
            {
                string outstr = msg;
                if (!PMessage(msg, ref outstr)) { return -1; }
                msg = outstr;
            }

            return CQAPI.SendPrivateMessage(ac, id, msg);
        }

        private static long Gm(int ac, long id, string msg)
        {
        //    string gid = Guid.NewGuid().ToString("X");
            CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "原群組消息", $"({ac})群: {id} {msg}");

            if (RobotBase.isenableplugin)
            {
                string outstr = msg;
                if (!PMessage(msg, ref outstr)) { return -1; }
                msg = outstr;
            }
            
            return CQAPI.SendGroupMessage(ac, id, msg);
        }

        private static long Dm(int ac, long msgid)
        {
            CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "原消息刪除", $"({ac})ID : {msgid}");
            return CQAPI.DeleteMessage(ac, msgid);
        }


        private static bool PMessage(string msg , ref string outstr, string gid ="")
        {
        
            string tmp = msg;
         //   lock (ViewModelData.g.SRModelList)
         //   {
                try
                {
                    if (ViewModelData.g.SRModelList.Where(wc => wc.IsEnabled).Where(w => (SRID)w.TypeId == SRID.BL && msg.Contains(w.Condition)).ToList().Count > 0) {
                   //     CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "觸發", $"[{gid}]攔截");
                        return false;
                    }

                    if (ViewModelData.g.SRModelList.Where(wc => wc.IsEnabled).Where(w => (SRID)w.TypeId == SRID.BR && msg.Contains(w.Condition)).ToList().Count > 0) {
                   //     CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "觸發", $"[{gid}]跳出");
                        return true;
                    }

                    ViewModelData.g.SRModelList.Where(wc => wc.IsEnabled).ToList().ForEach(f =>
                    {
                        if (msg.Contains(f.Condition))
                        {
                            switch ((SRID)f.TypeId)
                            {
                                case SRID.FS:
                                    tmp = f.Value + tmp;
                             //       CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "觸發", $"[{gid}]前置:{f.Id}");
                                    break;
                                case SRID.LS:
                                    tmp = tmp + f.Value;
                             //       CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "觸發", $"[{gid}]後置:{f.Id}");
                                    break;
                                case SRID.IP:
                                    tmp = tmp.Replace(f.Condition, f.Value + f.Condition);
                             //       CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "觸發", $"[{gid}]插入:{f.Id}");
                                    break;
                                case SRID.IC:
                                    tmp = tmp.Replace(f.Condition, f.Condition + f.Value);
                            //        CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "觸發", $"[{gid}]加入:{f.Id}");
                                    break;
                                case SRID.RP:
                                    tmp = tmp.Replace(f.Condition, f.Value);
                            //        CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_DEBUG, "觸發", $"[{gid}]取代:{f.Id}");
                                    break;
                            }
                        }

                    });
                }
                catch { }
        //    }

            outstr = tmp;
            return true;
        }

        public static void Main(string robotQQ, Int32 msgType, Int32 msgSubType, string msgSrc, string targetActive, string targetPassive, string msgContent, int messageid)
        {
        }

        private static void Setup()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.AppendPrivatePath(@"bin\");

            Directory.CreateDirectory("bin");
            Directory.CreateDirectory(RobotBase.appfolder );
            Directory.CreateDirectory(RobotBase.appfolder + "conf");

            try
            {
                IniFile i = new IniFile(RobotBase.appfolder + RobotBase.iniconf);
                long admin = 0;
                Int64.TryParse(i.IniReadValue(CQAPI.GetLoginQQ(RobotBase.CQ_AuthCode).ToString(), "AdminQQ"), out admin);
                if (admin > 0)
                {
                    RobotBase.AdminQQ = admin.ToString();
                }
            }
            catch { }
        }

        private static void Setup_OutPut()
        {

            Assembly assembly = Assembly.GetExecutingAssembly();

            Queue<string> LoadLibrary = new Queue<string>();
            LoadLibrary.Enqueue("EasyHook.dll");
            LoadLibrary.Enqueue("EasyHook32.dll");
            LoadLibrary.Enqueue("EasyLoad32.dll");
            LoadLibrary.Enqueue("EasyHook32Svc.exe");
            LoadLibrary.Enqueue("MahApps.Metro.dll");
            LoadLibrary.Enqueue("MahApps.Metro.IconPacks.Material.dll");
            LoadLibrary.Enqueue("MahApps.Metro.IconPacks.Modern.dll");
            LoadLibrary.Enqueue("ControlzEx.dll");
            LoadLibrary.Enqueue("System.Windows.Interactivity.exe");

            while (LoadLibrary.Count() != 0)
            {
                string resource = LoadLibrary.Dequeue();

                try
                {
                    using (var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{resource}"))
                    {
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        stream.Flush();
                        stream.Close();
                        string path = $@"{AppDomain.CurrentDomain.BaseDirectory}bin\{resource}";

                        if (!File.Exists(path))
                        {
                            File.WriteAllBytes(path, buffer);
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CQAPI.AddLog(RobotBase.CQ_AuthCode, CQAPI.LogPriority.CQLOG_ERROR, "ERROR", "UnhandledException");
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{args.Name.Split(',')[0]}.dll"))
            {

                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                return Assembly.Load(buffer);
            }
        }

    }
}
