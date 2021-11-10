using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                bool hasMinimumQuantity = true;

                foreach (var pricedOrder in foundSkus)
                {
                    var sku = SkuCombinationRuleUnits.Single(t => t.SkuName.Equals(pricedOrder.SkuName));
                    if (pricedOrder.Quantity < sku.Quantity)
                    {
                        hasMinimumQuantity = false;
                    }
                }

                if (hasMinimumQuantity == false)
                {
                    //Order does not have minimum quantity from each SKU to process discount
                    return 0;
                }

                int discountCount = 0;
                while (true)
                {
                    bool breakOuter = false;
                    foreach (var pricedOrder in foundSkus)
                    {
                        var sku = SkuCombinationRuleUnits.Single(t => t.SkuName.Equals(pricedOrder.SkuName));
                        if (pricedOrder.Quantity >= sku.Quantity)
                        {
                            pricedOrder.Quantity -= sku.Quantity;
                        }
                        else
                        {
                            breakOuter = true;
                            break;
                        }
                    }

                    if (breakOuter)
                    {
                        break;
                    }
                    ++discountCount;
                }

                double actualValue = 0;
                foreach (var pricedOrder in foundSkus)
                {
                    actualValue += pricedOrder.Price;
                }

                discountedValue = discountCount * DiscountedValue.GetDiscountedValue(actualValue);
                
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
