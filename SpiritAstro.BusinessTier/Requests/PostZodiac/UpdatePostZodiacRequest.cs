using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.PostZodiac
{
    public class UpdatePostZodiacRequest
    {
        public long PostId { get; set; }
        public int ZodiacId { get; set; }
    }
}
