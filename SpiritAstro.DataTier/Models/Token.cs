using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Token
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string TokenString { get; set; }

        public virtual User User { get; set; }
    }
}
