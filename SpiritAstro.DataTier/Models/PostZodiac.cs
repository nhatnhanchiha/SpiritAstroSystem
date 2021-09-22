using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class PostZodiac
    {
        public long PostId { get; set; }
        public int ZodiacId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Zodiac Zodiac { get; set; }
    }
}
