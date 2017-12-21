using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Assets.Communication
{
    public class TCPConnection : MonoBehaviour
    {
        //Simulation Monitor IP and port
        public string conHost = "127.0.0.1";
        public int conPort = 1111;

        //connection status
        public bool socketReady = false;

        TcpClient mySocket;
        NetworkStream theStream;
        StreamWriter theWriter;
        StreamReader theReader;

        //try to initiate connection
        public void setupSocket()
        {
            try
            {
                mySocket = new TcpClient(conHost, conPort);
                theStream = mySocket.GetStream();
                theWriter = new StreamWriter(theStream);
                theReader = new StreamReader(theStream);
                socketReady = true;
            }
            catch (Exception e)
            {
                Debug.Log("Socket error:" + e);
            }
        }

        //send message to server
        public void writeSocket(string message)
        {
            if (!socketReady)
                return;
            String tmpString = message;
            theWriter.Write(tmpString);
            theWriter.Flush();
        }

        //read message from server
        public string readSocket()
        {
            String result = "";
            if (socketReady && theStream.DataAvailable)
            {
                Byte[] inStream = new Byte[mySocket.SendBufferSize];
                theStream.Read(inStream, 0, inStream.Length);
                result += System.Text.Encoding.UTF8.GetString(inStream);
            }
            return result;
        }

        //disconnect from the socket
        public void closeSocket()
        {
            if (!socketReady)
                return;
            theWriter.Close();
            theReader.Close();
            mySocket.Close();
            socketReady = false;
        }

        //keep connection alive, reconnect if connection lost
        public void maintainConnection()
        {
            if (!theStream.CanRead)
            {
                setupSocket();
            }
        }


    }
}
