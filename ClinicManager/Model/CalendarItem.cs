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
        public FunctionType FunctionType { get; set; }
        public int PersonId { get; set; }
    }
}
