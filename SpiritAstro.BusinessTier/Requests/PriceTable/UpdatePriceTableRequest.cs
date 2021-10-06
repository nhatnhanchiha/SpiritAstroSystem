using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.PriceTable
{
    public class UpdatePriceTableRequest
    {
        public double Price1 { get; set; }
        public double Duration1 { get; set; }
        public double Price2 { get; set; }
        public double Duration2 { get; set; }
        public double Price3 { get; set; }
        public double Duration3 { get; set; }
    }
}
