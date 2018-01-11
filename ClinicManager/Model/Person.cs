using System;
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
        public string PESEL { get; set; }

        public Person(string name, string lastName, Address address, PersonType personType)
        {
            
            Name = name;
            LastName = lastName;
            Address = address;
            PersonType = personType;
            init();
        }
        private void init()
        {
            Calendar = new List<CalendarDay>();
            FunctionTypes = new List<FunctionItem>();
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
