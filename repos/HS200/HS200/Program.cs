using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace HS200
{
    class Program
    {

        private static List<string> outputs = new List<string>();
        private static List<string> createdOFile = new List<string>();
        private static List<string> data = new List<string>();

        static void Main(string[] args)
        {

            watchForFile();
            //List<PatientSample> patientArr = new List<PatientSample>();

            //PatientSample patient = new PatientSample("sample-#1");
            //patient.createOrder("UREA");
            //patient.createOrder("CRE");

            //PatientSample patient2 = new PatientSample("sample-#2");
            //patient2.createOrder("GPT");
            //patient2.createOrder("BD-2");

            //patientArr.Add(patient);
            //patientArr.Add(patient2);

            //Console.WriteLine(patientArr.Count);
            //createInputWorklist(patientArr);

            //string filename = @"C:\Users\orbithealth\Desktop\programData\Output Worklist\test";

            //Console.Read();


            //List<string> output = new List<string>();

            // output = readResults();

            // string result_msg = string.Join("##", output.ToArray());

            // string[] res = Regex.Split(result_msg,"##");

            // ASTMMessage astmMessage = new ASTMMessage(res);

            // Console.WriteLine(astmMessage.Records[3].Fields[0].Value);

            //int i = 0;
            //string astmHeader = "Hello World";
            //byte[] header = new UTF8Encoding(true).GetBytes(astmHeader);
            //while (i < 1)
            //{
            //    filename = filename + i + ".txt";
            //    using (FileStream fs = File.Create(filename))
            //    {
            //        fs.Write(header, 0, header.Length);

            //    }
            //    filename = @"C:\Users\orbithealth\Desktop\programData\Output Worklist\test"; 
            //    i++;
            //}



            

                Console.Read();


        }

        static void watchForFile()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"C:\Users\orbithealth\Desktop\programData\Output Worklist\";

            watcher.NotifyFilter =  NotifyFilters.Attributes |
                                    NotifyFilters.CreationTime |
                                    NotifyFilters.DirectoryName |
                                    NotifyFilters.FileName |
                                    NotifyFilters.LastAccess |
                                    NotifyFilters.LastWrite |
                                    NotifyFilters.Security |
                                    NotifyFilters.Size;

            watcher.Filter = "*.*";
            //watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
           

            watcher.EnableRaisingEvents = true;
            
            DirectoryInfo d1 = new DirectoryInfo(@"C:\Users\orbithealth\Desktop\programData\Output Worklist\");
            
        }

        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            //Specify what is done when a file is changed.

           data = readResults(e.Name);
            foreach (string s in data)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine("{0}, with path {1} has been {2}", e.Name, e.FullPath, e.ChangeType);
        }
        static List<string> readResults(string fname)
        {
            string filename = @"C:\Users\orbithealth\Desktop\programData\Output Worklist\"+fname;
            List<string> msg = new List<string>();
            using (StreamReader sr = File.OpenText(filename))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    msg.Add(s);
                }
            }
            return msg;

        }

        static void messageParser()
        {

        } 

        static public void createInputWorklist(List<PatientSample> patient)
        {
            var filename = @"C:\Users\orbithealth\Desktop\programData\Input Worklist\"+ DateTime.Now.ToString("yyyyMMddTHHmmss") + ".astm";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            Dictionary<int, string> record;
            record = new Dictionary<int, string>(100);
            string astmHeader = @"H|\^&|||HSX00^V1.0|||||Host||P|1|20110117" + "\n";
            string astmEnd = @"L||N" + "\n";
            int i = 0;
            int sequencep = 1;
            int sequenceo = 1;
            record.Add(i, astmHeader);
            using (FileStream fs = File.Create(filename))
            {

                outputs.Add(filename);
                byte[] header = new UTF8Encoding(true).GetBytes(astmHeader);
                byte[] end = new UTF8Encoding(true).GetBytes(astmEnd);
                fs.Write(header, 0, header.Length);

                foreach (PatientSample sample in patient)
                {
                    byte[] p = new UTF8Encoding(true).GetBytes(createPatientAstm(sample.id, sequencep) + "\n");

                    fs.Write(p, 0, p.Length);
                    foreach (PatientOrder order in sample.orders)
                    {
                        var o = new UTF8Encoding(true).GetBytes(createOrderAstm(order, sequenceo) + "\n");
                        fs.Write(o, 0, o.Length);
                        sequenceo++;
                    }
                    sequenceo = 1;
                    sequencep++;
                }
                fs.Write(end, 0, end.Length);

            }
            record.Add(1, astmEnd);


        }

        static public string createPatientAstm(string pid, int sequence)
        {
            Dictionary<int, string> fields;

            fields = new Dictionary<int, string>(34);

            fields.Add(0, "P");
            fields.Add(1, sequence.ToString());
            fields.Add(3, pid);
            fields.Add(5, "FIDO");
            fields.Add(6, "SANFRATELLO DOMENICO");
            fields.Add(7, "20050201");
            fields.Add(8, "M");
            fields.Add(33, "");

            int max = 0;
            foreach (var field in fields)
            {
                if (max < field.Key)
                {
                    max = field.Key;
                }
            }

            StringBuilder temp = new StringBuilder();

            for (int index = 0; index <= max; index++)
            {
                if (fields.ContainsKey(index))
                {
                    temp.Append(fields[index]);
                }

                if (index != max) temp.Append("|");
            }
            return temp.ToString();

        }

        static public string createOrderAstm(PatientOrder order, int sequence)
        {

            Dictionary<int, string> fields;

            fields = new Dictionary<int, string>(34);

            fields.Add(0, "O");
            fields.Add(1, sequence.ToString());
            fields.Add(4, order.methodName);
            fields.Add(5, order.priority);
            fields.Add(15, order.nature);
            fields.Add(30, "");


            int max = 0;
            foreach (var field in fields)
            {
                if (max < field.Key)
                {
                    max = field.Key;
                }
            }

            StringBuilder temp = new StringBuilder();

            for (int index = 0; index <= max; index++)
            {
                if (fields.ContainsKey(index))
                {
                    temp.Append(fields[index]);
                }

                if (index != max) temp.Append("|");
            }
            return temp.ToString();
        }
    }
}
