using ClinicManager.Exam;
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
                        return new EKG() { NamePerformer = "EKG", Description = "Elektrokardiografia", FunctionType = FunctionType.EKG };
                    }
                case FunctionType.EEG:
                    {
                        return new EEG() { NamePerformer = "EEG", Description = "Elektroencefalografia", FunctionType = FunctionType.EEG };
                    }
                case FunctionType.RTG:
                    {
                        return new RTG() { NamePerformer = "RTG", Description = "Zdjęcie rentgenowskie", FunctionType = FunctionType.RTG };
                    }
                case FunctionType.UKG:
                    {
                        return new UKG() { NamePerformer = "UKG", Description = "Echokardiografia", FunctionType = FunctionType.UKG };
                    }
            }

            return null;
        }
    }
}
