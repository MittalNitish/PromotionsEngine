namespace PromotionEngine.Rules
{
    public class FixedDiscountedValue : IDiscountedValue
    {
        public double Discount  { get; set; }

        public double GetDiscountedValue(double actualValue)
        {
            return actualValue - Discount;
        }
    }
}
