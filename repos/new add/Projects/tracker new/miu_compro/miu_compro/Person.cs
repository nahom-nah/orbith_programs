using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miu_compro
{
    internal class Person
    {
        public string Name { get; set; }
        public DateTime dateOfBirth { get; set; }

        public educationLavel lavel { get; set; }

        public List<Person> children = new List<Person>();

    }
}
