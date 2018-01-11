using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class CalendarDay
    {
        public DateTime Date { get; set; }
        public List<CalendarItem> CalendarItems {get; set;}

        public CalendarDay()
        {
            init();
        }
        private void init()
        {
            CalendarItems = new List<CalendarItem>();
        }
    }
}
