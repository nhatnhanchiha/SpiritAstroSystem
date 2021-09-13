using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            SubCategoryPosts = new HashSet<SubCategoryPost>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<SubCategoryPost> SubCategoryPosts { get; set; }
    }
}
