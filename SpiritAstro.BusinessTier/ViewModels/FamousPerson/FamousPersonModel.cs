using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.ViewModels.FamousPerson
{
    public class FamousPersonModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ZodiacId { get; set; }
    }


}
