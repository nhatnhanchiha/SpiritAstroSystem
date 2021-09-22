using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Zodiac
    {
        public Zodiac()
        {
            CustomerZodiacs = new HashSet<CustomerZodiac>();
            PostZodiacs = new HashSet<PostZodiac>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual ICollection<CustomerZodiac> CustomerZodiacs { get; set; }
        public virtual ICollection<PostZodiac> PostZodiacs { get; set; }
    }
}
