using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HS200Simulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string msg = @"H|\^&|||HSX00^V1.0|||||Host||P|1|20081029"+"\n"
                              + "P|1||0001||||20080000|Undefined|||||||||||||||||||||||||"+"\n"
                              + "C|1|||" + "\n"
                              + "O|1|||ALP|False||||||||||Serum|||||||||||||||" +"\n"
                              + "R|1|ALP||||||1234||||20081029|" + "\n"
                              + "L||N";
            string filename = @"C:\Users\orbithealth\Desktop\programData\Output Worklist\astmSample.txt";
            byte[] msgToSend = new UTF8Encoding(true).GetBytes(msg);
            using (FileStream fs = File.Create(filename))
            {
                fs.Write(msgToSend,0, msgToSend.Length);
            }
        }
    }
}
