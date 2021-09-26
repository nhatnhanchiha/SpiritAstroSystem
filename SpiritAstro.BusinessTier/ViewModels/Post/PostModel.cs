using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpiritAstro.BusinessTier.Commons.Attributes;

namespace SpiritAstro.BusinessTier.ViewModels.Post
{
    public class PostModel
    {
        public long? Id { get; set; }
        [String]
        public string Title { get; set; }
        [String]
        public string Content { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public bool? IsApprove { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public long? CategoryId { get; set; }
        public long? AstrologerId { get; set; }
    }
}
