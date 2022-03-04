using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace HS200
{
    public class OrderSender
    {
        Database db;
        public OrderSender()
        {
             db = new Database();
        }
        public void startSending()
        {
            while (true)
            {
               
                DataTable messages = db.SelectAllMessages();

                if (messages.Rows.Count > 0)
                {
                    DataRow dr = messages.Rows[0];
                    Console.WriteLine("raw data is " + dr["message"].ToString(), ConsoleColor.DarkYellow);

                    string dataToSend = dr["message"].ToString().Trim();

                    string[] astmMessageRecords = SplitMessage(dataToSend);
                    Console.WriteLine("Message parsed successfully", ConsoleColor.DarkYellow);

                    int messageId = int.Parse(dr["id"].ToString());

                    var filename = @"C:\Users\orbithealth\Desktop\programData\Input Worklist\" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".astm";

                    using (FileStream fs = File.Create(filename))
                    {
                        foreach (string am in astmMessageRecords)
                        {

                            byte[] p = new UTF8Encoding(true).GetBytes(am + "\n");

                            fs.Write(p, 0, p.Length);
                        }
                    }

                    bool s = db.DeleteMessage(messageId);
                    Console.WriteLine("Message deleted ->" + s, ConsoleColor.DarkYellow);
                }
            }
        }

        private string[] SplitMessage(string dataToSend)
        {
            return Regex.Split(dataToSend, "##");
        }
    }
}
