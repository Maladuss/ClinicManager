using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class ExamExtension
    {
        public static IFunction getExam(FunctionType functionType)
        {
            switch (functionType)
            {
                case FunctionType.EKG:
                    {
                        return new EKG() { Name = "EKG", Description = "napierdalanie prądem", FunctionType = FunctionType.EKG };
                    }break;
            }

            return null;
        }
    }
}
