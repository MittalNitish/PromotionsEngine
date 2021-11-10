using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine
{
    public class MultipleSkuCombinationRule : IPromotionRule
    {
        public IList<SkuCombinationRuleUnit> SkuCombinationRuleUnits { get; set; }
        public IDiscountedValue DiscountedValue { get; set; }

        public double MatchRule(IList<PricedOrder> orders)
        {
            var foundSkus = new List<PricedOrder>();
            double discountedValue = 0;
            foreach (var skuCombinationRuleUnit in SkuCombinationRuleUnits)
            {
                bool foundSku = false;
                foreach (var pricedOrder in orders)
                {
                    if (pricedOrder.SkuName.Equals(skuCombinationRuleUnit.SkuName))
                    {
                        foundSku = true;
                        foundSkus.Add(pricedOrder);
                    }
                }

                if (foundSku == false)
                {
                    //Required SKU is not matched, can not continue to match this rule
                    break;
                }
            }

            if (foundSkus.Count == SkuCombinationRuleUnits.Count)
            {
                var discountedCounts = new Dictionary<string, Tuple<int, int>();
                foreach (var skuCombinationRuleUnit in SkuCombinationRuleUnits)
                {
                    var order = orders.Single(t => t.SkuName.Equals(skuCombinationRuleUnit.SkuName));

                    if (order.Quantity >= skuCombinationRuleUnit.Quantity)
                    {
                        var discountCount = order.Quantity / skuCombinationRuleUnit.Quantity;
                        order.Quantity -= discountCount;
                        discountedCounts.Add(order.SkuName, discountCount);
                    }
                    else
                    {
                        //Order doesn't satisfy the minimum quantity, hence return no discount
                        return 0;
                    }
                }

                double actualValue = 0;
                foreach (var pricedOrder in foundSkus)
                {
                    actualValue += discountedCounts[pricedOrder.SkuName] * pricedOrder.Price;
                }

                discountedValue = DiscountedValue.GetDiscountedValue(actualValue);
            }

            return discountedValue;
        }
    }

    public class SkuCombinationRuleUnit
    {
        public string SkuName { get; set; }
        public int Quantity { get; set; }
    }
}
