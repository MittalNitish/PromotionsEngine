using System;
using System.Collections.Generic;
using System.Text;
using PromotionEngine.Rules;

namespace PromotionEngine
{
    public interface IPromotionsRepository
    {
        IList<IPromotionRule> GetPromotionRules();
    }
}
