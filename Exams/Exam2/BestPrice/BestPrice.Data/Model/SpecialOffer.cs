using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPrice.Data.Model
{
    public class SpecialOffer
    {
        public int SpecialOfferId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;
        [Range(0, 1)]
        public double DiscountRate { get; set; }
        public int MinAmount { get; set; }
    }
}
