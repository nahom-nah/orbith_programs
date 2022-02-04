using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
 
using NHapi.Base.Parser;
 

namespace HC5D
{
    class Subscriber
    {
        static string fs = char.ConvertFromUtf32(28);
        private static TcpListener server;
        private string cr = char.ConvertFromUtf32(13);
        PipeParser parser;
        public Subscriber(IPAddress address, Int32 port)
        {
            server = new TcpListener(address, port);
            server.Start();
        }

        public void listen()
        {
            parser = new PipeParser();
            
            while (true)
            {
                Console.WriteLine("waiting for connection ...", ConsoleColor.Yellow);
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Connected!!!", ConsoleColor.Green);

                NetworkStream stream = client.GetStream();
                int i;

                byte[] bytes = new byte[1];

                string responsedata = null;

                List<string> msg;
                string res = null;
                
                string full_message = null;

                msg = new List<string>();
                while ((i = stream.Read(bytes, 0, bytes.Length))!= 0)
                {
                    responsedata = Encoding.UTF8.GetString(bytes);
                   
                    if(responsedata == cr)
                    {


                        //s = new string(msg.Where(c => !char.IsControl(c)).ToArray());

                        msg.Add(res);
                        res = null;
                        //var message = parser.Parse(s);
                        
                        
                        continue;
                    }
                    if(responsedata == fs)
                    {

                         full_message = String.Join("##", msg);

                         

                        Database database = new Database();
                        database.InsertResult(full_message);
                    }

                    //msg.Add(responsedata);
                    res += responsedata;

                }

               
                

            }
        }
    }
}
