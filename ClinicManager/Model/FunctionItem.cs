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
        public string PriceItemToString
        {
            get
            {
                return $"{PriceItems.Where(x => x.PriceItemType == PriceItemType.STANDARD).FirstOrDefault().Price.ToString()} zł";
            }
        }
        public TimeSpan Time { get; set; }

        public FunctionItem()
        {
            init();
        }
        public FunctionItem(FunctionType functionType, PriceItem priceitem, TimeSpan time)
        {
            init();
            FunctionType = functionType;
            PriceItems.Add(priceitem);
            Time = time;
            
        }
        public void init()
        {
            PriceItems = new List<PriceItem>();
        }
    }
}
