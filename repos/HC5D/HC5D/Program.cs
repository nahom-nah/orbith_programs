using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HC5D
{
    public class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {

                Int32 port = 7777;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                //Subscriber subscriber = new Subscriber(localAddr, port);

                //Thread listenerThread = new Thread(subscriber.listen);


                //listenerThread.Start();


                ResultSender sender = new ResultSender();

                Thread resultSender = new Thread(new ThreadStart(sender.startSending));

                resultSender.Start();


                Console.Read();


            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
             
        }
    }
}