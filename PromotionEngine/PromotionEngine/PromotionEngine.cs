using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PromotionEngine.Entities;

namespace PromotionEngine
{
    public class PromotionEngine
    {
        private readonly IPriceRepository _priceRepository;
        private readonly IPromotionsRepository _promotionsRepository;

        public PromotionEngine(IPriceRepository priceRepository, IPromotionsRepository promotionsRepository)
        {
            _priceRepository = priceRepository;
            _promotionsRepository = promotionsRepository;
        }

        public double Execute(List<Order> orders)
        {
            var promotions = _promotionsRepository.GetPromotionRules();

            var pricedOrders = orders.Select(o => new PricedOrder(o) {Price = _priceRepository.GetPrice(o.SkuName)})
                .ToList();

            double totalOrderValue = 0;
            
            //calculate discounted value
            foreach (var promotionRule in promotions)
            {
                totalOrderValue += promotionRule.MatchRule(pricedOrders);
            }

            //calculate remaining value

            foreach (var pricedOrder in pricedOrders)
            {
                totalOrderValue += pricedOrder.Price * pricedOrder.Quantity;
            }

            return totalOrderValue;
        }
    }
}
