using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS200
{
     class PatientSample
    {
        public string fname = null;
        public string lname = null;
        public DateTime birthdate;
        public string type = null;
        public string id  = null;

        public List<PatientOrder> orders;
        
        public PatientSample(string id)
        {
            this.id = id;
            orders = new List<PatientOrder>();
        }

        public void createOrder(string methodName, string priority="False", string nature = "Serum")
        {
            PatientOrder order = new PatientOrder(methodName, priority, nature);
            orders.Add(order);
        }
    }
}
