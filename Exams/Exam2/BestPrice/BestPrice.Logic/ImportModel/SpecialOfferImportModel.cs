namespace BestPrice.Logic.ImportModel
{
    public class SpecialOfferImportModel
    {
        public string Vendor { get; set; } = null!;
        public string Product { get; set; } = null!;
        public float DiscountRate { get; set; }
        public int MinAmount { get; set; }
    }

}

