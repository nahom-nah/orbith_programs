using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ASTMOListener
{
    class Publisher
    {
        string soh = char.ConvertFromUtf32(1);
        string stx = char.ConvertFromUtf32(2);
        string etx = char.ConvertFromUtf32(3);
        string eot = char.ConvertFromUtf32(4);
        string enq = char.ConvertFromUtf32(5);
        string ack = char.ConvertFromUtf32(6);
        string nack = char.ConvertFromUtf32(21);
        string etb = char.ConvertFromUtf32(23);
        string lf = char.ConvertFromUtf32(10);
        string cr = char.ConvertFromUtf32(13);
        private System.Net.Sockets.Socket sender;

        byte[] localhost;
        int port;


        public Publisher(byte[] localhost, int port)
        {
            this.localhost = localhost;
            this.port = port;

        }

        public void Send()
        {
            IPAddress address = new IPAddress(localhost);
            IPEndPoint endPoint = new IPEndPoint(address, port);

            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream ,ProtocolType.Tcp);

            sender.Connect(endPoint);
            Console.WriteLine("Socket connected to " + sender.RemoteEndPoint.ToString());

            int byteRec = 0;
            byte[] bytes = null;
            string data = null;
            while (true)
            {
                try
                {
                    string[] dataToSend = new string[]
                    {
                         @"H|\^&||PSWD|Maglumi 2000|||||Lis||P|E1394-97|20100323",
                         "Q|1|^1234567||ALL||||||||O",
                         "L|1|N"
                    };


                    sender.Send(Encoding.UTF8.GetBytes(enq));
                    Console.WriteLine("Enq sent...");
                    Thread.Sleep(1000);

                   


                    sender.Send(Encoding.UTF8.GetBytes(stx));
                    Console.WriteLine("Stx sent...");
                    Thread.Sleep(1000);

                     

                    foreach(string s in dataToSend)
                    {
                        byte[] msg = Encoding.UTF8.GetBytes(s);
                        sender.Send(msg);
                        Console.WriteLine("sending message....");
                        Thread.Sleep(1000);
 

                    }

                    sender.Send(Encoding.UTF8.GetBytes(etx));
                    Console.WriteLine("etx sent....");
                    Thread.Sleep(1000);
                     

                    sender.Send(Encoding.UTF8.GetBytes(eot));

                    Console.WriteLine("eot sent....");
                    Thread.Sleep(1000);



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
