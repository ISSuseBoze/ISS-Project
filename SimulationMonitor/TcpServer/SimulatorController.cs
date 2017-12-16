using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpServer.CustomEventArgs;

namespace TcpServer
{
    public class SimulatorController: IDisposable
    {
        private Server server { get; set; }

        //most recent is on top
        //public List<string> messages;
        public Queue<string> messages;

        //delegate and events that are passed on
        public delegate void ServerEventDelegate(object sender, SimulatorEventArgs e);
        public event ServerEventDelegate OnDataReceived;
        public event ServerEventDelegate OnServerStarted;
        public event ServerEventDelegate OnListeningForSimulator;
        public event ServerEventDelegate OnSimulatorAccepted;
        public event ServerEventDelegate OnServerStopped;

        public SimulatorController(string hostIP, int port)
        {
            messages = new Queue<string>();

            //set server
            server = new Server(hostIP, port);
            //set server events
            server.OnServerStarted += Server_OnServerStarted;
            server.OnServerStopped += Server_OnServerStopped;
            server.OnListeningForClient += Server_OnListeningForSimulator;
            server.OnClientAccepted += Server_OnSimulatorAccepted;
            server.OnDataReceived += Server_OnDataReceived;


        }

        //should only receive END_TASK<space><CSV>
        private void Server_OnDataReceived(object sender, ServerEventArgs e)
        {
            string message = e.Message;
            //try catch because of parsing
            try
            {
                if (message.Split(' ')[0] == "END_TASK")
                {
                    //all is OK; get the data; place it as most recent
                    messages.Enqueue(message.Split(' ')[1]);
                    

                    //in the end notify anybody who might be above
                    OnDataReceived.Invoke(this, new SimulatorEventArgs(message));
                }
                else
                {
                    throw new Exception("Message received from client simulator did not parse well");
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        //send event upward
        private void Server_OnListeningForSimulator(object sender, ServerEventArgs e)
        {
            OnListeningForSimulator.Invoke(this, new SimulatorEventArgs(e.Message));
        }

        //send event upward
        private void Server_OnServerStopped(object sender, ServerEventArgs e)
        {
            OnServerStopped.Invoke(this, new SimulatorEventArgs(e.Message));
        }

        //send event upward
        private void Server_OnSimulatorAccepted(object sender, ServerEventArgs e)
        {
            OnSimulatorAccepted.Invoke(this, new SimulatorEventArgs(e.Message));
        }

        //send event upward
        private void Server_OnServerStarted(object sender, ServerEventArgs e)
        {
            OnServerStarted.Invoke(this, new SimulatorEventArgs(e.Message));
        }

        public void send_START()
        {
            server.sendToClient("START");
        }

        public void send_HALT()
        {
            server.sendToClient("HALT");
        }

        //should only send <NEW_TASK><space><JSON>
        public void send_TASK(string taskJSON)
        {
            server.sendToClient("NEW_TASK " + taskJSON);
        }

        //give least recent message
        public string getMessage()
        {
            return messages.Dequeue();
        }

        public void Dispose()
        {
            if(server != null)
            {
                server.Dispose();
            }
        }
    }
}
