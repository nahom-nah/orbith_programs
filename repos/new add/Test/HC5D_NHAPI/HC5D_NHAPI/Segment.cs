using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC5D_NHAPI
{
    internal class Segment
    {
        private Dictionary<int, String> fields;
        public Segment()
        {
            fields = new Dictionary<int, string>(100);
        }

        public string Name
        {
            get
            {
                if (!fields.ContainsKey(0))
                {
                    return String.Empty;
                }
                return fields[0];
            }
        }
    }
}
