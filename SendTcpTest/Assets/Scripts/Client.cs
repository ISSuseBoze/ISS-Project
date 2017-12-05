using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;


namespace Assets.Scripts {

    public class Client:IDisposable
    {
        private TcpClient client;
        public string IP { get; set; }
        public int Port { get; set; }

        public Client(string ip, int port)
        {
            client = new TcpClient();
            IP = ip;
            Port = port;
            client.Connect(IP, Port);
           
        }

        public void SendMsg(string msg)
        {
            
            client.Client.Send(System.Text.UTF8Encoding.UTF8.GetBytes(msg));
        }

        public void CloseConn()
        {
            client.Close();
        }

        public void Dispose()
        {
            CloseConn();
        }
    }

}