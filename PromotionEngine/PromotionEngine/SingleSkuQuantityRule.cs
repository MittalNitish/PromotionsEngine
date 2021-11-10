using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class SingleSkuQuantityRule : IPromotionRule
    {
        public string SkuName { get; set; }
        public int Quantity { get; set; }
        public IDiscountedValue DiscountedValue { get; set; }

        public double MatchRule(IList<PricedOrder> orders)
        {
            double discountedValue = 0;
            foreach (var order in orders)
            {
                if (order.SkuName.Equals(SkuName) && order.Quantity >= Quantity)
                {
                    var discountCount = order.Quantity / Quantity;
                    //Add the discount on applicable quantities
                    discountedValue = discountCount * DiscountedValue.GetDiscountedValue(Quantity * order.Price);
                    order.Quantity -= discountCount * Quantity;
                    //Since this rule applies to only one SKU at a time, return from loop
                    break;
                }
            }

            return discountedValue;
        }
    }
}
