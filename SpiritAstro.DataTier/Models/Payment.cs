using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public long BookingId { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
