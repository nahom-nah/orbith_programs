using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Topshelf;

namespace HS200
{
    class Program
    {

        private static List<string> outputs = new List<string>();
        private static List<string> createdOFile = new List<string>();
     
        
        
        static void Main(string[] args)
        {







                var exitCode = HostFactory.Run(x =>
                {
                    x.Service<ASTMService>(s =>
                    {
                        s.ConstructUsing(astmService => new ASTMService());
                        s.WhenStarted(astmService => astmService.OnStartUp());
                        s.WhenStopped(astmService => astmService.stop());
                    });


                    x.RunAsLocalSystem();
                    x.SetServiceName("HumaStar_200_Service");
                    x.SetDisplayName("HUMASTAR 200");
                    x.SetDescription("integration service for HumaStar 200 cbc machine");
                });


                int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
                Environment.ExitCode = exitCodeValue;

            









            //listening for a result output file
            //Subscriber subscriber = new Subscriber();

            //Thread listener = new Thread(new ThreadStart(subscriber.watchForFile));

            //listener.Start();




            //sending result to orbit system
            //ResultSender sender = new ResultSender();

            //Thread resultSender = new Thread(new ThreadStart(sender.StartSending));

            //resultSender.Start();




            //post api endpoint for orders
            //LabService service = new LabService();

            //Thread labService = new Thread(new ThreadStart(service.StartService));

            //labService.Start();



            //sending order file to output worklist file(machine)
            //OrderSender orderSender = new OrderSender();

            //Thread osender = new Thread(new ThreadStart(orderSender.startSending));

            //osender.Start();




        }
         
        
       


        
    }
}
