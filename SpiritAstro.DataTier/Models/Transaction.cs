using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Transaction
    {
        public long PaymentId { get; set; }
        public long WalletId { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}
