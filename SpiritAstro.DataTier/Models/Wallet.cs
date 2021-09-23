using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
