using ClinicManager.Mock;
using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Service
{
    public class ServiceData
    {
        public Clinic getClinicWithData(string name)
        {
            return MockClinic.getClinic(name);
        }
    }
}
