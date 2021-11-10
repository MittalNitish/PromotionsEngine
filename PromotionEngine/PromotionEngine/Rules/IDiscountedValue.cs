namespace PromotionEngine.Rules
{
    public interface IDiscountedValue
    {
        double Discount { get; set; }
        double GetDiscountedValue(double actualValue);
    }
}