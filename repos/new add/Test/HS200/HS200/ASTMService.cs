using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Net;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace HS200
{
    class ASTMService : ServiceBase
    {
        TcpListener server = null;
        private static readonly byte[] ArshoIP = { 192, 168, 7, 213 };
        private static readonly byte[] Localhost = { 127, 0, 0, 1 };
        private const int Port = 9999;

        Subscriber subscriber;
        ResultSender sender;
        LabService service;
        OrderSender orderSender;

        bool doFlag;

        //Thread resultSender;
        //Thread listnerThread;

        public ASTMService()
        {


            try
            {
                 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void OnStartUp()
        {

            doFlag = true;

            System.Net.IPAddress address = new IPAddress(Localhost);
            System.Net.IPEndPoint endPoint = new IPEndPoint(address, Port);

            subscriber = new Subscriber();

            sender = new ResultSender();

            service = new LabService();

            orderSender = new OrderSender();

            while (doFlag == true)
            {


                Parallel.Invoke(() => subscriber.watchForFile(), () => sender.StartSending(),()=> service.StartService(),()=> orderSender.startSending());

            }
 
        }
        public void stop()
        {
            doFlag = false;

         

        }
    }
}
