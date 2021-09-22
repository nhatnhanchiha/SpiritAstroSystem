using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Field
    {
        public Field()
        {
            FieldDetails = new HashSet<FieldDetail>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long Exp { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public long PriceTableId { get; set; }

        public virtual PriceTable PriceTable { get; set; }
        public virtual ICollection<FieldDetail> FieldDetails { get; set; }
    }
}
