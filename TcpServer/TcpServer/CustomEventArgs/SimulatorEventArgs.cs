using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer.CustomEventArgs
{
    public class SimulatorEventArgs: EventArgs
    {
        public string Message { get; set; }

        public SimulatorEventArgs(string message)
        {
            Message = message;
        }
    }
}
