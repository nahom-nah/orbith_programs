using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS200
{
     class PatientOrder
    {
        public string methodName;
        public string priority = "False";
        public string nature = "Serum";

        public PatientOrder(string methodName, string priority, string nature)
        {
            this.methodName = methodName;
        }

    }
}
