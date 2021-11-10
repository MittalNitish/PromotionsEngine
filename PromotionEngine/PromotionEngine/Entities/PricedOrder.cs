namespace PromotionEngine.Entities
{
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