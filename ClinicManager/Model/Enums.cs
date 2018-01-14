using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public enum PersonType
    {
        patient = 0,
        doctor = 1
    }
    public enum PriceItemType
    {
        NFZ = 0,
        STANDARD = 1
    }
    public enum FunctionType
    {
        EKG = 0,
        RTG = 1,
        UKG = 2,
        EEG = 3
    }
}
