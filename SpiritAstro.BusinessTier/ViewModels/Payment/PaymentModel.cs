using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.ViewModels.Payment
{
    public class PaymentModel
    {
        public static string[] Fields =
        {
            "Id", "BookingId"
        };
        public long? Id { get; set; }
        public long? BookingId { get; set; }
    }
}
