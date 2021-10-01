using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.ViewModels.PriceTable
{
    public class PriceTableModel
    {
        public static string[] Fields =
        {
            "Price1", "Duration1", "Price2", "Duration2", "Price3", "Duration3"
        };

        public double? Price1 { get; set; }
        public double? Duration1 { get; set; }
        public double? Price2 { get; set; }
        public double? Duration2 { get; set; }
        public double? Price3 { get; set; }
        public double? Duration3 { get; set; }
    }
}
