using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Post
    {
        public Post()
        {
            PostPlanets = new HashSet<PostPlanet>();
            PostZodiacs = new HashSet<PostZodiac>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public bool IsApprove { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<PostPlanet> PostPlanets { get; set; }
        public virtual ICollection<PostZodiac> PostZodiacs { get; set; }
    }
}
