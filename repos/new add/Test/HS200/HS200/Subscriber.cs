using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HS200
{
    internal class Subscriber
    {

        private static List<string> data = new List<string>();
        public void watchForFile()
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
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);


            watcher.EnableRaisingEvents = true;

            DirectoryInfo d1 = new DirectoryInfo(@"C:\Users\orbithealth\Desktop\programData\Output Worklist\");

        }

        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                string filedirectory = @"C:\Users\orbithealth\Desktop\programData\Output Worklist\";
                List<string> patientResults = new List<string>();

                Database database = new Database();
                data = readResults(e.Name);

                string header = data[0];
                string end = data[data.Count - 1];


                patientResults.Add(header);
                int j = 0;
                foreach (string line in data)
                {
                    string nline = String.Concat(line.Where(c => !Char.IsWhiteSpace(c)));



                    if (line == header || line == end)
                    {
                        continue;
                    }

                    if (nline.StartsWith("P"))
                    {

                        for (int i = data.IndexOf(line); i < data.Count; i++)
                        {

                             
                            string li = String.Concat(data[i].Where(c => !Char.IsWhiteSpace(c)));

                            if (li.StartsWith("P") && j != 0 || li == end)
                            {

                                patientResults.Add(end);
                                string dataToStore = String.Join("##", patientResults.ToArray());
                                database.InsertResult(dataToStore);
                                patientResults.Clear();

                                patientResults.Add(header);
                                patientResults.Add(li);
                                 
                            }
                            else
                            {
                                //50 60
                                if (li != end)
                                {
                                    patientResults.Add(li);
                                    j++;
                                }

                            }
                        }
                    }
                    else
                    {
                        continue;
                    }

                }

                //string dataToStore = String.Join("##", data.ToArray());

                //database.InsertResult(dataToStore);

                Console.WriteLine("data received & file deleted!");
                File.Delete(filedirectory + e.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //Specify what is done when a file is changed.


        }

        public static List<string> readResults(string fname)
        {
            string filename = @"C:\Users\orbithealth\Desktop\programData\Output Worklist\" + fname;
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


    }

 


}


