using ClinicManager.Mock;
using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager
{
    public class ClinicSingleton
    {
        private static Clinic instance;

        private ClinicSingleton() { }

        public static Clinic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = MockClinic.getClinic("Medicover");
                }
                return instance;
            }
        }
    }

}
