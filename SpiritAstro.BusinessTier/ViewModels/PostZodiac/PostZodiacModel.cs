using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.ViewModels.PostZodiac
{
    public class PostZodiacModel
    {
        public static string[] Fields =
        {
            "PostId", "ZodiacId"
        };
        public long? PostId { get; set; }
        public int? ZodiacId { get; set; }
    }
}
