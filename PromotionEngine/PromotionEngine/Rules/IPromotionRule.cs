using System.Collections.Generic;
using PromotionEngine.Entities;

namespace PromotionEngine.Rules
{
    public interface IPromotionRule
    {
        double MatchRule(IList<PricedOrder> orders);
    }
}
