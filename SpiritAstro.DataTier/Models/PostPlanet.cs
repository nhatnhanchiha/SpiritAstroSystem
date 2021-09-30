using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class PostPlanet
    {
        public long PostId { get; set; }
        public int PlanetId { get; set; }

        public virtual Planet Planet { get; set; }
        public virtual Post Post { get; set; }
    }
}
