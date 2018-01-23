using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public enum PersonType
    {
        Patient = 0,
        Doctor = 1,
        NN = 2
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
