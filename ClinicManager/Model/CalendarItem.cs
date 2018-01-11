using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class CalendarItem
    {
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public IFunction function { get; set; }

    }
}
