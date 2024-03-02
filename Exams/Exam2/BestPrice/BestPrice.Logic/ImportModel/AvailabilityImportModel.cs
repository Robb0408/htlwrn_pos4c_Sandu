// HAU: ℹ️ consider using single line namespace declarations to reduce nesting of your code 
namespace BestPrice.Logic.ImportModel
{
    public class AvailabilityImportModel
    {
        public string Vendor { get; set; } = null!;
        public string Product { get; set; } = null!;
        public int Amount { get; set; }
        public float Price { get; set; }
    }

}

