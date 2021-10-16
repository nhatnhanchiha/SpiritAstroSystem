using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpiritAstro.BusinessTier.ViewModels.Post;
using SpiritAstro.BusinessTier.ViewModels.Zodiac;

namespace SpiritAstro.BusinessTier.ViewModels.PostZodiac
{
    public class PostZodiacModel
    {
        public static string[] Fields =
        {
            "PostId", "ZodiacId", "Post", "Zodiac"
        };
        public long? PostId { get; set; }
        public int? ZodiacId { get; set; }
        public PostModel Post { get; set; }
        public ZodiacModel Zodiac { get; set; }
    }
}
