using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BestPrice.Logic.ImportModel
{
    public class BestPriceImportModel
    {
        public string[] Products { get; set; } = null!;
        public string[] Vendors { get; set; } = null!;
        public AvailabilityImportModel[] Availabilities { get; set; } = null!;
        public SpecialOfferImportModel[] SpecialOffers { get; set; } = null!;
    }

}

