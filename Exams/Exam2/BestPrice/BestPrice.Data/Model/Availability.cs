using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPrice.Data.Model
{
    public class Availability
    {
        // HAU: ℹ️ small remark - add blank lines between properties
        public int AvailabilityId { get; set; }
        public int StockAmount { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
