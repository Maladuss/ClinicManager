using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostNumber { get; set; }
        public string PostalCode { get; set; }
        
    }
}
