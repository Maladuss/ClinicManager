using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Model
{
    public class PriceItem
    {
        public PriceItemType PriceItemType { get; set; }
        public float Price { get; set; }

        public PriceItem(PriceItemType priceItemType, float price)
        {
            PriceItemType = priceItemType;
            Price = price;
        }
    }
}
