using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Robot.Property.RobotProperty;

namespace Robot.Property
{
    public static class RobotBase
    {
        public static RobotType robot = RobotType.MPQ;
        public static int CQ_AuthCode = 0;
        public static string PluginName = "stringreplace";
        public static string ApiVer = "9";
        public static string AppID = "link.toroko.stringreplace";
        public static string AppName = "MPG(消息前置处理助手)";
        public static string LoginQQ = "";
        public static string AdminQQ = "";
        public static bool isinit = false;
        public static bool isinitFileMonitor = false;
        public static bool blockallmessages = false;
        public static bool isenableplugin = false;
        public static string appfolder = "";
        public static string iniconf = $"conf/init.ini";
        public static string data = $"db/data.json";
    }

    public static class RobotProperty
    {
        public enum RobotType : int
        {
            Test = -1,
            CQ = 0,
            MPQ = 1,
            Amanda = 2,
            IRQQ = 3,
            Huajing = 4,
            QY = 5
        }
    }

    public class Response
    {
        public Int32 msgType { get; set; }
        public Int32 msgSubType { get; set; }
        public string msgSrc { get; set; }
        public string targetActive { get; set; }
        public string msgContent { get; set; }
        public string robotQQ { get; set; }
    }
}
