using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class ClinicManager
    {
        public List<FunctionItem> functionItems { get; set; }

        public ClinicManager()
        {
            init();
        }
        public void init()
        {
            functionItems = new List<FunctionItem>();
            getFunctionsItems();
        }
        public void getFunctionsItems()
        {
            functionItems.Add(new FunctionItem()
            {
                FunctionType = FunctionType.EKG,
                PriceItems = new List<PriceItem>() { new PriceItem(PriceItemType.STANDARD, 50f), new PriceItem(PriceItemType.NFZ, 0f) },
                Time = new TimeSpan(0, 30, 0)
            });
        }
    }
}
