using System.ComponentModel.DataAnnotations;

namespace BestPrice.Data.Model
{
    public class Product
    {
        public int ProductId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public List<Availability> Availabilities { get; set; } = new();
        public List<SpecialOffer> SpecialOffers { get; set; } = new();
    }
}