using System;
using System.Text;
namespace checksum {
   
    public class Program
    {

        public static void Main(string[] args)
            {


            string sum = "0011";
            byte[] res = null;
            string truncated = null;
            var binaryString = ToBinary(ConvertToByteArray("1mtmpr|pi0913-25|si|ci0913-25|rtTSH|qd1|", Encoding.ASCII));
            
            var arr = binaryString.Split(' ');

            foreach (var item in arr)
            {  
               sum = AddBinary(sum, item);
            }

           
            for (int i = sum.Length-1; i>=0; i--)
            {
 
                truncated = sum.ElementAt(i)+truncated;
                if (truncated.Length == 8)
                {
                    break;
                }
                

            }
            
            var hex = BinaryStringToHexString(truncated);
            Console.WriteLine("the ascii representaion of the binary = "+hex);

          
             
        }
        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }
        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public static string AddBinary(string a, string b)
        {
            string result = "";

            int s = 0;

            int i = a.Length - 1, j = b.Length - 1;
            while (i >= 0 || j >= 0 || s == 1)
            {
                s += ((i >= 0) ? a[i] - '0' : 0);
                s += ((j >= 0) ? b[j] - '0' : 0);
                result = (char)(s % 2 + '0') + result;

                s /= 2;
                i--; j--;
            }
            return result;
        }

        public static string BinaryStringToHexString(string binary)
        {
            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

             

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }


    }


    

}
