using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Mock
{
    public class MockClinic
    {
        public static Clinic getClinic(string name)
        {
            Clinic clinic = new Clinic(name);
            clinic.addEmployee(MockPerson.getPerson("Jakub", "Loska", PersonType.doctor));
            clinic.addEmployee(MockPerson.getPerson("Gabriel", "Malada", PersonType.doctor));
            clinic.addEmployee(MockPerson.getPerson("Mateusz", "Muszer", PersonType.doctor));
            clinic.addEmployee(MockPerson.getPerson("Juzek", "Stalin", PersonType.doctor));

            clinic.getEmployees("Jakub", "Loska").addFunctionTypes(MockFunctionItem.getFunctionItem(FunctionType.EKG));
            clinic.getEmployees("Gabriel", "Malada").addFunctionTypes(MockFunctionItem.getFunctionItem(FunctionType.EKG, FunctionType.EEG, FunctionType.RTG, FunctionType.UKG));
            clinic.getEmployees("Mateusz", "Muszer").addFunctionTypes(MockFunctionItem.getFunctionItem(FunctionType.EKG, FunctionType.UKG));
            clinic.getEmployees("Juzek", "Stalin").addFunctionTypes(MockFunctionItem.getFunctionItem(FunctionType.EKG, FunctionType.EEG, FunctionType.UKG));

            clinic.addPatient(MockPerson.getPerson("Tosia", "Wałowicz", PersonType.patient));
            clinic.addPatient(MockPerson.getPerson("Ewa", "Malada", PersonType.patient));
            clinic.addPatient(MockPerson.getPerson("Jadwiga", "Sroka", PersonType.patient));
            clinic.addPatient(MockPerson.getPerson("Krzysztof", "Stalin", PersonType.patient));

            return clinic;
        }
    }
}
