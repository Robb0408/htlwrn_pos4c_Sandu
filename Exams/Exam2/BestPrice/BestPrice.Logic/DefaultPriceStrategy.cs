using BestPrice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPrice.Logic
{
    public class DefaultPriceStrategy : IPriceCalculationStrategy
    {
        public decimal CalculatePrice(ProductPriceCalculationItem productResultItem)
        {
            return productResultItem.Product.Availabilities.Min(a => a.Price) * productResultItem.Amount;
        }
    }
}
