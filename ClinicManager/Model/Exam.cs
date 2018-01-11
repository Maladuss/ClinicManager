using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class EKG: IFunction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FunctionType FunctionType { get; set; }
        public Person patient { get; set; }
        private bool done = false;

        public bool CheckDone()
        {
            return done;
        }
        public void ExamStop()
        {
            done = true;
        }
        public void ExamReset()
        {
            done = false;
        }
    }
}
