using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.Zodiac
{
    public class UpdateZodiacRequest
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
    }
}
