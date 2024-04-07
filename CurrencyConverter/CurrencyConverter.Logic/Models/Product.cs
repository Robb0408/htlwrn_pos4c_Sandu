namespace CurrencyConverter.Logic.Models
{
    public class Product
    {
        public string Description { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
