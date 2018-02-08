using ClinicManager.DataBase;
using ClinicManager.Mock;
using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicManager.Service
{
    public class ServiceData
    {
        private bool MockOn { get; } = false;
        private DataBaseService dataBase { get; set; }

        public ServiceData()
        {
            dataBase = new DataBaseService();
            dataBase.Connection();
            CheckOrAddFuntionType();
            //Address address = new Address() { City = "Bielko-Bialau", Street = "Łaczna", PostalCode = "4635-11", PostNumber = "56" };
            ////address.AddressId = 5;
            //Person person = new Person("Staszek", "Hills", "2434553", address, PersonType.doctor);
            //person.Address = address;
            ////person.PersonId = 2;
            //AddPerson(person);

           // List<Person> persons = getPersons();
          // int id =  AddAddress(new Address() { City = "Zakopane", Street = "krótka", PostalCode = "34-3r", PostNumber = "3r" });

        }
        public void CheckOrAddFuntionType()
        {
            if (!dataBase.CheckExistsPersonType("Doctor"))
            {
                dataBase.AddFuntionType("Doctor");
            }
            if (!dataBase.CheckExistsPersonType("Patient"))
            {
                dataBase.AddFuntionType("Patient");
            }
        }
        public Clinic getClinicWithData(string name)
        {
            if (MockOn)
            {
                return MockClinic.getClinic(name);
            }
            else
            {
                Clinic clinic = new Clinic("Medicover");                
                clinic.addPatients(getPersons(PersonType.Patient));
                setPersons(clinic);
                return clinic;
            }
        }
        //for DOCTOR!!!
        private void setPersons(Clinic clinic)
        {
            List<Person> Empols = getPersons(PersonType.Doctor);
            
            foreach(var em in Empols)
            {
                List<CalendarItem> items = dataBase.GetCalendarDays(em.PersonId);
                foreach(var ci in items)
                {
                    if(clinic.getPatients().Exists(x => x.PersonId == ci.PersonId))
                    {
                        Person patient = clinic.getPatients().Where(x => x.PersonId == ci.PersonId).FirstOrDefault();
                        em.addCalendarItem(patient, ci.FunctionType, ci.DateTimeStart);
                    }
                    else
                    {
                        Person patient = dataBase.GetPerson(ci.PersonId);
                        if(patient != null)
                            em.addCalendarItem(patient, ci.FunctionType, ci.DateTimeStart);
                    }
                }

            }
            clinic.addEmployees(Empols);
        }

        public int AddAddress(Address address)
        {
            return dataBase.AddAddress(address);
        }
        public void UpdateAddress(Address address)
        {
            dataBase.UpdateAddress(address);
        }
        public void AddPerson(Person person)
        {
            dataBase.AddPerson(person);
        }
        public void UpdatePerson(Person person)
        {
            dataBase.UpdatePerson(person);
        }
        public void DeletePerson(Person person)
        {
            dataBase.DeletePerson(person);
        }
        public Person getPerson(int id)
        {
           return dataBase.GetPerson(id);
        }
        public List<Person> getPersons(PersonType type)
        {
            List<Person> persons = dataBase.GetPersons(type);

            if(type == PersonType.Doctor)
            {
                foreach (var ob in persons)
                {
                    ob.addFunctionTypes(dataBase.GetFunctions(ob.PersonId));
                }
            }
            return persons;
        }
        public bool AddFunction(FunctionItem function, int PersonId)
        {
            int FunctionId = dataBase.AddFunction(function, PersonId);
            if (FunctionId == -1) return false;
            return true;
        }
        public List<FunctionItem> GetFunctions(int Personid)
        {
            return dataBase.GetFunctions(Personid);
        }
        public void DeleteFunction(int functionId)
        {
            dataBase.DeleteFunction(functionId);
        }
        public void AddCalendarDay(IFunction function, int idPerson)
        {
            dataBase.AddCalendarDay(function, idPerson);
        }
    }
}
