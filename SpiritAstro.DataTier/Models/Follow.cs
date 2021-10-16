using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Follow
    {
        public long AstrologerId { get; set; }
        public long CustomerId { get; set; }

        public virtual Astrologer Astrologer { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
