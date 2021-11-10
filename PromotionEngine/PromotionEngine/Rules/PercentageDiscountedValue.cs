namespace PromotionEngine.Rules
{
    public class PercentageDiscountedValue : IDiscountedValue
    {
        public double Discount  { get; set; }

        public double GetDiscountedValue(double actualValue)
        {
            return actualValue - (actualValue * Discount / 100);
        }
    }
}