using System.Text;
using System;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace parsing
{

    public class ResultData
    {
        public string patientId { get; set; }
        public string specimenId { get; set; }
        public string result { get; set; }

        public ResultData(string pid, string cid, string res)
        {
            this.patientId = pid;
            this.specimenId = cid;
            this.result = res;
        }

    }
    public class Program
    {
        
       
        public static void Main(string[] args)
        {
            
             var resString = "1mtrsl|pis0913-024|pn|pb|ps-|so|si|cis0913-024|rtFSH|rnFSH|tt12:25|td13/01/2022|ql|qn30.94 mUI/ml|y3mUI/ml|qd1|ncvalid|idVIDASPC01|sn|m4haimanot|♥12";

            string[] record = Regex.Split(resString, "\\|");
             string pid = null;
            string cid = null;
            string res = null;

            foreach(string s in record)
            {
                if (s.Contains("pis"))
                { 
                 pid = s.Substring(3);

                
                }else if(s.Contains("cis")){
                    cid = s.Substring(3);
                }
                else if (s.Contains("qn"))
                {
                  string[]  sqn = s.Split(" ");
                    res= sqn[0].Substring(2);
                }
                else
                {
                    continue;
                }
            }

            var resData = new ResultData(pid, cid, res);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(resData);
            Console.WriteLine("json =>"+json);
            
       
        }
       
    }
}