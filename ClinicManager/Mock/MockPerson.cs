using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Mock
{
    public class MockPerson
    {
        public static Person getPerson(string name, string lastname, PersonType type)
        {
            switch (type)
            {
                case PersonType.doctor:
                    return new Person(name.ToUpper(), lastname.ToUpper(),"123132123", MockAddress.getAddress(), PersonType.doctor);
                case PersonType.patient:
                    return new Person(name.ToUpper(), lastname.ToUpper(),"45343634534", MockAddress.getAddress(), PersonType.patient);

                default: return new Person(name.ToUpper(), lastname.ToUpper(),"14348763423", MockAddress.getAddress(), PersonType.doctor);
            }
        }
    }
}
