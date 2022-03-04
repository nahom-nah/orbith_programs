using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace HS200
{
    class ResultSender
    {
        Database db;

        public ResultSender()
        {
            db = new Database();
        }

        public void StartSending()
        {
            while (true)
            {
                
                MessageInput inp = GetMessage();
                
                
                if (inp != null)
                {
                    Console.WriteLine("3");
                    WriteLog("New result from machine arrived!");
                    SendMessage(inp).GetAwaiter();
                }
            }
        }
        private async Task SendMessage(MessageInput message)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:9000";
                var client = new HttpClient();
                
                var response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
                WriteLog("Response from server ->" + result);
            }
            catch (Exception exe)
            {
                WriteLog(exe.Message);
            }
        }

        private void WriteLog(string message, ConsoleColor color = ConsoleColor.DarkBlue)
        {
            Helper.WriteLog(message, "ResultSender", color);
        }


        private MessageInput GetMessage()
        {
            try
            {
                DataTable tbl = db.SelectAllResults();
                
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    string savedMessage = dr["message"].ToString().Trim();
                    string[] splitedMessage = SplitMessage(savedMessage);
                    ASTMMessage aSTMMessage = new ASTMMessage(splitedMessage);


                    db.DeleteResult(int.Parse(dr["id"].ToString().Trim()));
                    return aSTMMessage.GetResultMessageObject();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exe)
            {
                WriteLog("Exception occured while reading and preparing result message " + exe.Message);
                return null;
            }
        }
        private string[] SplitMessage(string dataToSend)
        {
            return Regex.Split(dataToSend, "##");
        }
    }
}
