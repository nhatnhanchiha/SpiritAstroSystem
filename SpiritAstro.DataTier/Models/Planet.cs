﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Planet
    {
        public Planet()
        {
            PostPlanets = new HashSet<PostPlanet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<PostPlanet> PostPlanets { get; set; }
    }
}
