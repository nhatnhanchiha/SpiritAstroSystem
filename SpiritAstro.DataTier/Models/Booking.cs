using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
        }

        public long Id { get; set; }
        public long AstrologerId { get; set; }
        public long CustomerId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public double? Rating { get; set; }
        public string Note { get; set; }

        public virtual User Astrologer { get; set; }
        public virtual User Customer { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
