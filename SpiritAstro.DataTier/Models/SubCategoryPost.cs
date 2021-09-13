using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class SubCategoryPost
    {
        public int SubCategoryId { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
