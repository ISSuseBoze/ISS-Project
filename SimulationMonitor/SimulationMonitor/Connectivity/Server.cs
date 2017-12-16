using SimulationMonitor.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace SimulationMonitor.Connectivity
{

    public delegate void ServerEventDelegate(object sender, ServerEventArgs e);

    public class Server: IDisposable
    {
        private TcpClient client;
        private TcpListener server;
        public List<string> receivedText { get; private set; }
        public string NewTextData { get; set; }

        public string IP { get; set; }
        public int Port { get; set; }

        public event ServerEventDelegate OnDataReceived;
        public event ServerEventDelegate OnServerStarted;
        public event ServerEventDelegate OnListeningForClient;
        public event ServerEventDelegate OnClientAccepted;
        public event ServerEventDelegate OnServerStopped;

        public Server(string ip, int port)
        {
            receivedText = new List<string>();
            IP = ip;
            Port = port;
        }

        public TcpListener startServer()
        {
            server = new TcpListener(IPAddress.Parse(IP), Port);

            server.Start();
            OnServerStarted.Invoke(this, new ServerEventArgs("Server started."));

            return server;
         
        }

        public async Task acceptClient()
        {
            

            OnListeningForClient.Invoke(this, new ServerEventArgs("Server Accepting Clients."));

            client = await server.AcceptTcpClientAsync();

            OnClientAccepted.Invoke(this, new ServerEventArgs("Server Accepted Client."));
            
        }

        public void receiveSessionData()
        {
            NetworkStream stream = client.GetStream();

            while (true)
            {
                try
                {
                    while (!stream.DataAvailable) ;//wait while nothing is available

                }
                catch (Exception e)
                {
                    break;
                }



                Byte[] bytes = new Byte[client.Available];

                stream.Read(bytes, 0, bytes.Length);

                NewTextData = System.Text.Encoding.UTF8.GetString(bytes);

                receivedText.Add(NewTextData);
                

                OnDataReceived.Invoke(this, new ServerEventArgs("Received: " + NewTextData.ToString()));

            }
        }

        public void sendToClient(string message)
        {
            Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
            client.Client.Send(bytes);
        }

        public void closeConnection()
        {
            client.Close();
            server.Stop();
           
            OnServerStopped.Invoke(this, new ServerEventArgs("Server stopped."));
        }

        public void Dispose()
        {
            closeConnection();
        }
    }
}
