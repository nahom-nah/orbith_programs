using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace ASTMOListener
{
    class Subscriber
    {

        static string soh = char.ConvertFromUtf32(1);
        static string stx = char.ConvertFromUtf32(2);
        static string etx = char.ConvertFromUtf32(3);
        static string eot = char.ConvertFromUtf32(4);
        static string enq = char.ConvertFromUtf32(5);
        static string ack = char.ConvertFromUtf32(6);
        static string nack = char.ConvertFromUtf32(21);
        static string etb = char.ConvertFromUtf32(23);
        static string lf = char.ConvertFromUtf32(10);
        static string cr = char.ConvertFromUtf32(13);
        private IPEndPoint endpoint;
        private Socket listener;
        public Subscriber(IPEndPoint endpoint)
        {
            this.endpoint = endpoint;
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(endpoint);
            Console.WriteLine("Listening on port "+ endpoint);
            listener.Listen(1);
        }

        public void SendOrder(Socket sck)
        {
            Console.WriteLine("________________inside send order______________");
            int bytesRec = 0;
            string messageToSend = @"H|\^&||PSWD|Maglumi 2000|||||Lis||P|E1394-97|20100326##P|1##O|1|1234567||^^^TSH##L|1|N";

            string[] astmMessageRecords = SplitMessage(messageToSend);

            sck.Send(Encoding.UTF8.GetBytes(enq));
            Console.WriteLine("enq sent...");

            Thread.Sleep(10);
            sck.Send(Encoding.UTF8.GetBytes(stx));
            Console.WriteLine("stx sent");
            Thread.Sleep(10);
            //main message sending started
            foreach (string am in astmMessageRecords)
            {
                Console.WriteLine("inside ......");
                sck.Send(Encoding.UTF8.GetBytes(am));
                Console.WriteLine(am, ConsoleColor.DarkYellow);
                Thread.Sleep(10);


                

            }

                sck.Send(Encoding.UTF8.GetBytes(etx));
                Console.WriteLine("etx sent", ConsoleColor.DarkYellow);
                Thread.Sleep(10);
                sck.Send(Encoding.UTF8.GetBytes(eot));
                Console.WriteLine("eot sent", ConsoleColor.DarkYellow);
                Thread.Sleep(10);



        }

        private string[] SplitMessage(string dataToSend)
        {
            return Regex.Split(dataToSend, "##");
        }
        public void Listen()
        {

            int byteRec = 0;

            Socket receiver = listener.Accept();
            while (true)
            {

                try
                {
                    string data = null;
                    byte[] bytes = null;
                    SendOrder(receiver);
                    bytes = new byte[1024];
                    byteRec = receiver.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes,0, byteRec);

                    if(data.IndexOf(enq) > -1)
                    {
                        
                         
                        receiver.Send(Encoding.UTF8.GetBytes(ack));
                        Console.WriteLine("<<== ENQ");
                    }

                    if(data.IndexOf(stx) > -1)
                    {
                        receiver.Send(Encoding.UTF8.GetBytes(ack));
                        Console.WriteLine("<<== STX");
                    }

                    if (data.IndexOf("H") > -1 ||
                          data.IndexOf("Q") > -1 ||
                          data.IndexOf("R") > -1 ||
                          data.IndexOf("O") > -1 ||
                          data.IndexOf("L") > -1)
                    {
                        receiver.Send(Encoding.UTF8.GetBytes(ack));
                        Console.WriteLine("...receiving data....");
                        Console.WriteLine("<<== "+ data);
                    }

                    if(data.IndexOf(etx) > -1)
                    {
                        receiver.Send(Encoding.UTF8.GetBytes(ack));
                        Console.WriteLine("<<== ETX");
                    }
                    if(data.IndexOf(eot) > -1)
                    {
                        receiver.Send(Encoding.UTF8.GetBytes(ack));
                        Console.WriteLine("<<== EOT");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
