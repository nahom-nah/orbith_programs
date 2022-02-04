using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace parsing
{

    public class ASTMField
    {
        public int Index { get; set; }
        public string Value { get; set; }
    }

    public class ASTMRecord
    {
        public List<ASTMField> Fields { get; set; }

        public string Header
        {
            get
            {
                if (Fields.Count > 0)
                    return Fields[0].Value;
                else
                    return string.Empty;
            }
        }
        public ASTMRecord(string input)
        {
            Fields = new List<ASTMField>();
            string[] tokens = Regex.Split(input, "\\|");

            for(int i =0; i < tokens.Length; i++)
            {
                string current = tokens[i].ToString();
                Fields.Add(new ASTMField() { Index = i, Value = current });
            }
        }


     }

    class ASTMmessage
    {
        public List<ASTMRecord> Records { get; set; }
        public ASTMmessage()
        {

        }
    }
}
