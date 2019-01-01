using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.Hook
{
    public class CQHookEvent
    {
        public class CQ_sendMsg_Response
        {
            public int ac { get; set; }
            public int id { get; set; }
            public string msg { get; set; }
        }

    }
}
