using BestPrice.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPrice.Logic
{
    public class ProductPriceCalculationItem // maybe use this class as result?
    {
        private readonly IPriceCalculationStrategy strategy;

        public ProductPriceCalculationItem(Product product, decimal amount,
                                            IPriceCalculationStrategy strategy)
        {
            this.strategy = strategy;
            Product = product;
            Amount = amount;
        }

        // product with special offers and availabilities included
        public Product Product { get; set; }

        // calculate price with or without discount based on given strategy
        public decimal Price => strategy.CalculatePrice(this);

        public decimal Amount { get; set; }

        // more of your code here 

        public override string ToString()
        {
            // HAU: ℹ️ you can replace Where with First and use condition in First, consider using FirstOrDefault
            var temp = Product.Availabilities.Where(a => a.Price == Product.Availabilities.Min(a => a.Price)).First();

            // HAU: ℹ️ you have to ThenInclude to the Vendor in the query 
            return $"- {Product.Name}\n\t{temp.Vendor?.Name} | {Amount} x {temp.Price} = {Price}\n\n";
        }
    }
}
