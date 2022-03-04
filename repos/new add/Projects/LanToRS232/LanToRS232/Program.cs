using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace LanToRS232
{
    internal class Program
    {

        private static readonly byte[] ArshoIP = { 192, 168, 7, 213 };
        private static readonly byte[] Localhost = { 127, 0, 0, 1 };
        private const int Port = 21110;

        static void Main(string[] args)
        {
            System.Net.IPAddress address = new IPAddress(Localhost);
            System.Net.IPEndPoint endPoint = new IPEndPoint(address, Port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(1);
            try
            {
                Socket receiver = socket.Accept();

                while (true)
                {
                    try
                    {
                        receiver.Send(Encoding.UTF8.GetBytes("Hello Nahom"));
                        Console.WriteLine("data successfully sent");

                    }catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
    }
}
 