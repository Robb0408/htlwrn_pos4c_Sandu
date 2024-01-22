using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPrice.Data.Model
{
    public class Vendor
    {
        public int VendorId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public List<Availability> Availabilities { get; set; } = new();
        public List<SpecialOffer> SpecialOffers { get; set; } = new();

    }
}
