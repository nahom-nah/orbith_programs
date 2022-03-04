using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace HS200
{
   
    [ServiceContract]
    public interface ILabService
    {
        [OperationContract]
        [WebGet]
        string SayHello();


        [OperationContract]
        [WebInvoke(
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        ResFormat CreateNewOrder(MessageInput messageInput);

    }

    public class LabService : ILabService
    {
        private void WriteLog(string message)
        {
            Helper.WriteLog(message, "LabServiceHTTP");
        }
        public string SayHello()
        {
            return string.Format("Hello world from get");
        }
        public ResFormat CreateNewOrder(MessageInput messageInput)
        {
            Database db = new Database();
            WriteLog("New message from EMR or LIS arrived!");
            try
            {
                if (messageInput.sampleCode == null || messageInput.sampleCode.Trim() == string.Empty)
                {
                    return new ResFormat() { Message = "Order id cannot be null or empty!", OK = false };
                }
                if (messageInput.Tests == null || messageInput.Tests.Count == 0)
                {
                    return new ResFormat() { Message = "Test cannot be null or empty!", OK = false };
                }
                foreach (var item in messageInput.Tests)
                {
                    if (item.code == null || item.code.Trim() == string.Empty)
                    {
                        return new ResFormat() { Message = "In valid value in Tests (Tests cannot be null or empty)!", OK = false };
                    }
                }
                if (messageInput != null)
                {
                    string astmMessage = GenerateASTMString(messageInput);
                    return db.InsertMessage(astmMessage);
                }
                else
                {
                    return new ResFormat() { Message = "Message content cannot be null!", OK = false };
                }
            }
            catch (Exception exe)
            {
                return new ResFormat() { Message = "Something went wrong, failed to insert message => " + exe.Message, OK = false };
            }
            finally
            {
                db.CloseConnection();
            }

        }

        public void StartService()
        {
            WebServiceHost host = new WebServiceHost(typeof(LabService), new Uri("http://localhost:8000/"));
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(ILabService), new WebHttpBinding(), "");
            ServiceDebugBehavior sdb = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.HttpHelpPageEnabled = false;

            host.Open();
            WriteLog("Lab Http Service is running at port 8000");
        }
        private string GenerateASTMString(MessageInput input)
        {
            string[] fullname = input.customerName.Split(' ');
            try
            {

                string headerRecord = @"H|\^&|||HSX00^V1.0|||||Host||P|1|20110117";
                string patientRecord = "P|1|" + input.customerId + "|" + input.customerCode + "||" + fullname[0] + "^" + fullname[1];
                string orderRecordeach="";
                string orderRecordSegment = "^^^" + input.Tests[0].code;
                List<string> orderRecord = new List<string>();
                int i = 0;
                if (input.Tests.Count > 1)
                {
                    foreach (TestInput test in input.Tests)
                    {
                        if (i >= 0)
                        {
                            orderRecordeach = "O|1|" + input.sampleCode + "||"+test.code;
                            string orderRecordSuffix = "|False||||||||||Serum|||||||||||||||";

                            orderRecord.Add(orderRecordeach + orderRecordSuffix);
                        }
                        i++;
                    }
                }
               
                 
                string endRecord = "L|1|F";

                StringBuilder orderMessage = new StringBuilder();
                orderMessage.Append(headerRecord + "##");
                orderMessage.Append(patientRecord + "##");
                foreach(string order in orderRecord)
                {
                    orderMessage.Append(order + "##");
                }
                
                orderMessage.Append(endRecord);

                return orderMessage.ToString();
                //return @"H|\^&|||Host|||||||P|1|20010226080000##P|1|PID001|RID001##O|1|SID001^N^01^5||^^^f1^sIgE^1\^^^f2^sIgE^1||20010226090000|||N||1||||||||||||O##L|1|F";
            }
            catch (Exception exe)
            {
                WriteLog("Error while generating ASTM string _" + exe.Message);
                return null;
            }
        }

    }
}


