using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace sysmex
{
     class Program
    {
        private static readonly byte[] ArshoIP = { 10, 10, 1, 200 };
        private static readonly byte[] Localhost = { 127, 0, 0, 1 };
        private const int Port = 5006;
        private static System.Net.Sockets.Socket sender;
        static void Main(string[] args)
        {
            IPAddress address = new IPAddress(ArshoIP);
            IPEndPoint endPoint = new IPEndPoint(address, Port);
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(endPoint);

            Console.WriteLine("Socket connected to " + sender.RemoteEndPoint.ToString());
        }

    }
}
