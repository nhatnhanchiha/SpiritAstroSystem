using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.ViewModels.Transaction
{
    public class TransactionModel
    {
        public static string[] Fields =
        {
            "PaymentId", "WalletId"
        };
        public long? PaymentId { get; set; }
        public long? WalletId { get; set; }
    }
}
