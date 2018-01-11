﻿using System;
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
        private List<Person> clients { get; set; }

        public Clinic(string name)
        {
            Name = name;
            init();
        }
        private void init()
        {
            employees = new List<Person>();
            clients = new List<Person>();
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
        public bool addclient(Person client)
        {
            if (client != null)
            {
                clients.Add(client);

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
        public Person getClient(string name, string lastName)
        {
            return clients.Where(x => x.Name.CompareTo(name.ToUpper()) == 0 && x.LastName.CompareTo(lastName.ToUpper()) == 0).FirstOrDefault();
        }

    }
}