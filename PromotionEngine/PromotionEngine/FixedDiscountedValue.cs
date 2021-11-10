using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IDiscountedValue
    {
        double Discount { get; set; }
        double GetDiscountedValue(double actualValue);
    }

    public class FixedDiscountedValue : IDiscountedValue
    {
        public double Discount  { get; set; }

        public double GetDiscountedValue(double actualValue)
        {
            return actualValue - Discount;
        }
    }

    public class PercentageDiscountedValue : IDiscountedValue
    {
        public double Discount  { get; set; }

        public double GetDiscountedValue(double actualValue)
        {
            return actualValue - (actualValue * Discount / 100);
        }
    }
}
