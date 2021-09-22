using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class CustomerZodiac
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int ZodiacId { get; set; }
        public int Type { get; set; }

        public virtual User Customer { get; set; }
        public virtual Zodiac Zodiac { get; set; }
    }
}
