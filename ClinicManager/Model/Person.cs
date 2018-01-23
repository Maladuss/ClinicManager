using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        private PersonType personType;
        public PersonType PersonType
        {
            get
            {
                return personType;
            }
            set
            {
                personType = value;
                if (personType == PersonType.Doctor)
                {
                    initCalendar();
                }
            }
        }
        public List<FunctionItem> FunctionTypes { get; set; }
        public List<CalendarDay> Calendar { get; set; }
        //social security number - pesel
        public string SSN { get; set; }

        public Person()
        {
            FunctionTypes = new List<FunctionItem>();
           
        }
        public Person(string name, string lastName,string ssn, Address address, PersonType personType, List<FunctionItem> funtionItems = null)
        {
            
            Name = name;
            LastName = lastName;
            Address = address;
            SSN = ssn;
            PersonType = personType;
            if (funtionItems != null) FunctionTypes = funtionItems; else FunctionTypes = new List<FunctionItem>();
        }
        private void initCalendar()
        {
            Calendar = new List<CalendarDay>();

            if (PersonType == PersonType.Doctor)
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
        public bool CheckFreeTerm(DateTime? start, FunctionType type)
        {

            if(start != null)
            {
                FunctionItem funItem = getFuntionItem(type);
                DateTime end;
                if(funItem != null)
                {
                    end = new DateTime(funItem.Time.Ticks + start.Value.Ticks);
                    IFunction item = null;
                    CalendarDay day = Calendar.Where(x => x.Date.Date.CompareTo(start.Value.Date) == 0).FirstOrDefault();

                    if (day != null)
                    {
                        item = day.CalendarItems.Where(x =>
                        (x.DateTimeStart >= start && x.DateTimeStart > end) &&
                        (x.DateTimeEnd <= start && x.DateTimeEnd <= end) ||
                        (x.DateTimeStart <= start && x.DateTimeEnd <= end)
                        ).FirstOrDefault();

                        if (item == null)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool addCalendarItem(Person patient, FunctionType functionType, DateTime? TimeStart)
        {
            if (TimeStart.HasValue && patient != null)
            {
                FunctionItem funItem = getFuntionItem(functionType);
                DateTime timeEnd = new DateTime(funItem.Time.Ticks + TimeStart.Value.Ticks);

                CalendarItem item = null;
               
                CalendarDay day = Calendar.Where(x => x.Date.Date.CompareTo(TimeStart.Value.Date) == 0).FirstOrDefault();
                if(day != null)
                {                 
                    IFunction exam = ExamExtension.getExam(functionType);
                    exam.patient = patient;
                    exam.NamePerformer = $"{Name} {LastName}";
                    exam.DateTimeStart = TimeStart.Value;
                    exam.DateTimeEnd = timeEnd;

                    day.CalendarItems.Add(exam);

                    return true;                    
                }                
            }

            return false;
        }
        public bool CheckFunctionTypesExists(FunctionType type)
        {
            return FunctionTypes.Exists(x => x.FunctionType == type);
        }
        private FunctionItem getFuntionItem(FunctionType type)
        {
            return FunctionTypes.Find(x => x.FunctionType == type);
        }
    }
}
