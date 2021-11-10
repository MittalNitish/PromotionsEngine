using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IPromotionRule
    {
        double MatchRule(IList<PricedOrder> orders);
    }
}
