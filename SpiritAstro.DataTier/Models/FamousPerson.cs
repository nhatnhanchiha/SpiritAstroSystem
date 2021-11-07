using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class FamousPerson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ZodiacId { get; set; }
        public string UrlImage { get; set; }
        public bool? Gender { get; set; }
        public string DateOfBirth { get; set; }

        public virtual Zodiac Zodiac { get; set; }
    }
}
