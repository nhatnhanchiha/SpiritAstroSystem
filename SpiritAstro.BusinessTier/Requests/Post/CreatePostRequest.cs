using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.Post
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public long CategoryId { get; set; }
        public List<int> ZodiacIds { get; set; }
    }
}
