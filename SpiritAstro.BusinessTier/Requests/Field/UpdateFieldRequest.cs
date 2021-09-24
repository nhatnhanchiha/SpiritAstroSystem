using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.Field
{
    public class UpdateFieldRequest
    {
        public string Name { get; set; }
        public long Exp { get; set; }
        public long PriceTableId { get; set; }
    }
}
