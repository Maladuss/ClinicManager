using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Mock
{
    public class MockAddress
    {
        public static Address getAddress()
        {
            return new Address() { City = "Łaziska Górne", Street = "Cieszyńska", PostNumber = "10", PostalCode = "43-170"};
        }
    }
}
