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
                    return new Person(name.ToUpper(), lastname.ToUpper(), generateSNN(), MockAddress.getAddress(), PersonType.doctor);

                case PersonType.patient:
                    return new Person(name.ToUpper(), lastname.ToUpper(), generateSNN(), MockAddress.getAddress(), PersonType.patient);

                default: return new Person(name.ToUpper(), lastname.ToUpper(), generateSNN(), MockAddress.getAddress(), PersonType.doctor);
            }
        }
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        private static string generateSNN()
        {
            string tmp = ((GetRandomNumber(1,1000)) * 123).ToString();
            return tmp;
        }
    }
}
