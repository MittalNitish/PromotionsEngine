using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IPriceRepository
    {
        double GetPrice(string skuName);
    }
}
