using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class PriceRepository : IPriceRepository
    {
        private readonly IDictionary<string, double> _prices;
        public PriceRepository()
        {
            _prices = new Dictionary<string, double>
            {
                {"A", 50},
                {"B", 30},
                {"C", 20},
                {"D", 15}
            };
        }

        public double GetPrice(string skuName)
        {
            return _prices[skuName];
        }
    }
}
