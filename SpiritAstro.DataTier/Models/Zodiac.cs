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
            FamousPeople = new HashSet<FamousPerson>();
            PostZodiacs = new HashSet<PostZodiac>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }

        public virtual ICollection<CustomerZodiac> CustomerZodiacs { get; set; }
        public virtual ICollection<FamousPerson> FamousPeople { get; set; }
        public virtual ICollection<PostZodiac> PostZodiacs { get; set; }
    }
}
