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
        public double Price1 { get; set; }
        public double Duration1 { get; set; }
        public double Price2 { get; set; }
        public double Duration2 { get; set; }
        public double Price3 { get; set; }
        public double Duration3 { get; set; }

        public virtual ICollection<Field> Fields { get; set; }
    }
}
