using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miu_compro
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string note;
             note = "In a village of La Mancha, the name of which I have no desire to call to"
                   + "mind, there lived not long since one of those gentlemen that keep a lance"
                   + "in the lance-rack, an old buckler, a lean hack, and a greyhound for"
                   + "coursing.An olla of rather more beef than mutton, a salad on most"
                   + "nights, scraps on Saturdays, lentils on Fridays, and a pigeon or so extra"
                   + "on Sundays, made away with three - quarters of his income.";




            string[] str = note.Split(note.Select(i => char.IsPunctuation(i)? i : ' ').ToArray());





            foreach(var i in note.Select(i => char.IsPunctuation(i) || char.IsWhiteSpace(i) ? i : ' ').ToArray())
            {
                Console.WriteLine("-> : {0} ",i);
            }


            foreach(string str2 in str)
            {
                Console.WriteLine("-> {0}",str2);
            }







            //string str = "&ns #6lz";

            //char[] ans = new char[] { };

            //foreach(char c in str.ToCharArray())
            //{
                 
            //    if(c=='#' || c=='#'&& ans.Length > 0)
            //    {
            //        char toRemove =new char();
            //        if(ans.Length > 0)
            //        {
            //             toRemove = ans.Last();
            //        }
                    
            //        ans = ans.Where(x => x != toRemove).ToArray();

            //    }
            //    else
            //    {
            //        ans = ans.Concat(new char[] { c }).ToArray();
            //    }
            //}
            
            //Console.WriteLine("-> {0}",new String(ans));
            //string ans = String.Join("", str.Split(' ').Select(x => x != ""? char.ToUpper(x[0]) + x.Substring(1):""));

             

            Console.Read();
             
        }
    }
}

