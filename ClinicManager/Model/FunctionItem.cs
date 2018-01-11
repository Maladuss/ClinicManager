using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class FunctionItem
    {
        public FunctionType FunctionType { get; set; }
        public List<PriceItem> PriceItems { get; set; }
        public TimeSpan Time { get; set; }

        public FunctionItem()
        {
            init();
        }
        public void init()
        {
            PriceItems = new List<PriceItem>();
        }
    }
}
