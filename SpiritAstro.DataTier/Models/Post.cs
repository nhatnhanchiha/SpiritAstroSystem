using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Post
    {
        public Post()
        {
            SubCategoryPosts = new HashSet<SubCategoryPost>();
            UserPosts = new HashSet<UserPost>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsApprove { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<SubCategoryPost> SubCategoryPosts { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
