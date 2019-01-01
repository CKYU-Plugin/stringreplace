using Robot.API;
using Robot.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static CQ.Hook.CQHookEvent;

namespace CQ.Hook
{
    public class CQHook
    {
        private static EasyHook.LocalHook CQ_sendPrivateMsgHook;
        private static EasyHook.LocalHook CQ_sendGroupMsgHook;
        private static EasyHook.LocalHook CQ_deleteMsgHook;

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U8)]
        delegate long CQ_sendPrivateMsg_Delegate(int ac, [MarshalAs(UnmanagedType.U8)] long qqid, [MarshalAs(UnmanagedType.BStr)] string msg);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U8)]
        delegate long CQ_sendGroupMsg_Delegate(int ac, [MarshalAs(UnmanagedType.U8)] long grougid, [MarshalAs(UnmanagedType.BStr)] string msg);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U8)]
        delegate long CQ_deleteMsg_Delegate(int ac, [MarshalAs(UnmanagedType.U8)] long msgid);

        private static event EventHandler<CQ_sendMsg_Response> PrivateMsgResponder;
        private static event EventHandler<CQ_sendMsg_Response> GroupMsgResponder;
        private static event EventHandler<CQ_sendMsg_Response> DeleteMsgResponder;

        private static Func<int, long, string, long> PrivateMsgAction;
        private static Func<int, long, string, long> GroupMsgAction;
        private static Func<int, long, long> DeleteMsgAction;

        private static object locker = new object();


        public static void Init(Func<int, long, string, long> PrivateMsg, Func<int, long, string, long> GroupMsg, Func<int, long, long> DeleteMsg)
        {
            lock (locker)
            {
                PrivateMsgAction = PrivateMsg;
                GroupMsgAction = GroupMsg;
                DeleteMsgAction = DeleteMsg;
            }
        }

        public static void Hook(EventHandler<CQ_sendMsg_Response> PrivateMsg = null, EventHandler<CQ_sendMsg_Response> GroupMsg = null, EventHandler<CQ_sendMsg_Response> DeleteMsg = null)
        {
            CQ_sendPrivateMsgHook = EasyHook.LocalHook.Create(
EasyHook.LocalHook.GetProcAddress("CQP.dll", "CQ_sendPrivateMsg"), new CQ_sendPrivateMsg_Delegate(CQ_sendPrivateMsg_Hook), null);
            CQ_sendGroupMsgHook = EasyHook.LocalHook.Create(
EasyHook.LocalHook.GetProcAddress("CQP.dll", "CQ_sendGroupMsg"), new CQ_sendGroupMsg_Delegate(CQ_sendGroupMsg_Hook), null);
            CQ_deleteMsgHook = EasyHook.LocalHook.Create(
EasyHook.LocalHook.GetProcAddress("CQP.dll", "CQ_deleteMsg"), new CQ_deleteMsg_Delegate(CQ_deleteMsg_Hook), null);

            CQ_sendPrivateMsgHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
            CQ_sendGroupMsgHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
            CQ_deleteMsgHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });

            PrivateMsgResponder += PrivateMsg;
            GroupMsgResponder += GroupMsg;
            DeleteMsgResponder += DeleteMsg;
        }

        private static long CQ_sendPrivateMsg_Hook(int ac, long qqid, [MarshalAs(UnmanagedType.LPStr)] [Out] string msg)
        {
            return PrivateMsgAction(ac, qqid, msg);
        }

        private static long CQ_sendGroupMsg_Hook(int ac, long grougid, [MarshalAs(UnmanagedType.LPStr)] [Out] string msg)
        {
            return GroupMsgAction(ac, grougid, msg);
        }

        private static long CQ_deleteMsg_Hook(int ac, long msgid)
        {
            return DeleteMsgAction(ac, msgid);
        }

        public static void Unhook()
        {
            try
            {
                CQ_sendPrivateMsgHook.Dispose();
                CQ_sendGroupMsgHook.Dispose();
                CQ_deleteMsgHook.Dispose();
                EasyHook.LocalHook.Release();
            }
            catch { }
        }


    }
}
