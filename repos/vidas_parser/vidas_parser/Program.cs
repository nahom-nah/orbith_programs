using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace vidas_parser
{

    class Program
    {
         
        private static string sampleId;
        private static string code;
        private static string result;
        static void Main(string[] args)
        {
            string result = "1mtrsl|pis0913-024|pn|pb|ps-|so|si|cis0913-024|rtFSH|rnFSH|tt12:25|td13/01/2022|ql|qn30.94 mUI/ml|y3mUI/ml|qd1|ncvalid|idVIDASPC01|sn|m4haimanot|♥12";
        
            string[] fileds = result.Split('|');
            string original_id;
            foreach(string fd in fileds)
            {
                if (fd.StartsWith("cis"))
                {
                    //patientId = fd.Substring;
                    original_id = fd.Substring(3);
                    string[] split_id = original_id.Split('-');
                   
                    original_id = split_id[0] + DateTime.Now.ToString("MM") +DateTime.Now.ToString("yyyy")+split_id[1];
                    sampleId = original_id;
                }else if (fd.StartsWith("rt"))
                {
                    code = fd.Substring(2);
                }else if (fd.StartsWith("qn"))
                {
                  
                    result = fd.Substring(2);
                }
            }

            
            Console.WriteLine(sampleId);
            Console.WriteLine(code);
            Console.WriteLine(result);
            Console.Read();
            
        }
    }
}
