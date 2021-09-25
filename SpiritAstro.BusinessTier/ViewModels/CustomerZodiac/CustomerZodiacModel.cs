using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.ViewModels.CustomerZodiac
{
    public class CustomerZodiacModel
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int ZodiacId { get; set; }
        public int Type { get; set; }
    }
}
