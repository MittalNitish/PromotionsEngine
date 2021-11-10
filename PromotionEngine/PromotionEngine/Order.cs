using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class Order
    {
        public string SkuName { get; set; }
        public int Quantity { get; set; }
    }

    public class PricedOrder : Order
    {
        public double Price { get; set; }

        public PricedOrder(Order o)
        {
            this.SkuName = o.SkuName;
            this.Quantity = o.Quantity;
        }
    }
}
