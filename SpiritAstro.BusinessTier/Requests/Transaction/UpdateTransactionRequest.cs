using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.Transaction
{
    public class UpdateTransactionRequest
    {
        public long PaymentId { get; set; }
        public long WalletId { get; set; }
    }
}
