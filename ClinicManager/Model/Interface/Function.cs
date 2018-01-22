using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public interface IFunction
    {
        FunctionType FunctionType { get; set; }
        Person patient { get; set; }
        string NamePerformer { get; set; }
        DateTime DateTimeStart { get; set; }
        DateTime DateTimeEnd { get; set; }
        string Description { get; set; }
        bool CheckDone();
    }
}
