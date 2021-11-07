using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.FamousPerson
{
    public class UpdateFamousPersonRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ZodiacId { get; set; }
        public string UrlImage { get; set; }
        public short Gender { get; set; }
        public string DateOfBirth { get; set; }
    }
}
