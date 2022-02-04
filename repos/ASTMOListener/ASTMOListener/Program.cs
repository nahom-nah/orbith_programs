using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
namespace ASTMOListener
{
    internal class Program
    {
        private static readonly byte[] ArshoIP = { 192, 168, 7, 213 };
        private static readonly byte[] Localhost = { 127, 0, 0, 1 };
        private const int Port = 9000;

        static void Main(string[] args)
        {
            System.Net.IPAddress address = new IPAddress(ArshoIP);
            System.Net.IPEndPoint endPoint = new IPEndPoint(address, Port);

            Subscriber subscriber = new Subscriber(endPoint);
            System.Threading.Thread listnerThread = new Thread(new ThreadStart(subscriber.Listen));
            listnerThread.Start();

            //Publisher publisher = new Publisher(Localhost, Port);
            //Thread publisherThread = new Thread(new ThreadStart(publisher.Send));
            //publisherThread.Start();

        }
    }
}
