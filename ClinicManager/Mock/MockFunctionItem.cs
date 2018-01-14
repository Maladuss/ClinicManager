using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Mock
{
    public class MockFunctionItem
    {
        public static List<FunctionItem> getFunctionItem(params FunctionType[] funtions)
        {
            List<FunctionItem> items = new List<FunctionItem>();

            foreach(var funtion in funtions)
            {
                switch (funtion)
                {
                    case FunctionType.EEG:
                        items.Add(new FunctionItem()
                        {
                            FunctionType = FunctionType.EEG,
                            PriceItems = new List<PriceItem>() { new PriceItem(PriceItemType.STANDARD, 400f), new PriceItem(PriceItemType.NFZ, 0f) },
                            Time = new TimeSpan(0, 40, 0)
                        }); break;
                    case FunctionType.EKG:
                        items.Add(new FunctionItem()
                        {
                            FunctionType = FunctionType.EKG,
                            PriceItems = new List<PriceItem>() { new PriceItem(PriceItemType.STANDARD, 30f), new PriceItem(PriceItemType.NFZ, 0f) },
                            Time = new TimeSpan(0, 10, 0)
                        }); break;
                    case FunctionType.RTG:
                        items.Add(new FunctionItem()
                        {
                            FunctionType = FunctionType.RTG,
                            PriceItems = new List<PriceItem>() { new PriceItem(PriceItemType.STANDARD, 80f), new PriceItem(PriceItemType.NFZ, 0f) },
                            Time = new TimeSpan(0, 15, 0)
                        }); break;
                    case FunctionType.UKG:
                        items.Add(new FunctionItem()
                        {
                            FunctionType = FunctionType.UKG,
                            PriceItems = new List<PriceItem>() { new PriceItem(PriceItemType.STANDARD, 100f), new PriceItem(PriceItemType.NFZ, 0f) },
                            Time = new TimeSpan(0, 25, 0)
                        }); break;
                }
            }

            return items;
        }
    }
}
