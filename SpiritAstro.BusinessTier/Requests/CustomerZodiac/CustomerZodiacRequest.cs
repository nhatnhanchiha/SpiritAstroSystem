using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.CustomerZodiac
{
    public class CustomerZodiacRequest
    {
        public long CustomerId { get; set; }
        public int ZodiacId { get; set; }
    }
}
