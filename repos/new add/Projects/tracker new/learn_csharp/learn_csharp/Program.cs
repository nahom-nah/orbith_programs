using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learn_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {


            order(new int[] { 1, 2, 0, 1, 0, 1, 0, 3, 0, 1 });


            Console.WriteLine(toSnake("NahomBalcha"));
           

            Console.Read();
        }

        public static string toSnake(string str)
        {
            string res = new string( str.Select(x => char.IsUpper(x) ? x : '-').ToArray());
            Console.Write("==> {0}",res);
            return "";
        }
        public static int[] order(int[] arr)
        {
            arr = arr.OrderBy(x=> x==0).ToArray();

            foreach(int i in arr)
            {
                Console.WriteLine("-> {0}",i);
            }

            return arr;
        }
        public static int GetUnique(IEnumerable<int> numbers)
        {
            IEnumerable<int> nums = numbers.Distinct<int>();

            foreach(int i in nums){
                if (numbers.Count(j => j == i) == 1)
                {
                    return i;
                }
            }
            return 0;

        }
        public static bool Scramble(string str1, string str2)
        {
            // your code
            char[] arr = str2.ToCharArray();

            foreach(char c in arr)
            {
                
                //Console.WriteLine("count {0} : {1}",c, str2.Count(i => i == c) == str1.Count(i=> i==c));
                if(str1.IndexOf(c) == -1 || str1.Count(i=> i == c) < str2.Count(i=>i==c))
                {
                    return false;
                }
            }

            return true;

        }

        public static int[,] MultiplicationTable(int size)
        {
            int[,] table = new int[size+1,size+1];
            for(int i = 0; i < size; i++)
            {
                for(var j = 0; j < size; j++)
                {
                    table[i, j] = (i+1)*(j+1);
                    
                }
            }
            return table;
        }
    }
}

//3
//1 2 3
//2 4 6
//3 6 9