using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace HC5D
{
    public class MessageInput
    {
        public string OrderId { get; set; }
        public List<TestInput> Tests { get; set; }

    }
    public class TestInput
    {
        public string code { get; set; }
        public string result { get; set; }
    }
    class ResultSender
    {
        Database database = null;

        public ResultSender()
        {
            database = new Database();
        }

        public void startSending()
        {
            
            while (true)
            {
                
                MessageInput input = GetMessage();

                if (input != null)
                {
                    Console.WriteLine("New result from machine arrived!");
                    SendMessage(input).GetAwaiter();
                }

            }
        }
        

        private async Task SendMessage(MessageInput input)
        {

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:3000";
            var client = new HttpClient();

            Console.WriteLine(json);

        }
        public MessageInput getResultObject(Hl7Message msg)
        {
            try
            {


                MessageInput input = new MessageInput();

                input.OrderId = msg.segments[3].components[3].value.ToString().Trim();

                List<TestInput> tests = new List<TestInput>();
                int i = 0;
                foreach(Segment seg in msg.segments)
                {
                    string code = null;
                    if(i>9 && i < 39){
                        code =   msg.segments[i].components[3].value.Split('^')[1];
                       
                        tests.Add(new TestInput() { code = code, result=msg.segments[i].components[5].value });

                    }
                    


                        i++;
                }
                input.Tests = tests;
                return input;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public MessageInput GetMessage()
        {
           
            try
            {
                DataTable tbl = database.SelectAllResults();
                if(tbl.Rows.Count > 0)
                {
                    DataRow drow = tbl.Rows[0];
                    string savedMessage = drow["message"].ToString().Trim();

                    string[] splitedMessage = Regex.Split(savedMessage,"##");

                    Hl7Message msg = new Hl7Message(splitedMessage);

                    database.DeleteResult(int.Parse(drow["id"].ToString().Trim()));




                    return getResultObject(msg);
                }
                else
                {
                    return null;
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new MessageInput();
            }
        }
    }
}
