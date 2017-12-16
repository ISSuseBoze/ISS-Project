using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TcpTestClient
{
	public class Client
	{
		public string IP { get; set; }
		public int Port { get; set; }

		private TcpClient tcpClient;

		public Client(string ip, int port) 
		{
			IP = ip;
			Port = port;

			tcpClient = new TcpClient(IP, Port);
		}

		public void sendMessage(string message)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);

			tcpClient.Client.Send(bytes);
		}
	}
}
