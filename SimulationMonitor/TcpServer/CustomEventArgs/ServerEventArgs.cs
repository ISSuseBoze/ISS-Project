using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer.CustomEventArgs
{
   
    public class ServerEventArgs: EventArgs
    {
        public string Message { get; set; }

        public ServerEventArgs(string message)
        {
            Message = message;
        }
    }
}
