using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class FieldDetail
    {
        public long AstrologerId { get; set; }
        public long FieldId { get; set; }
        public long Exp { get; set; }

        public virtual Astrologer Astrologer { get; set; }
        public virtual Field Field { get; set; }
    }
}
