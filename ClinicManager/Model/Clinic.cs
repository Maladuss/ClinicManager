using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class Clinic
    {
        public string Name { get; set; }
        private List<Person> employees { get; set; }
        private List<Person> patient { get; set; }

        public Clinic(string name)
        {
            Name = name;
            init();
        }
        private void init()
        {
            employees = new List<Person>();
            patient = new List<Person>();
        }
        public bool addEmployee(Person empolyee)
        {
            if(empolyee != null)
            {
                employees.Add(empolyee);

                return true;
            }
            return false;      
        }
        public bool addEmployees(List<Person> empolyees)
        {
            if (empolyees != null)
            {
                this.employees.AddRange(empolyees);

                return true;
            }
            return false;
        }
        public bool addPatient(Person patient)
        {
            if (patient != null)
            {
                this.patient.Add(patient);

                return true;
            }
            return false;
        }
        public bool addPatients(List<Person> patients)
        {
            if (patients != null)
            {
                this.patient.AddRange(patients);

                return true;
            }
            return false;
        }
        public Person getEmployees(string name, string lastName)
        {
           return employees.Where(x => x.Name.CompareTo(name.ToUpper()) == 0 && x.LastName.CompareTo(lastName.ToUpper()) == 0).FirstOrDefault();
        }

        public List<Person> getEmployee(FunctionType functionType)
        {
            return employees.Where(x => x.FunctionTypes.All(y => y.FunctionType == functionType)).ToList();
        }
        public List<Person> getEmployee()
        {
            return employees;
        }
        public Person getPatient(string name, string lastName)
        {
            return patient.Where(x => x.Name.CompareTo(name.ToUpper()) == 0 && x.LastName.CompareTo(lastName.ToUpper()) == 0).FirstOrDefault();
        }

        public List<Person> getPatients()
        {
            return patient;
        }
    }
}
