using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class PriceTable
    {
        public PriceTable()
        {
            Fields = new HashSet<Field>();
        }

        public long Id { get; set; }

        public virtual ICollection<Field> Fields { get; set; }
    }
}
