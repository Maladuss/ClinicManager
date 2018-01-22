using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Exam
{
    public class EKG : IFunction
    {
        public string NamePerformer { get; set; }
        public string Description { get; set; }
        public FunctionType FunctionType { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
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
