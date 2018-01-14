﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class Person
    {
        //public int id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public PersonType PersonType { get; set; }
        public List<FunctionItem> FunctionTypes { get; set; }
        public List<CalendarDay> Calendar { get; set; }
        //social security number - pesel
        public string SSN { get; set; }

        public Person(string name, string lastName,string ssn, Address address, PersonType personType, List<FunctionItem> funtionItems = null)
        {
            
            Name = name;
            LastName = lastName;
            Address = address;
            SSN = ssn;
            PersonType = personType;
            if (funtionItems != null) FunctionTypes = funtionItems; else FunctionTypes = new List<FunctionItem>();
            init();
        }
        private void init()
        {
            Calendar = new List<CalendarDay>();
            
            if(PersonType == PersonType.doctor)
            {
                for (int i = 0; i < 100; i++)
                {
                    Calendar.Add(new CalendarDay() { Date = DateTime.Today.AddDays(i) });
                }
            }
            
        }
        public bool addFunctionTypes(FunctionItem functionItem)
        {
            if(functionItem != null)
            {
                FunctionTypes.Add(functionItem);

                return true;
            }

            return false;
        }
        public bool addFunctionTypes(List<FunctionItem> functionItems)
        {
            if (functionItems != null)
            {
                FunctionTypes.AddRange(functionItems);

                return true;
            }

            return false;
        }
        public bool CheckFreeTerm(DateTime start, DateTime end)
        {
            if(start != null && end != null)
            {
                CalendarItem item = null;
                CalendarDay day = Calendar.Where(x => x.Date.Date.CompareTo(start.Date) == 0).FirstOrDefault();

                if(day != null)
                {
                    item = day.CalendarItems.Where(x =>
                    (x.DateTimeStart >= start && x.DateTimeStart > end) &&
                    (x.DateTimeEnd <= start && x.DateTimeEnd <= end) ||
                    (x.DateTimeStart <= start && x.DateTimeEnd <= end)
                    ).FirstOrDefault();

                    if(item == null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool addCalendarItem(Person patient, FunctionType functionType, DateTime TimeStart, DateTime timeEnd)
        {
            if (TimeStart != null && patient != null)
            {
               
                CalendarItem item = null;
               
                CalendarDay day = Calendar.Where(x => x.Date.Date.CompareTo(TimeStart.Date) == 0).FirstOrDefault();
                if(day != null)
                {                 
                    IFunction exam = ExamExtension.getExam(functionType);
                    exam.patient = patient;

                    item = new CalendarItem() { DateTimeStart = TimeStart, DateTimeEnd = timeEnd, function = exam };
                    day.CalendarItems.Add(item);

                    return true;                    
                }                
            }

            return false;
        }
    }
}
