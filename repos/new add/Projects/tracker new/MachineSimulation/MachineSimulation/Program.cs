using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace MachineSimulation
{
    public class Publisher
    {
        public string soh = char.ConvertFromUtf32(1);
        public string stx = char.ConvertFromUtf32(2);
        public string etx = char.ConvertFromUtf32(3);
        public string eot = char.ConvertFromUtf32(4);
        public string enq = char.ConvertFromUtf32(5);


        public string ack = char.ConvertFromUtf32(6);
        public string nack = char.ConvertFromUtf32(21);
        public string etb = char.ConvertFromUtf32(23);
        public string lf = char.ConvertFromUtf32(10);
        public string cr = char.ConvertFromUtf32(13);

        public readonly IPAddress address ;
        public readonly int port  ;
        public Socket sender;

        public Publisher(IPAddress address, int port)
        {
            this.address = address;
            this.port = port;
        }

        public void Send()
        {
            IPEndPoint endpoint = new IPEndPoint(address, port);
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(endpoint);

            Console.WriteLine("Socket connected to " + sender.RemoteEndPoint.ToString());

             
            while (true)
            {
                try
                {
                    // Connect to Remote EndPoint  
                    string[] resultMessage = new string[] {
                        @"H|\^&||| ImmunoCAP Data Manager^1.00^1.00|||||||P|1|20010226080000",
                        "P|1|PID001|RID001",
                        "O|1|SID001^N^01^5||^^^f1^sIgE^1|||20010226090000||||N||1||||||||||||O",
                        "R|1|^^^f1^sIgE^1|17.500^2^Positive^0/1^1.300|ml/g||||F||||20010226100000|I000001",
                        "L|1|F",
                    };

                    string[] magluminResult = new string[] {
                        @"H|\^&||PSWD|Maglumi User|||||Lis||P|E1394-97|20211019",
                        "P|1",
                        "O|1|BCHE_435C||^^^TSH",
                        "R|1|^^^TSH|8.174|uIU/mL|0.3 to 4.5|H||||||20211015173356",
                        "L|1|N"
                    };

                    string[] maglumiQuery = new string[]
                    {
                         @"H|\^&||PSWD|Maglumi 2000|||||Lis||P|E1394-97|20100323"
                         ,"Q|1|^1234567||ALL||||||||O"
                         ,"L|1|N"
                    };


                    string[] mainMessage = new string[] {
                        @"H|\^&||PSWD|Maglumi 1000|||||Lis||P|E1394-97|20100319",
                        "P|1",
                        "O|1|4||^^^ACTH|R",
                        "O|2|4||^^^AFP|R",
                        "O|3|4||^^^ALD|R",
                        "O|4|4||^^^B-HCG|R",
                        "O|5|4||^^^B2-MG|R",
                        "O|6|4||^^^BGP|R",
                        "O|7|4||^^^BGW|R",
                        "O|8|4||^^^C IV|R",
                        "O|9|4||^^^CA125|R",
                        "O|10|4||^^^CA153|R",
                        "O|11|4||^^^CA199|R",
                        "O|12|4||^^^CA242|R",
                        "O|13|4||^^^CA50|R",
                        "O|14|4||^^^CA724|R",
                        "O|15|4||^^^CAFP|R",
                        "L|1|N"
                };
                    // Encode the data string into a byte array.    


                    //send enq
                    sender.Send(Encoding.UTF8.GetBytes(enq));
                    Console.WriteLine("enqury sent");
                    Thread.Sleep(1000);
                    //GetResponse(sender);
                    //send stx
                    sender.Send(Encoding.UTF8.GetBytes(stx));
                    Console.WriteLine("stx sent");
                    Thread.Sleep(1000);
                    //GetResponse(sender);
                    //send main message

                    foreach (string msg in maglumiQuery)
                    {
                        byte[] message = Encoding.UTF8.GetBytes(msg);
                        sender.Send(message);
                        Thread.Sleep(100);
                        //GetResponse(sender);

                    }

                    Thread.Sleep(1000);
                    //send stx
                    sender.Send(Encoding.UTF8.GetBytes(etx));
                    Console.WriteLine("etx sent");
                    Thread.Sleep(1000);
                    //GetResponse(sender);


                    //send stx
                    sender.Send(Encoding.UTF8.GetBytes(eot));
                    Console.WriteLine("eot sent");
                    Thread.Sleep(1000);
                    //GetResponse(sender);







                    // Release the socket.    
                    //sender.Shutdown(SocketShutdown.Both);
                    //sender.Close();
                    //Console.ReadKey();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException " + ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : " + se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : " + e.ToString());
                }



            }
        }

    }
    internal class Program
    {
      
        static void Main(string[] args)
        {

            Publisher publisher = new Publisher(IPAddress.Loopback, 7777);

            Thread publishThread = new Thread(new ThreadStart(publisher.Send));

            publishThread.Start();
            
        }
    }
}
