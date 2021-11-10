using System.Collections.Generic;
using PromotionEngine.Rules;

namespace PromotionEngine
{
    public class PromotionsRepository : IPromotionsRepository
    {
        public IList<IPromotionRule> GetPromotionRules()
        {
            SingleSkuQuantityRule aRule = new SingleSkuQuantityRule
            {
                SkuName = "A",
                Quantity = 3,
                DiscountedValue = new FixedDiscountedValue {Discount = 20}
            };

            SingleSkuQuantityRule bRule = new SingleSkuQuantityRule
            {
                SkuName = "B",
                Quantity = 2,
                DiscountedValue = new FixedDiscountedValue {Discount = 15}
            };

            MultipleSkuCombinationRule cdRule = new MultipleSkuCombinationRule
            {
                DiscountedValue = new FixedDiscountedValue {Discount = 5},
                SkuCombinationRuleUnits = new List<SkuCombinationRuleUnit>
                {
                    new SkuCombinationRuleUnit{SkuName = "C", Quantity = 1},
                    new SkuCombinationRuleUnit{SkuName = "D", Quantity = 1}
                }
            };

            return new List<IPromotionRule> {aRule, bRule, cdRule};
        }
    }
}