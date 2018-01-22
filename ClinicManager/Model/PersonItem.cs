using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class PersonItem
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }

        public PersonItem(string name, string lastName, string ssn)
        {
            Name = name;
            LastName = lastName;
            SSN = ssn;
        }
    }
}
