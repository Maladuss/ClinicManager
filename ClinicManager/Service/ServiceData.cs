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

                //wymuszenie mock
                List<Person> Empols = getPersons(PersonType.Doctor);
                foreach(var ob in Empols)
                {
                    ob.addFunctionTypes(MockFunctionItem.getRandomFunctionItem());
                }

                clinic.addEmployees(Empols);
                clinic.addPatients(getPersons(PersonType.Patient));
                return clinic;
            }
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
            return dataBase.GetPersons(type);
        }
    }
}
